using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAPrototypeBootstrap : MonoBehaviour
    {
        [SerializeField] private float sphereRadius = 5f;
        [SerializeField] private float mazeSurfaceRadius = 5.08f;
        [SerializeField] private bool buildOnStart = true;

        private readonly List<GameObject> generated = new List<GameObject>();

        private void Start()
        {
            if (buildOnStart) BuildPrototype();
        }

        [ContextMenu("Build LIIA prototype")]
        public void BuildPrototype()
        {
            if (GameFlow.Instance != null) return;

            var map = Resources.Load<Texture2D>("Maps/laberinto_01");
            if (map == null)
            {
                Debug.LogError("No se encontró Resources/Maps/laberinto_01.png. Ejecuta LIIA > Generar mapa PNG de ejemplo.");
                return;
            }

            var root = NewObject("PlanetRoot");
            var planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planet.name = "PlanetVisual_Static";
            planet.transform.SetParent(root.transform, false);
            planet.transform.localScale = Vector3.one * sphereRadius * 2f;
            Destroy(planet.GetComponent<Collider>());
            SetColor(planet, new Color(.035f, .1f, .17f));

            var layout = root.AddComponent<SphericalMazeLayout>();
            layout.Configure(map, mazeSurfaceRadius);
            SphericalMazeMeshBuilder.Build(layout, root.transform);

            var input = root.AddComponent<KeyboardSphereTiltInput>();
            var ballObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ballObject.name = "Ball_ConstrainedToMap";
            ballObject.transform.localScale = Vector3.one * .44f;
            SetColor(ballObject, new Color(1f, .73f, .08f));
            var ball = ballObject.AddComponent<MazeBallController>();
            ball.Configure(layout, input);

            var flow = NewObject("GameFlow").AddComponent<GameFlow>();
            flow.Configure(ball);
            NewObject("HUD").AddComponent<LIIAHUD>();
            CreateMarker("Start", layout.StartCell, layout, new Color(.12f, .95f, .4f));
            CreateMarker("Goal", layout.GoalCell, layout, new Color(.98f, .18f, .32f));
            CreateCamera(ballObject.transform, root.transform);
            CreateLight();
        }

        private static void CreateMarker(string name, Vector2Int cell, SphericalMazeLayout layout, Color color)
        {
            var marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            marker.name = name;
            marker.transform.SetParent(layout.transform, true);
            var normal = layout.SurfaceNormal(cell.x, cell.y);
            marker.transform.position = layout.SurfacePoint(cell.x, cell.y, .13f);
            marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            marker.transform.localScale = new Vector3(.35f, .08f, .35f);
            Destroy(marker.GetComponent<Collider>());
            SetColor(marker, color);
        }

        private GameObject NewObject(string objectName)
        {
            var result = new GameObject(objectName);
            generated.Add(result);
            return result;
        }

        private static void CreateCamera(Transform ball, Transform sphereCenter)
        {
            var cameraObject = new GameObject("Main Camera");
            var camera = cameraObject.AddComponent<Camera>();
            camera.tag = "MainCamera";
            var normal = (ball.position - sphereCenter.position).normalized;
            camera.transform.position = sphereCenter.position + normal * 14f;
            camera.transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
            camera.fieldOfView = 48f;
            cameraObject.AddComponent<BallFollowCamera>().Configure(ball, sphereCenter);
        }

        private static void CreateLight()
        {
            var light = new GameObject("Key Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.3f;
            light.transform.rotation = Quaternion.Euler(35f, -25f, 0f);
        }

        private static void SetColor(GameObject target, Color color)
        {
            var renderer = target.GetComponent<Renderer>();
            if (renderer == null) return;
            renderer.material = new Material(Shader.Find("Standard")) { color = color };
        }
    }
}
