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

            var level = MazeLevelCatalog.Selected;
            var map = Resources.Load<Texture2D>($"Maps/{level.ResourceName}");
            if (map == null)
            {
                Debug.LogError($"No se encontró Resources/Maps/{level.ResourceName}.png. Ejecuta LIIA > Generar mapas PNG de ejemplo.");
                return;
            }
            if (!SphericalMazeValidator.TryValidate(map, out var mapValidation))
            {
                Debug.LogError($"Mapa LIIA inválido: {mapValidation}");
                return;
            }
            Debug.Log(mapValidation);

            var root = NewObject("PlanetRoot");
            var planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planet.name = "PlanetVisual_Static";
            planet.transform.SetParent(root.transform, false);
            planet.transform.localScale = Vector3.one * sphereRadius * 2f;
            Destroy(planet.GetComponent<Collider>());
            SetColor(planet, new Color(.015f, .045f, .08f));

            var layout = root.AddComponent<SphericalMazeLayout>();
            layout.Configure(map, mazeSurfaceRadius);
            SphericalMazeMeshBuilder.Build(layout, root.transform);

            var input = root.AddComponent<KeyboardSphereTiltInput>();
            NewObject("Audio").AddComponent<LIIAAudioFeedback>();
            var ballObject = new GameObject("Ball_ConstrainedToMap");
            ballObject.name = "Ball_ConstrainedToMap";
            var ballVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ballVisual.name = "BallVisual_Rolling";
            ballVisual.transform.SetParent(ballObject.transform, false);
            ballVisual.transform.localScale = Vector3.one * .44f;
            Destroy(ballVisual.GetComponent<Collider>());
            SetColor(ballVisual, new Color(1f, .73f, .08f));
            var ball = ballObject.AddComponent<MazeBallController>();
            ball.Configure(layout, input, ballVisual.transform);

            var flow = NewObject("GameFlow").AddComponent<GameFlow>();
            flow.Configure(ball, level);
            NewObject("HUD").AddComponent<LIIAHUD>();
            var diagnostics = NewObject("Diagnostics").AddComponent<LIIADevDiagnostics>();
            diagnostics.Configure(ball, layout);
            var miniMap = NewObject("MiniMap").AddComponent<LIIAMiniMap>();
            miniMap.Configure(ball, layout);
            CreateMarker("Start", layout.StartCell, layout, new Color(.12f, .95f, .4f));
            CreateMarker("Goal", layout.GoalCell, layout, new Color(.98f, .18f, .32f));
            foreach (var cell in layout.StarCells)
                CreateGameplayMarker(MazeTile.Star, cell, layout, new Color(1f, .75f, .08f));
            foreach (var cell in layout.CheckpointCells)
                CreateGameplayMarker(MazeTile.Checkpoint, cell, layout, new Color(.2f, .6f, 1f));
            foreach (var cell in layout.TrapCells)
                CreateGameplayMarker(MazeTile.Trap, cell, layout, new Color(.9f, .15f, .95f));
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

        private static void CreateGameplayMarker(MazeTile type, Vector2Int cell, SphericalMazeLayout layout, Color color)
        {
            var primitive = type == MazeTile.Star ? PrimitiveType.Sphere :
                type == MazeTile.Checkpoint ? PrimitiveType.Capsule : PrimitiveType.Cube;
            var marker = GameObject.CreatePrimitive(primitive);
            marker.name = type.ToString();
            marker.transform.SetParent(layout.transform, true);
            var normal = layout.SurfaceNormal(cell.x, cell.y);
            marker.transform.position = layout.SurfacePoint(cell.x, cell.y, .16f);
            marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            marker.transform.localScale = type == MazeTile.Star
                ? new Vector3(.28f, .28f, .28f)
                : type == MazeTile.Checkpoint
                    ? new Vector3(.18f, .34f, .18f)
                    : new Vector3(.42f, .1f, .42f);
            Destroy(marker.GetComponent<Collider>());
            SetColor(marker, color);
            marker.AddComponent<MazeGameplayMarker>().Configure(type, cell);
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
            camera.transform.position = sphereCenter.position + normal * (Vector3.Distance(ball.position, sphereCenter.position) + 4.6f);
            camera.transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
            camera.fieldOfView = 54f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(.025f, .055f, .1f);
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
