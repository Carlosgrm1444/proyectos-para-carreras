using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class KeyboardSphereTiltInput : MonoBehaviour, ISphereTiltInput
    {
        [SerializeField] private float deadZone = 0.05f;

        public Vector2 ReadTilt()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var value = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            return value.sqrMagnitude < deadZone * deadZone ? Vector2.zero : value;
        }
    }
}
