using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace LIIA.SphereMaze
{
    /// <summary>
    /// Recibe el protocolo BTN desde el puente local serial→UDP.
    /// Evita depender de System.IO.Ports, que no forma parte del perfil Unity usado.
    /// </summary>
    public sealed class Esp32UsbBridgeTiltInput : MonoBehaviour, ISphereTiltInput
    {
        [SerializeField] private int udpPort = 33331;
        [SerializeField] private float staleAfterSeconds = .25f;

        private UdpClient receiver;
        private Vector2 tilt;
        private float lastPacketAt = -999f;
        private string lastError = "Esperando puente USB";

        public bool IsConnected => HasFreshSignal;
        public bool HasFreshSignal => receiver != null && Time.unscaledTime - lastPacketAt <= staleAfterSeconds;
        public float SignalAgeSeconds => Mathf.Max(0f, Time.unscaledTime - lastPacketAt);
        public string PortName => $"UDP 127.0.0.1:{udpPort}";
        public string LastError => lastError;
        public int LastSequence { get; private set; } = -1;
        public Vector2 ReadTilt() => HasFreshSignal ? tilt : Vector2.zero;

        private void Awake()
        {
            try
            {
                receiver = new UdpClient(new IPEndPoint(IPAddress.Loopback, udpPort));
                receiver.Client.Blocking = false;
            }
            catch (Exception exception)
            {
                lastError = exception.GetType().Name;
                Debug.LogWarning($"No se pudo abrir UDP del controlador LIIA: {lastError}");
            }
        }

        private void Update()
        {
            if (receiver == null) return;
            try
            {
                while (receiver.Available > 0)
                {
                    var remote = new IPEndPoint(IPAddress.Loopback, 0);
                    var data = receiver.Receive(ref remote);
                    if (!IPAddress.IsLoopback(remote.Address)) continue;
                    var line = Encoding.ASCII.GetString(data).Trim();
                    if (!TryParsePacket(line, out var sequence, out var parsedTilt)) continue;
                    LastSequence = sequence;
                    tilt = parsedTilt;
                    lastPacketAt = Time.unscaledTime;
                    lastError = string.Empty;
                }
            }
            catch (SocketException) { }
            catch (Exception exception)
            {
                lastError = exception.GetType().Name;
            }
        }

        private void OnDestroy()
        {
            receiver?.Close();
            receiver = null;
        }

        public static bool TryParsePacket(string line, out int sequence, out Vector2 parsedTilt)
        {
            sequence = -1;
            parsedTilt = Vector2.zero;
            if (string.IsNullOrWhiteSpace(line)) return false;
            var parts = line.Trim().Split(',');
            if (parts.Length != 6 || parts[0] != "BTN") return false;
            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out sequence)) return false;
            if (!TryButton(parts[2], out var up) || !TryButton(parts[3], out var down) ||
                !TryButton(parts[4], out var left) || !TryButton(parts[5], out var right)) return false;
            parsedTilt = Vector2.ClampMagnitude(new Vector2(right - left, up - down), 1f);
            return true;
        }

        private static bool TryButton(string value, out float button)
        {
            button = 0f;
            if (value == "0") return true;
            if (value == "1") { button = 1f; return true; }
            return false;
        }
    }
}
