using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class SphereTiltController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour inputSource;
        [SerializeField] private float maxTiltDegrees = 20f;
        [SerializeField] private float tiltResponse = 7f;
        [SerializeField] private float returnSpeed = 5f;

        private ISphereTiltInput input;
        private Quaternion baseRotation;

        public Vector2 CurrentTilt { get; private set; }

        public void ConfigureInput(MonoBehaviour source) => inputSource = source;

        private void Awake()
        {
            baseRotation = transform.localRotation;
            input = inputSource as ISphereTiltInput;
            if (input == null)
            {
                foreach (var component in GetComponents<MonoBehaviour>())
                {
                    if (component is ISphereTiltInput candidate) { input = candidate; break; }
                }
            }
        }

        private void Update()
        {
            var target = input == null ? Vector2.zero : input.ReadTilt();
            CurrentTilt = Vector2.Lerp(CurrentTilt, target, (target == Vector2.zero ? returnSpeed : tiltResponse) * Time.deltaTime);
            var tilt = Quaternion.Euler(-CurrentTilt.y * maxTiltDegrees, 0f, CurrentTilt.x * maxTiltDegrees);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, baseRotation * tilt, 12f * Time.deltaTime);
        }
    }
}
