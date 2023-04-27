using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;


public class AndroidBuilder
{
    public static void BuildApk()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/UnionDemo/UnionExample.unity"};
        buildPlayerOptions.locationPathName = GetApkPath();
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private static string GetApkPath()
    {
        string[] commandArgs = Environment.GetCommandLineArgs();
        string path = null;
        int lastIndex = commandArgs.Length - 1;
        for (int i=0; i <= lastIndex; i++)
        {
            if (string.Equals("-apkPath", commandArgs[i]) && i < lastIndex)
            {
                path = commandArgs[i + 1];
                break;
            }
        }
        if (path == null)
        {
            throw (new Exception("no apkPath"));
        }
        Console.WriteLine("Android apk path " + path);
        return path;
    }
}