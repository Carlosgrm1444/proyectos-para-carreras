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
                Debug.LogError($"No se encontró Resources/Maps/{level.ResourceName}.png. Ejecuta LIIA > Generar mapas PNG finales.");
                return;
            }
            if (!SphericalMazeValidator.TryValidate(map, out var mapValidation, level.WrapHorizontally))
            {
                Debug.LogError($"Mapa LIIA inválido: {mapValidation}");
                return;
            }
            Debug.Log(mapValidation);

            var root = NewObject("PlanetRoot");
            var planet = new GameObject("PlanetVisual_LowPoly");
            planet.name = "PlanetVisual_Static";
            planet.transform.SetParent(root.transform, false);
            planet.AddComponent<MeshFilter>().sharedMesh = LIIAGeometryFactory.CreateLowPolySphere(sphereRadius);
            planet.AddComponent<MeshRenderer>().sharedMaterial = LIIAMaterialFactory.Create(new Color(.065f, .34f, .4f), .3f, .16f, .48f);

            var layout = root.AddComponent<SphericalMazeLayout>();
            layout.Configure(
                map,
                mazeSurfaceRadius,
                level.LongitudeSpan,
                level.LatitudeSpan,
                level.WrapHorizontally);
            SphericalMazeMeshBuilder.Build(layout, root.transform);

            var keyboardInput = root.AddComponent<KeyboardSphereTiltInput>();
            var esp32Input = root.AddComponent<Esp32UsbBridgeTiltInput>();
            var input = root.AddComponent<SphereTiltInputRouter>();
            input.Configure(keyboardInput, esp32Input);
            var ballObject = new GameObject("Ball_ConstrainedToMap");
            ballObject.name = "Ball_ConstrainedToMap";
            var ballVisual = new GameObject("BallVisual_Rolling");
            ballVisual.name = "BallVisual_Rolling";
            ballVisual.transform.SetParent(ballObject.transform, false);
            ballVisual.transform.localScale = Vector3.one * .22f;
            ballVisual.AddComponent<MeshFilter>().sharedMesh = LIIAGeometryFactory.CreateLowPolySphere(1f, 8, 12);
            ballVisual.AddComponent<MeshRenderer>().sharedMaterial = LIIAMaterialFactory.Create(new Color(1f, .64f, .08f), 1.9f, .14f, .82f);
            var ball = ballObject.AddComponent<MazeBallController>();
            ball.Configure(layout, input, ballVisual.transform);

            var audio = NewObject("Audio").AddComponent<LIIAAudioFeedback>();
            audio.Configure(ball);
            NewObject("VisualFeedback").AddComponent<LIIAVisualFeedback>();

            var flow = NewObject("GameFlow").AddComponent<GameFlow>();
            flow.Configure(ball, level, 3, layout.StarCells.Count);
            NewObject("HUD").AddComponent<LIIAHUD>();
            var diagnostics = NewObject("Diagnostics").AddComponent<LIIADevDiagnostics>();
            diagnostics.Configure(ball, layout, input);
            var miniMap = NewObject("MiniMap").AddComponent<LIIAMiniMap>();
            miniMap.Configure(ball, layout);
            CreateStartMarker(layout.StartCell, layout);
            CreateGoalMarker(layout.GoalCell, layout);
            foreach (var cell in layout.StarCells)
                CreateGameplayMarker(MazeTile.Star, cell, layout, new Color(1f, .75f, .08f));
            foreach (var cell in layout.CheckpointCells)
                CreateGameplayMarker(MazeTile.Checkpoint, cell, layout, new Color(.2f, .6f, 1f));
            foreach (var cell in layout.TrapCells)
                CreateGameplayMarker(MazeTile.Trap, cell, layout, new Color(.9f, .15f, .95f));
            CreateCamera(ballObject.transform, root.transform);
            CreateLight();
            generated.Add(LIIAGeometryFactory.CreateStarfield(root.transform.position));
        }

        private static void CreateStartMarker(Vector2Int cell, SphericalMazeLayout layout)
        {
            var marker = new GameObject("StartPortal");
            marker.transform.SetParent(layout.transform, true);
            var normal = layout.SurfaceNormal(cell.x, cell.y);
            marker.transform.position = layout.SurfacePoint(cell.x, cell.y, .18f);
            marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            LIIAGeometryFactory.AddGlowRing(marker, new Color(.12f, 1f, .55f), .31f, .045f);
        }

        private static void CreateGoalMarker(Vector2Int cell, SphericalMazeLayout layout)
        {
            var marker = new GameObject("Goal_Flag");
            marker.transform.SetParent(layout.transform, true);
            var normal = layout.SurfaceNormal(cell.x, cell.y);
            marker.transform.position = layout.SurfacePoint(cell.x, cell.y, .17f);
            marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            LIIAGeometryFactory.CreateGoalFlag(marker.transform, new Color(1f, .52f, .08f), new Color(.94f, .92f, .82f));
            LIIAGeometryFactory.AddGlowRing(marker, new Color(1f, .55f, .08f), .38f, .045f);
        }

        private static void CreateGameplayMarker(MazeTile type, Vector2Int cell, SphericalMazeLayout layout, Color color)
        {
            var marker = new GameObject(type.ToString());
            marker.name = type.ToString();
            marker.transform.SetParent(layout.transform, true);
            var normal = layout.SurfaceNormal(cell.x, cell.y);
            marker.transform.position = layout.SurfacePoint(cell.x, cell.y, type == MazeTile.Star ? .48f : .17f);
            marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            if (type == MazeTile.Star)
            {
                marker.AddComponent<MeshFilter>().sharedMesh = LIIAGeometryFactory.CreateStar();
                marker.AddComponent<MeshRenderer>().sharedMaterial = LIIAMaterialFactory.Create(color, 2.25f, .08f, .78f);
            }
            else if (type == MazeTile.Checkpoint)
            {
                var pillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                pillar.name = "CheckpointPillar";
                pillar.transform.SetParent(marker.transform, false);
                pillar.transform.localPosition = Vector3.up * .25f;
                pillar.transform.localScale = new Vector3(.18f, .25f, .18f);
                Destroy(pillar.GetComponent<Collider>());
                pillar.GetComponent<Renderer>().sharedMaterial = LIIAMaterialFactory.Create(color, 1.35f, .35f, .72f);
                LIIAGeometryFactory.AddGlowRing(marker, color, .31f, .035f, .03f);
            }
            else
            {
                for (var index = -1; index <= 1; index++)
                {
                    var spike = new GameObject("TrapSpike");
                    spike.transform.SetParent(marker.transform, false);
                    spike.transform.localPosition = Vector3.right * index * .18f;
                    spike.AddComponent<MeshFilter>().sharedMesh = LIIAGeometryFactory.CreateSpike();
                    spike.AddComponent<MeshRenderer>().sharedMaterial = LIIAMaterialFactory.Create(color, 1.4f, .2f, .52f);
                }
            }
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
            camera.nearClipPlane = .08f;
            camera.farClipPlane = 80f;
            cameraObject.AddComponent<AudioListener>();
            cameraObject.AddComponent<BallFollowCamera>().Configure(ball, sphereCenter);
        }

        private static void CreateLight()
        {
            var light = new GameObject("Key Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.35f;
            light.color = new Color(.86f, .94f, 1f);
            light.transform.rotation = Quaternion.Euler(35f, -25f, 0f);

            var fill = new GameObject("Fill Light").AddComponent<Light>();
            fill.type = LightType.Directional;
            fill.intensity = .58f;
            fill.color = new Color(.45f, .72f, .84f);
            fill.transform.rotation = Quaternion.Euler(-28f, 145f, 0f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(.24f, .34f, .39f);
        }

        private static void SetColor(GameObject target, Color color)
        {
            var renderer = target.GetComponent<Renderer>();
            if (renderer == null) return;
            var material = LIIAMaterialFactory.Create(color);
            if (material != null) renderer.material = material;
        }
    }
}
