using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class BallFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 followOffset = new Vector3(0f, 9f, -14f);
        [SerializeField] private float positionSmoothTime = 0.28f;
        [SerializeField] private float lookHeight = 0.35f;

        private Vector3 velocity;

        public void Configure(Transform followTarget)
        {
            target = followTarget;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            var desiredPosition = target.position + followOffset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref velocity,
                positionSmoothTime);
            transform.LookAt(target.position + Vector3.up * lookHeight);
        }
    }
}
