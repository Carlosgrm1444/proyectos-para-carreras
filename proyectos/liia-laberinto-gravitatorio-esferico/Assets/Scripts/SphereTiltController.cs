using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class SphereTiltController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour inputSource;
        [SerializeField] private float rotationSpeed = 70f;

        private ISphereTiltInput input;
        public Vector2 CurrentTilt { get; private set; }
        public Vector3 AngularVelocity { get; private set; }

        public void ConfigureInput(MonoBehaviour source)
        {
            inputSource = source;
            input = source as ISphereTiltInput;
        }

        private void Awake()
        {
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
            if (input == null)
            {
                input = inputSource as ISphereTiltInput;
            }

            CurrentTilt = input == null ? Vector2.zero : input.ReadTilt();
            var axis = new Vector3(-CurrentTilt.y, 0f, CurrentTilt.x);
            if (axis.sqrMagnitude < 0.0001f)
            {
                AngularVelocity = Vector3.zero;
                return;
            }

            axis.Normalize();
            AngularVelocity = axis * rotationSpeed * Mathf.Deg2Rad;
            transform.Rotate(axis, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
