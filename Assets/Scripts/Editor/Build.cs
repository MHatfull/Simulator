// C# example.
using UnityEditor;
using UnityEngine;

public class Build : MonoBehaviour
{
    [MenuItem("Build/Build all")]
    public static void MyBuild()
    {
        BuildPipeline.BuildPlayer(WindowsBuildOptions());
        BuildPipeline.BuildPlayer(LinuxBuildOptions());
    }

    static BuildPlayerOptions WindowsBuildOptions()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/main.unity" };
        buildPlayerOptions.locationPathName = "Builds/Windows/Windows.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        return buildPlayerOptions;
    }

    static BuildPlayerOptions LinuxBuildOptions()
    {
        EditorUserBuildSettings.enableHeadlessMode = true;
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/main.unity" };
        buildPlayerOptions.locationPathName = "Builds/Linux/Linux.x86_64";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.None;
        return buildPlayerOptions;
    }
}