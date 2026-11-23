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

            // Fallback explícito para que las flechas funcionen aunque el
            // mapeo legacy no esté disponible en la configuración del editor.
            if (Mathf.Abs(x) < deadZone)
            {
                x = (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f) -
                    (Input.GetKey(KeyCode.LeftArrow) ? 1f : 0f);
            }

            if (Mathf.Abs(y) < deadZone)
            {
                y = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) -
                    (Input.GetKey(KeyCode.DownArrow) ? 1f : 0f);
            }

            var value = Vector2.ClampMagnitude(new Vector2(x, y), 1f);
            return value.sqrMagnitude < deadZone * deadZone ? Vector2.zero : value;
        }
    }
}
