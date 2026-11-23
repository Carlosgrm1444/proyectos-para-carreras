using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class SphericalMazeMeshBuilder
    {
        public static void Build(SphericalMazeLayout layout, Transform parent)
        {
            CreateElevatedPath(layout, parent);
        }

        private static void CreateElevatedPath(SphericalMazeLayout layout, Transform parent)
        {
            const float pathHeight = .15f;
            const float undersideHeight = .015f;
            const float borderHeight = .19f;
            const float borderInset = .085f;
            var floorVertices = new List<Vector3>();
            var floorTriangles = new List<int>();
            var sideVertices = new List<Vector3>();
            var sideTriangles = new List<int>();
            var borderVertices = new List<Vector3>();
            var borderTriangles = new List<int>();
            for (var y = 0; y < layout.Height; y++)
            for (var x = 0; x < layout.Width; x++)
            {
                if (!layout.IsWalkable(x, y)) continue;
                AddQuad(floorVertices, floorTriangles,
                    layout.SurfacePoint(x - .5f, y - .5f, pathHeight),
                    layout.SurfacePoint(x + .5f, y - .5f, pathHeight),
                    layout.SurfacePoint(x + .5f, y + .5f, pathHeight),
                    layout.SurfacePoint(x - .5f, y + .5f, pathHeight));

                if (!layout.IsWalkable(x - 1, y))
                    AddBoundary(layout, sideVertices, sideTriangles, borderVertices, borderTriangles,
                        x - .5f, y - .5f, x - .5f, y + .5f,
                        x - .5f + borderInset, y - .5f, x - .5f + borderInset, y + .5f,
                        undersideHeight, pathHeight, borderHeight);
                if (!layout.IsWalkable(x + 1, y))
                    AddBoundary(layout, sideVertices, sideTriangles, borderVertices, borderTriangles,
                        x + .5f, y + .5f, x + .5f, y - .5f,
                        x + .5f - borderInset, y + .5f, x + .5f - borderInset, y - .5f,
                        undersideHeight, pathHeight, borderHeight);
                if (!layout.IsWalkable(x, y - 1))
                    AddBoundary(layout, sideVertices, sideTriangles, borderVertices, borderTriangles,
                        x + .5f, y - .5f, x - .5f, y - .5f,
                        x + .5f, y - .5f + borderInset, x - .5f, y - .5f + borderInset,
                        undersideHeight, pathHeight, borderHeight);
                if (!layout.IsWalkable(x, y + 1))
                    AddBoundary(layout, sideVertices, sideTriangles, borderVertices, borderTriangles,
                        x - .5f, y + .5f, x + .5f, y + .5f,
                        x - .5f, y + .5f - borderInset, x + .5f, y + .5f - borderInset,
                        undersideHeight, pathHeight, borderHeight);
            }

            var floor = CreateMeshObject("MazePath_Ivory", floorVertices, floorTriangles, parent, new Color(.9f, .89f, .79f), .68f, .03f, .56f);
            floor.AddComponent<MeshCollider>().sharedMesh = floor.GetComponent<MeshFilter>().sharedMesh;
            CreateMeshObject("MazePath_Sides", sideVertices, sideTriangles, parent, new Color(.045f, .25f, .3f), .3f, .1f, .42f);
            CreateMeshObject("MazePath_GoldenEdges", borderVertices, borderTriangles, parent, new Color(1f, .54f, .1f), 2.5f, .55f, .78f);
        }

        private static void AddBoundary(
            SphericalMazeLayout layout,
            List<Vector3> sideVertices,
            List<int> sideTriangles,
            List<Vector3> borderVertices,
            List<int> borderTriangles,
            float outerAx, float outerAy, float outerBx, float outerBy,
            float innerAx, float innerAy, float innerBx, float innerBy,
            float undersideHeight, float pathHeight, float borderHeight)
        {
            AddQuad(sideVertices, sideTriangles,
                layout.SurfacePoint(outerAx, outerAy, undersideHeight),
                layout.SurfacePoint(outerBx, outerBy, undersideHeight),
                layout.SurfacePoint(outerBx, outerBy, pathHeight),
                layout.SurfacePoint(outerAx, outerAy, pathHeight));
            AddQuad(borderVertices, borderTriangles,
                layout.SurfacePoint(outerAx, outerAy, borderHeight),
                layout.SurfacePoint(outerBx, outerBy, borderHeight),
                layout.SurfacePoint(innerBx, innerBy, borderHeight),
                layout.SurfacePoint(innerAx, innerAy, borderHeight));
        }

        private static GameObject CreateMeshObject(
            string name,
            List<Vector3> vertices,
            List<int> triangles,
            Transform parent,
            Color color,
            float emission,
            float metallic,
            float smoothness)
        {
            var result = new GameObject(name);
            result.transform.SetParent(parent, false);
            var mesh = new Mesh { name = name + "Mesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            result.AddComponent<MeshFilter>().sharedMesh = mesh;
            var material = LIIAMaterialFactory.Create(color, emission, metallic, smoothness);
            if (material != null) result.AddComponent<MeshRenderer>().sharedMaterial = material;
            return result;
        }

        private static void AddQuad(List<Vector3> vertices, List<int> triangles, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            var index = vertices.Count;
            vertices.Add(a); vertices.Add(b); vertices.Add(c); vertices.Add(d);
            triangles.Add(index); triangles.Add(index + 1); triangles.Add(index + 2);
            triangles.Add(index); triangles.Add(index + 2); triangles.Add(index + 3);
        }
    }
}
