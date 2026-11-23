using System;
using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class LIIAGeometryFactory
    {
        public static Mesh CreateLowPolySphere(float radius, int latitudeSegments = 14, int longitudeSegments = 28)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            for (var latitude = 0; latitude < latitudeSegments; latitude++)
            {
                var lat0 = Mathf.Lerp(-Mathf.PI * .5f, Mathf.PI * .5f, latitude / (float)latitudeSegments);
                var lat1 = Mathf.Lerp(-Mathf.PI * .5f, Mathf.PI * .5f, (latitude + 1f) / latitudeSegments);
                for (var longitude = 0; longitude < longitudeSegments; longitude++)
                {
                    var lon0 = longitude / (float)longitudeSegments * Mathf.PI * 2f;
                    var lon1 = (longitude + 1f) / longitudeSegments * Mathf.PI * 2f;
                    var a = SpherePoint(radius, lat0, lon0);
                    var b = SpherePoint(radius, lat0, lon1);
                    var c = SpherePoint(radius, lat1, lon1);
                    var d = SpherePoint(radius, lat1, lon0);
                    AddTriangle(vertices, triangles, a, b, c);
                    AddTriangle(vertices, triangles, a, c, d);
                }
            }

            var mesh = new Mesh { name = "LIIA_LowPolyPlanet" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        public static Mesh CreateStar(float outerRadius = .28f, float innerRadius = .13f, float depth = .09f)
        {
            var outline = new Vector2[10];
            for (var i = 0; i < outline.Length; i++)
            {
                var angle = Mathf.PI * .5f + i * Mathf.PI / 5f;
                var radius = i % 2 == 0 ? outerRadius : innerRadius;
                outline[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }

            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            for (var i = 0; i < outline.Length; i++)
            {
                var next = (i + 1) % outline.Length;
                AddTriangle(vertices, triangles,
                    new Vector3(0f, 0f, -depth),
                    new Vector3(outline[next].x, outline[next].y, -depth),
                    new Vector3(outline[i].x, outline[i].y, -depth));
                AddTriangle(vertices, triangles,
                    new Vector3(0f, 0f, depth),
                    new Vector3(outline[i].x, outline[i].y, depth),
                    new Vector3(outline[next].x, outline[next].y, depth));
                AddQuad(vertices, triangles,
                    new Vector3(outline[i].x, outline[i].y, -depth),
                    new Vector3(outline[next].x, outline[next].y, -depth),
                    new Vector3(outline[next].x, outline[next].y, depth),
                    new Vector3(outline[i].x, outline[i].y, depth));
            }

            return BuildMesh("LIIA_Star", vertices, triangles);
        }

        public static Mesh CreateSpike(float radius = .16f, float height = .45f)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var a = new Vector3(-radius, 0f, -radius);
            var b = new Vector3(radius, 0f, -radius);
            var c = new Vector3(radius, 0f, radius);
            var d = new Vector3(-radius, 0f, radius);
            var tip = new Vector3(0f, height, 0f);
            AddTriangle(vertices, triangles, a, b, tip);
            AddTriangle(vertices, triangles, b, c, tip);
            AddTriangle(vertices, triangles, c, d, tip);
            AddTriangle(vertices, triangles, d, a, tip);
            AddTriangle(vertices, triangles, a, d, c);
            AddTriangle(vertices, triangles, a, c, b);
            return BuildMesh("LIIA_TrapSpike", vertices, triangles);
        }

        public static LineRenderer AddGlowRing(GameObject parent, Color color, float radius, float width, float height = .02f)
        {
            var ring = new GameObject("GlowRing");
            ring.transform.SetParent(parent.transform, false);
            ring.transform.localPosition = Vector3.up * height;
            var line = ring.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.loop = true;
            line.positionCount = 48;
            line.startWidth = width;
            line.endWidth = width;
            line.sharedMaterial = LIIAMaterialFactory.Create(color, 2.2f, .1f, .75f);
            for (var i = 0; i < line.positionCount; i++)
            {
                var angle = i / (float)line.positionCount * Mathf.PI * 2f;
                line.SetPosition(i, new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius));
            }
            return line;
        }

        public static GameObject CreateGoalFlag(Transform parent, Color gold, Color ivory)
        {
            var root = new GameObject("GoalFlag");
            root.transform.SetParent(parent, false);

            var pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            pole.name = "Pole";
            pole.transform.SetParent(root.transform, false);
            pole.transform.localPosition = Vector3.up * .38f;
            pole.transform.localScale = new Vector3(.035f, .38f, .035f);
            UnityEngine.Object.Destroy(pole.GetComponent<Collider>());
            pole.GetComponent<Renderer>().sharedMaterial = LIIAMaterialFactory.Create(gold, .9f, .65f, .8f);

            for (var y = 0; y < 2; y++)
            for (var x = 0; x < 3; x++)
            {
                var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = "FlagTile";
                tile.transform.SetParent(root.transform, false);
                tile.transform.localPosition = new Vector3(.12f + x * .15f, .65f - y * .14f, 0f);
                tile.transform.localScale = new Vector3(.15f, .14f, .025f);
                UnityEngine.Object.Destroy(tile.GetComponent<Collider>());
                tile.GetComponent<Renderer>().sharedMaterial = LIIAMaterialFactory.Create((x + y) % 2 == 0 ? ivory : gold, .35f, .15f, .6f);
            }
            return root;
        }

        public static GameObject CreateStarfield(Vector3 center, int count = 120, float radius = 30f)
        {
            var random = new System.Random(1444);
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            for (var i = 0; i < count; i++)
            {
                var direction = RandomDirection(random);
                var position = center + direction * radius;
                var tangent = Vector3.Cross(direction, Mathf.Abs(Vector3.Dot(direction, Vector3.up)) > .9f ? Vector3.right : Vector3.up).normalized;
                var bitangent = Vector3.Cross(direction, tangent).normalized;
                var size = Mathf.Lerp(.025f, .11f, (float)random.NextDouble());
                AddQuad(vertices, triangles,
                    position - tangent * size - bitangent * size,
                    position - tangent * size + bitangent * size,
                    position + tangent * size + bitangent * size,
                    position + tangent * size - bitangent * size);
            }

            var result = new GameObject("SpaceBackdrop_Stars");
            result.AddComponent<MeshFilter>().sharedMesh = BuildMesh("LIIA_Starfield", vertices, triangles);
            result.AddComponent<MeshRenderer>().sharedMaterial = LIIAMaterialFactory.Create(new Color(.58f, .82f, 1f), 3.4f, 0f, .1f);
            return result;
        }

        private static Vector3 SpherePoint(float radius, float latitude, float longitude)
        {
            return new Vector3(
                Mathf.Cos(latitude) * Mathf.Sin(longitude),
                Mathf.Sin(latitude),
                Mathf.Cos(latitude) * Mathf.Cos(longitude)) * radius;
        }

        private static Vector3 RandomDirection(System.Random random)
        {
            var y = Mathf.Lerp(-1f, 1f, (float)random.NextDouble());
            var angle = (float)random.NextDouble() * Mathf.PI * 2f;
            var horizontal = Mathf.Sqrt(1f - y * y);
            return new Vector3(Mathf.Cos(angle) * horizontal, y, Mathf.Sin(angle) * horizontal);
        }

        private static Mesh BuildMesh(string name, List<Vector3> vertices, List<int> triangles)
        {
            var mesh = new Mesh { name = name };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        private static void AddTriangle(List<Vector3> vertices, List<int> triangles, Vector3 a, Vector3 b, Vector3 c)
        {
            if (Vector3.Cross(b - a, c - a).sqrMagnitude < .0000001f) return;
            var index = vertices.Count;
            vertices.Add(a); vertices.Add(b); vertices.Add(c);
            triangles.Add(index); triangles.Add(index + 1); triangles.Add(index + 2);
        }

        private static void AddQuad(List<Vector3> vertices, List<int> triangles, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            AddTriangle(vertices, triangles, a, b, c);
            AddTriangle(vertices, triangles, a, c, d);
        }
    }
}
