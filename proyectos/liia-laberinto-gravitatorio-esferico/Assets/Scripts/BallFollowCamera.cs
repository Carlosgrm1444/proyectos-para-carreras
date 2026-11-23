using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class BallFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform surfaceCenter;
        [SerializeField] private float heightAboveSurface = 10.5f;
        [SerializeField] private float positionSmoothTime = 0.28f;
        [SerializeField] private float rotationSpeed = 9f;

        private Vector3 velocity;

        public void Configure(Transform followTarget, Transform center)
        {
            target = followTarget;
            surfaceCenter = center;
        }

        private void LateUpdate()
        {
            if (target == null || surfaceCenter == null) return;

            // Cada punto de la esfera define su propio "arriba". La cámara
            // se ubica sobre la bola respecto a esa normal, por lo que la
            // superficie local siempre se percibe como el piso.
            var surfaceNormal = (target.position - surfaceCenter.position).normalized;
            var desiredPosition = target.position + surfaceNormal * heightAboveSurface;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref velocity,
                positionSmoothTime);

            var screenUp = Vector3.ProjectOnPlane(Vector3.up, surfaceNormal);
            if (screenUp.sqrMagnitude < 0.001f)
                screenUp = Vector3.ProjectOnPlane(Vector3.forward, surfaceNormal);

            var desiredRotation = Quaternion.LookRotation(-surfaceNormal, screenUp.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                1f - Mathf.Exp(-rotationSpeed * Time.deltaTime));
        }
    }
}
