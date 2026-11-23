using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace LIIA.SphereMaze.Editor
{
    public static class BuildLIIA
    {
        private static readonly string[] Scenes = { "Assets/Scenes/LIIA.unity" };

        [MenuItem("LIIA/Build/Linux x86_64")]
        public static void BuildLinux()
        {
            Build(BuildTarget.StandaloneLinux64, "Builds/LIIA-Linux/LIIA");
        }

        [MenuItem("LIIA/Build/Windows x86_64")]
        public static void BuildWindows()
        {
            Build(BuildTarget.StandaloneWindows64, "Builds/LIIA-Windows/LIIA.exe");
        }

        private static void Build(BuildTarget target, string outputPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = Scenes,
                locationPathName = outputPath,
                target = target,
                options = BuildOptions.None
            });
            if (report.summary.result != BuildResult.Succeeded)
                throw new System.InvalidOperationException($"Build falló para {target}: {report.summary.result}");
            UnityEngine.Debug.Log($"Build LIIA terminado: {outputPath} ({report.summary.totalSize / 1024 / 1024} MB)");
        }
    }
}
