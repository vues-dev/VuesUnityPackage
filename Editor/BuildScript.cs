using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Perform Android Build")]
    public static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/Bootstrap.unity" }; // Replace with your scene paths
        string buildPath = "build/android/game.apk";

        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.None);
        Debug.Log("APK Build Completed!");
    }
}
