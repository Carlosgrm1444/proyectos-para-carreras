using UnityEngine;

namespace LIIA.SphereMaze
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class SurfaceBallController : MonoBehaviour
    {
        [SerializeField] private Transform sphereRoot;
        [SerializeField] private float surfaceRadius = 5.22f;
        [SerializeField] private float acceleration = 7f;
        [SerializeField] private float damping = 0.18f;
        [SerializeField] private float surfaceGrip = 12f;
        [SerializeField] private float radialCorrection = 35f;
        [SerializeField] private float fallThreshold = 6.1f;
        [SerializeField] private float radialVelocityDamping = 18f;

        private Rigidbody body;
        private bool respawning;
        private Vector3 spawnPosition;

        public bool IsRespawning => respawning;

        public void Configure(Transform root, float radius, Vector3 spawn)
        {
            sphereRoot = root;
            surfaceRadius = radius;
            spawnPosition = spawn;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.useGravity = false;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            spawnPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (respawning || sphereRoot == null) return;

            var center = sphereRoot.position;
            var offset = body.position - center;
            var normal = offset.sqrMagnitude < 0.001f ? Vector3.up : offset.normalized;

            // La pelota es un peón sobre la superficie: eliminamos cualquier
            // velocidad radial y corregimos la posición antes de aplicar fuerzas.
            // Así no puede despegarse por acumulación de errores del solver físico.
            var distance = offset.magnitude;
            if (distance > 0.001f && Mathf.Abs(distance - surfaceRadius) > 0.0005f)
            {
                body.position = center + normal * surfaceRadius;
                offset = body.position - center;
            }
            body.velocity = Vector3.ProjectOnPlane(body.velocity, normal);
            var gravity = Physics.gravity.sqrMagnitude < 0.001f ? Vector3.down * 9.81f : Physics.gravity;
            var tangentialGravity = Vector3.ProjectOnPlane(gravity, normal);
            body.AddForce(tangentialGravity * acceleration, ForceMode.Acceleration);
            body.AddForce(-body.velocity * damping, ForceMode.Acceleration);

            var tiltController = sphereRoot.GetComponent<SphereTiltController>();
            if (tiltController != null)
            {
                var surfaceVelocity = Vector3.Cross(tiltController.AngularVelocity, offset);
                var relativeVelocity = surfaceVelocity - body.velocity;
                body.AddForce(Vector3.ProjectOnPlane(relativeVelocity, normal) * surfaceGrip, ForceMode.Acceleration);
            }

            var radialError = surfaceRadius - offset.magnitude;
            body.AddForce(normal * radialError * radialCorrection, ForceMode.Acceleration);
            body.AddForce(-normal * Vector3.Dot(body.velocity, normal) * radialVelocityDamping, ForceMode.Acceleration);

            if (Vector3.Distance(body.position, center) > fallThreshold)
                Fall();
        }

        public void SetSpawn(Vector3 position)
        {
            spawnPosition = position;
            body.position = position;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        public void Respawn(Vector3 position)
        {
            if (respawning) return;
            respawning = true;
            body.position = position;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            respawning = false;
        }

        private void Fall()
        {
            if (respawning) return;
            GameFlow.Instance?.PlayerFell();
        }
    }
}
