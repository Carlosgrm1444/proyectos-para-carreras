using System;
using UnityEngine;
#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif

public static class GameHaptics
{
#if UNITY_STANDALONE_WIN
    [StructLayout(LayoutKind.Sequential)] private struct XInputVibration { public ushort left; public ushort right; }
    [DllImport("xinput1_4.dll", EntryPoint = "XInputSetState", CallingConvention = CallingConvention.StdCall)]
    private static extern uint SetState(uint index, ref XInputVibration vibration);

    public static void Pulse(float low, float high, float duration)
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer) return;
        var vibration = new XInputVibration { left = (ushort)(Mathf.Clamp01(low) * 65535f), right = (ushort)(Mathf.Clamp01(high) * 65535f) };
        for (uint i = 0; i < 4; i++)
        {
            try { SetState(i, ref vibration); } catch (DllNotFoundException) { return; } catch (EntryPointNotFoundException) { return; }
        }
        GameHapticsBehaviour.Ensure().StartPulse(vibration, duration);
    }

    private sealed class GameHapticsBehaviour : MonoBehaviour
    {
        private static GameHapticsBehaviour instance;
        public static GameHapticsBehaviour Ensure()
        {
            if (instance != null) return instance;
            var obj = new GameObject("GameHaptics");
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<GameHapticsBehaviour>();
            return instance;
        }
        public void StartPulse(XInputVibration vibration, float duration) => StartCoroutine(Stop(vibration, duration));
        private System.Collections.IEnumerator Stop(XInputVibration vibration, float duration)
        {
            yield return new WaitForSecondsRealtime(Mathf.Max(.01f, duration));
            var zero = new XInputVibration();
            for (uint i = 0; i < 4; i++) { try { SetState(i, ref zero); } catch (DllNotFoundException) { yield break; } catch (EntryPointNotFoundException) { yield break; } }
        }
    }
#else
    public static void Pulse(float low, float high, float duration) { }
#endif
}

