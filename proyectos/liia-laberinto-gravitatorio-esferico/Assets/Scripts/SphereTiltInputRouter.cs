using UnityEngine;

namespace LIIA.SphereMaze
{
    /// <summary>Combina el control físico con teclado sin perder el respaldo local.</summary>
    public sealed class SphereTiltInputRouter : MonoBehaviour, ISphereTiltInput
    {
        private KeyboardSphereTiltInput keyboard;
        private Esp32UsbBridgeTiltInput esp32;

        public string ActiveSource { get; private set; } = "Teclado";
        public Esp32UsbBridgeTiltInput Esp32 => esp32;

        public void Configure(KeyboardSphereTiltInput keyboardInput, Esp32UsbBridgeTiltInput esp32Input)
        {
            keyboard = keyboardInput;
            esp32 = esp32Input;
        }

        public Vector2 ReadTilt()
        {
            var keyboardValue = keyboard == null ? Vector2.zero : keyboard.ReadTilt();
            // Una tecla sostenida es una anulación deliberada del operador,
            // incluso si el controlador está conectado pero sin botones activos.
            if (keyboardValue.sqrMagnitude > .001f)
            {
                ActiveSource = "Teclado";
                return keyboardValue;
            }

            if (esp32 != null && esp32.HasFreshSignal)
            {
                ActiveSource = "ESP32";
                return esp32.ReadTilt();
            }

            ActiveSource = "Teclado";
            return keyboardValue;
        }
    }
}
