using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAPrototypeBootstrap : MonoBehaviour
    {
        [SerializeField] private float sphereRadius = 5f;
        [SerializeField] private float trackRadius = 5.08f;
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
            Physics.gravity = Vector3.down * 9.81f;

            var root = NewObject("SphereRoot");
            var visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visual.name = "SphereVisual";
            visual.transform.SetParent(root.transform, false);
            visual.transform.localScale = Vector3.one * sphereRadius * 2f;
            Destroy(visual.GetComponent<Collider>());
            SetColor(visual, new Color(0.08f, 0.16f, 0.25f, 0.55f));

            var input = root.AddComponent<KeyboardSphereTiltInput>();
            var tilt = root.AddComponent<SphereTiltController>();
            tilt.ConfigureInput(input);

            BuildEquator(root.transform);
            BuildUpperRing(root.transform);
            BuildBridge(root.transform);

            var ballObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ballObject.name = "Ball";
            ballObject.transform.position = SurfacePoint(0f, 0f, trackRadius + 0.22f);
            ballObject.transform.localScale = Vector3.one * 0.44f;
            SetColor(ballObject, new Color(1f, 0.75f, 0.1f));
            var ball = ballObject.AddComponent<SurfaceBallController>();
            ball.Configure(root.transform, trackRadius + 0.22f, ballObject.transform.position);
            var body = ballObject.GetComponent<Rigidbody>() ?? ballObject.AddComponent<Rigidbody>();
            body.mass = 1f;
            body.drag = 0.05f;

            var flow = NewObject("GameFlow").AddComponent<GameFlow>();
            flow.Configure(ball);
            NewObject("HUD").AddComponent<LIIAHUD>();
            CreateCheckpoint(root.transform, 55f);
            CreateStar(root.transform, 115f, 35f);
            CreateGoal(root.transform, 175f, 35f);
            CreateCamera(ballObject.transform, root.transform);
            CreateLight();
        }

        private void BuildEquator(Transform root)
        {
            for (var i = 0; i < 32; i++)
            {
                if (i == 8 || i == 9) continue;
                CreateTrack(root, i * 360f / 32f, 0f);
            }
        }

        private void BuildUpperRing(Transform root)
        {
            for (var i = 0; i < 20; i++) CreateTrack(root, 180f + i * 360f / 20f, 35f);
        }

        private void BuildBridge(Transform root)
        {
            for (var i = 1; i <= 5; i++) CreateTrack(root, 30f + i * 5f, i * 7f);
        }

        private void CreateTrack(Transform root, float longitude, float latitude)
        {
            var normal = SphericalDirection(longitude, latitude);
            var tangent = SphericalDirection(longitude + 1f, latitude) - normal;
            var track = GameObject.CreatePrimitive(PrimitiveType.Cube);
            track.name = "TrackSegment";
            track.transform.SetParent(root, false);
            track.transform.position = normal * trackRadius;
            track.transform.rotation = Quaternion.LookRotation(tangent.normalized, normal);
            track.transform.localScale = new Vector3(0.72f, 0.16f, 1.15f);
            SetColor(track, new Color(0.18f, 0.65f, 0.85f));

            var right = track.transform.right;
            CreateRail(root, track.transform.position + right * 0.48f + normal * 0.25f, track.transform.rotation);
            CreateRail(root, track.transform.position - right * 0.48f + normal * 0.25f, track.transform.rotation);
        }

        private void CreateRail(Transform root, Vector3 position, Quaternion rotation)
        {
            var rail = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rail.name = "TrackRail";
            rail.transform.SetParent(root, false);
            rail.transform.position = position;
            rail.transform.rotation = rotation;
            rail.transform.localScale = new Vector3(0.12f, 0.45f, 1.15f);
            SetColor(rail, new Color(0.08f, 0.35f, 0.5f));
        }

        private void CreateCheckpoint(Transform root, float longitude)
        {
            var point = SurfacePoint(longitude, 0f, trackRadius + 0.2f);
            var checkpoint = NewObject("Checkpoint");
            checkpoint.transform.SetParent(root, true);
            checkpoint.transform.position = point;
            var collider = checkpoint.AddComponent<SphereCollider>();
            collider.radius = 0.55f;
            collider.isTrigger = true;
            checkpoint.AddComponent<Checkpoint>();
        }

        private void CreateStar(Transform root, float longitude, float latitude)
        {
            var point = SurfacePoint(longitude, latitude, trackRadius + 0.55f);
            var star = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            star.name = "Star";
            star.transform.SetParent(root, true);
            star.transform.position = point;
            star.transform.localScale = new Vector3(0.32f, 0.12f, 0.32f);
            SetColor(star, new Color(1f, 0.8f, 0.05f));
            var collider = star.GetComponent<Collider>();
            collider.isTrigger = true;
            star.AddComponent<CollectibleStar>();
        }

        private void CreateGoal(Transform root, float longitude, float latitude)
        {
            var point = SurfacePoint(longitude, latitude, trackRadius + 0.45f);
            var goal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            goal.name = "Goal";
            goal.transform.SetParent(root, true);
            goal.transform.position = point;
            goal.transform.localScale = new Vector3(0.8f, 0.08f, 0.8f);
            SetColor(goal, new Color(0.1f, 1f, 0.25f));
            goal.GetComponent<Collider>().isTrigger = true;
            goal.AddComponent<Goal>();
        }

        private Vector3 SurfacePoint(float longitude, float latitude, float radius)
        {
            return SphericalDirection(longitude, latitude) * radius;
        }

        private static Vector3 SphericalDirection(float longitude, float latitude)
        {
            var lon = longitude * Mathf.Deg2Rad;
            var lat = latitude * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(lat) * Mathf.Cos(lon), Mathf.Sin(lat), Mathf.Cos(lat) * Mathf.Sin(lon)).normalized;
        }

        private GameObject NewObject(string objectName)
        {
            var result = new GameObject(objectName);
            generated.Add(result);
            return result;
        }

        private void CreateCamera(Transform ball, Transform sphereCenter)
        {
            var cameraObject = new GameObject("Main Camera");
            var camera = cameraObject.AddComponent<Camera>();
            camera.tag = "MainCamera";
            var normal = (ball.position - sphereCenter.position).normalized;
            camera.transform.position = ball.position + normal * 10.5f;
            camera.transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
            camera.fieldOfView = 42f;
            cameraObject.AddComponent<BallFollowCamera>().Configure(ball, sphereCenter);
        }

        private void CreateLight()
        {
            var light = new GameObject("Key Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            light.transform.rotation = Quaternion.Euler(35f, -25f, 0f);
        }

        private static void SetColor(GameObject target, Color color)
        {
            var renderer = target.GetComponent<Renderer>();
            if (renderer == null) return;
            var material = new Material(Shader.Find("Standard")) { color = color };
            renderer.material = material;
        }
    }
}
