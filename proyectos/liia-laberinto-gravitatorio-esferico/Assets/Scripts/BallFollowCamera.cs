using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class BallFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform surfaceCenter;
        [SerializeField] private float heightAboveSurface = 8.5f;
        [SerializeField] private float normalSmoothSpeed = 6f;
        [SerializeField] private float rotationSpeed = 12f;

        private Vector3 cameraNormal;

        public void Configure(Transform followTarget, Transform center)
        {
            target = followTarget;
            surfaceCenter = center;
            cameraNormal = (target.position - surfaceCenter.position).normalized;
        }

        private void LateUpdate()
        {
            if (target == null || surfaceCenter == null) return;

            // Cada punto de la esfera define su propio "arriba". La cámara
            // se ubica sobre la bola respecto a esa normal, por lo que la
            // superficie local siempre se percibe como el piso.
            var surfaceNormal = (target.position - surfaceCenter.position).normalized;
            var normalBlend = 1f - Mathf.Exp(-normalSmoothSpeed * Time.deltaTime);
            cameraNormal = Vector3.Slerp(cameraNormal, surfaceNormal, normalBlend).normalized;

            // La posición se calcula desde el centro: al suavizar la normal
            // recorremos un arco exterior, nunca una recta que atraviese el planeta.
            var cameraDistance = Vector3.Distance(target.position, surfaceCenter.position) + heightAboveSurface;
            transform.position = surfaceCenter.position + cameraNormal * cameraDistance;

            var screenUp = Vector3.ProjectOnPlane(Vector3.up, cameraNormal);
            if (screenUp.sqrMagnitude < 0.001f)
                screenUp = Vector3.ProjectOnPlane(Vector3.forward, cameraNormal);

            var desiredRotation = Quaternion.LookRotation(-cameraNormal, screenUp.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                1f - Mathf.Exp(-rotationSpeed * Time.deltaTime));
        }
    }
}
