using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Perform Android Build")]
    public static void PerformBuild()
    {
        string[] scenes = GetScenesFromBuildSettings(); // Replace with your scene paths
        string buildPath = "build/android/game.apk";

        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.None);
        Debug.Log("APK Build Completed!");
    }

    public static string[] GetScenesFromBuildSettings()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path) // Return full scene path instead of just name
            .ToArray();
    }
}
