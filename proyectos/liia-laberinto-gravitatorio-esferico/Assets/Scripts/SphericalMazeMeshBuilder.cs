using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class SphericalMazeMeshBuilder
    {
        public static void Build(SphericalMazeLayout layout, Transform parent)
        {
            CreateFloor(layout, parent);
            CreateWalls(layout, parent);
        }

        private static void CreateFloor(SphericalMazeLayout layout, Transform parent)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            for (var y = 0; y < layout.Height; y++)
            for (var x = 0; x < layout.Width; x++)
            {
                if (!layout.IsWalkable(x, y)) continue;
                AddQuad(vertices, triangles,
                    layout.SurfacePoint(x - .5f, y - .5f, .01f),
                    layout.SurfacePoint(x + .5f, y - .5f, .01f),
                    layout.SurfacePoint(x + .5f, y + .5f, .01f),
                    layout.SurfacePoint(x - .5f, y + .5f, .01f));
            }
            CreateMeshObject("MazeFloor", vertices, triangles, parent, new Color(.06f, .72f, .9f), .18f);
        }

        private static void CreateWalls(SphericalMazeLayout layout, Transform parent)
        {
            const float wallHeight = .58f;
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            for (var y = 0; y < layout.Height; y++)
            for (var x = 0; x < layout.Width; x++)
            {
                if (layout.IsWalkable(x, y)) continue;
                var a = layout.SurfacePoint(x - .5f, y - .5f, .01f);
                var b = layout.SurfacePoint(x + .5f, y - .5f, .01f);
                var c = layout.SurfacePoint(x + .5f, y + .5f, .01f);
                var d = layout.SurfacePoint(x - .5f, y + .5f, .01f);
                var at = layout.SurfacePoint(x - .5f, y - .5f, wallHeight);
                var bt = layout.SurfacePoint(x + .5f, y - .5f, wallHeight);
                var ct = layout.SurfacePoint(x + .5f, y + .5f, wallHeight);
                var dt = layout.SurfacePoint(x - .5f, y + .5f, wallHeight);
                AddQuad(vertices, triangles, at, bt, ct, dt);
                AddQuad(vertices, triangles, a, at, dt, d);
                AddQuad(vertices, triangles, b, c, ct, bt);
                AddQuad(vertices, triangles, a, b, bt, at);
                AddQuad(vertices, triangles, d, dt, ct, c);
            }
            var walls = CreateMeshObject("MazeWalls_ExtrudedFromPng", vertices, triangles, parent, new Color(.035f, .2f, .32f), .04f);
            walls.AddComponent<MeshCollider>().sharedMesh = walls.GetComponent<MeshFilter>().sharedMesh;
        }

        private static GameObject CreateMeshObject(string name, List<Vector3> vertices, List<int> triangles, Transform parent, Color color, float emission)
        {
            var result = new GameObject(name);
            result.transform.SetParent(parent, false);
            var mesh = new Mesh { name = name + "Mesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            result.AddComponent<MeshFilter>().sharedMesh = mesh;
            var material = new Material(Shader.Find("Standard")) { color = color };
            if (emission > 0f)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * emission);
            }
            result.AddComponent<MeshRenderer>().sharedMaterial = material;
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
