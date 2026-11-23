using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildAutomation
{
    private const string WindowsOutputPath = "Builds/Windows/DerechoFirewall.exe";
    private const string WebGlOutputPath = "Builds/WebGL";
    private const string LinuxOutputPath = "Builds/Linux/DerechoFirewall.x86_64";

    [MenuItem("Firewall/Build Windows")]
    public static void BuildWindows()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        Build(WindowsOutputPath, BuildTarget.StandaloneWindows64, "Windows");
    }

    [MenuItem("Firewall/Build WebGL")]
    public static void BuildWebGl()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
        Build(WebGlOutputPath, BuildTarget.WebGL, "WebGL");
    }

    [MenuItem("Firewall/Build Linux")]
    public static void BuildLinux()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        Build(LinuxOutputPath, BuildTarget.StandaloneLinux64, "Linux");
    }

    private static void Build(string outputPath, BuildTarget target, string label)
    {
        var directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Scenes/Main.unity" },
            locationPathName = outputPath,
            target = target,
            options = BuildOptions.None
        });
        Debug.Log($"Firewall {label} build: {report.summary.result} | {report.summary.totalSize} bytes | {report.summary.totalTime}");
        if (report.summary.result != BuildResult.Succeeded) throw new System.Exception($"Build {label} fallido: {report.summary.result}");
    }
}
