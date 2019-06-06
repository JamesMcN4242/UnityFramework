﻿////////////////////////////////////////////////////////////
/////   UtilityToolbar.cs
/////   James McNeil - 2019
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A utility function bar in the unity editor
/// Methods do not require altering between projects
/// </summary>
public static class UtilityToolbar {

    public const string k_utilityMenuPath = "Utility Menu/";
    public const string k_buildMenuPath = k_utilityMenuPath + "Build/";
    public const string k_buildAndroidMenuPath = k_buildMenuPath + "Android/";
    public const string k_buildWindowsMenuPath = k_buildMenuPath + "Windows/";

#region Menu Items

    /// <summary>
    /// Clear the player prefs saved data
    /// </summary>
    [MenuItem(k_utilityMenuPath + "Clear Player Prefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

#region Build Buttons

    /// <summary>
    /// Builds an android apk file
    /// </summary>
    [MenuItem(k_buildAndroidMenuPath + "Build")]
    public static void BuildAndroid()
    {
        CreateBuild(BuildTarget.Android);
    }

    /// <summary>
    /// Builds and runs an android apk file
    /// </summary>
    [MenuItem(k_buildAndroidMenuPath + "Build and Run")]
    public static void BuildAndRunAndroid()
    {
        CreateBuild(BuildTarget.Android, true);
    }

    /// <summary>
    /// Builds an exe file for the windows platform
    /// </summary>
    [MenuItem(k_buildWindowsMenuPath + "Build")]
    public static void BuildWindows()
    {
        CreateBuild(BuildTarget.StandaloneWindows);
    }

    /// <summary>
    /// Builds and runs an exe file for the windows platform
    /// </summary>
    [MenuItem(k_buildWindowsMenuPath + "Build and Run")]
    public static void BuildAndRunWindows()
    {
        CreateBuild(BuildTarget.StandaloneWindows, true);
    }

#endregion

#endregion

    /// <summary>
    /// Build the player options
    /// </summary>
    /// <param name="target">Build target</param>
    /// <param name="savePath">Path to save to</param>
    /// <param name="autoRunAfterBuild">Should the build run on completion</param>
    /// <returns>Created build player options</returns>
    private static BuildPlayerOptions GetBuildPlayerOptions(BuildTarget target, string savePath, bool autoRunAfterBuild)
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
        buildPlayerOptions.locationPathName = ReturnFilePathWithExtension(target, savePath);
        buildPlayerOptions.target = target;
        buildPlayerOptions.options = autoRunAfterBuild ? BuildOptions.AutoRunPlayer : BuildOptions.None;

        return buildPlayerOptions;
    }

    /// <summary>
    /// Create the build for the specified platform
    /// </summary>
    /// <param name="target">Target platform</param>
    /// <param name="autoRunAfterBuild">Shoud the build autorun on completion</param>
    private static void CreateBuild(BuildTarget target, bool autoRunAfterBuild = false)
    {
        //Ensure we're not already making a build
        if (!BuildPipeline.isBuildingPlayer)
        {
            //Todo: Allow user to specify path
            string savePath = "tempPath";

            BuildPlayerOptions buildPlayerOptions = GetBuildPlayerOptions(target, savePath, autoRunAfterBuild);
            string report = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (!string.IsNullOrEmpty(report))
            {
                Debug.Log("Build Report: " + report);
            }
        }
        else
        {
            Debug.LogWarning("Build already under way");
        }
    }
    
    /// <summary>
    /// Return the file path with the correct extension
    /// </summary>
    /// <param name="targetPlatform">Target platform to be built for</param>
    /// <param name="path">supplied build path</param>
    /// <returns></returns>
    private static string ReturnFilePathWithExtension(BuildTarget targetPlatform, string path)
    {
        string extension = string.Empty;

        switch(targetPlatform)
        {
            case BuildTarget.Android:
                {
                    extension = ".apk";
                    break;
                }

            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                {
                    extension = ".exe";
                    break;
                }

            default:
                {
                    Debug.LogError("No extension type specified for target platform: " + targetPlatform.ToString());
                    break;
                }
        }
               
        //TODO: Check if the extension has already been defined at the end of the string
        return path + extension;
    }
}