using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace LIIA.SphereMaze
{
    /// <summary>Utilidad de QA: captura un frame sólo al iniciar con -liiaCapture ruta.png.</summary>
    public sealed class LIIAFrameCapture : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InstallWhenRequested()
        {
            var arguments = Environment.GetCommandLineArgs();
            var startInPlay = Array.Exists(arguments, argument => string.Equals(argument, "-liiaCapturePlay", StringComparison.OrdinalIgnoreCase));
            for (var index = 0; index < arguments.Length - 1; index++)
            {
                if (!string.Equals(arguments[index], "-liiaCapture", StringComparison.OrdinalIgnoreCase)) continue;
                var capture = new GameObject("LIIA_QA_FrameCapture").AddComponent<LIIAFrameCapture>();
                capture.StartCoroutine(capture.Capture(arguments[index + 1], startInPlay));
                return;
            }
        }

        private IEnumerator Capture(string outputPath, bool startInPlay)
        {
            if (startInPlay)
            {
                yield return new WaitForSecondsRealtime(.25f);
                GameFlow.Instance?.RestartAttempt();
            }
            yield return new WaitForSecondsRealtime(2f);
            yield return new WaitForEndOfFrame();
            var image = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            image.Apply(false, false);
            WritePpm(outputPath, image);
            Destroy(image);
            yield return new WaitForSecondsRealtime(1f);
            Application.Quit();
        }

        private static void WritePpm(string outputPath, Texture2D image)
        {
            using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream))
            {
                var header = System.Text.Encoding.ASCII.GetBytes($"P6\n{image.width} {image.height}\n255\n");
                writer.Write(header);
                var pixels = image.GetPixels32();
                for (var y = image.height - 1; y >= 0; y--)
                for (var x = 0; x < image.width; x++)
                {
                    var color = pixels[y * image.width + x];
                    writer.Write(color.r);
                    writer.Write(color.g);
                    writer.Write(color.b);
                }
            }
        }
    }
}
