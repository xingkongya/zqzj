using UnityEditor;
using UnityEditor.Build;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEngine;

class MyCustomBuildProcessor : IPostprocessBuild
{
    public int callbackOrder { get { return 0; } }
    public void OnPostprocessBuild(BuildTarget target, string path)
    {
      
    }


    public void Test(BuildTarget target, string path) {
        string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(projectPath);

        Debug.Log("~~~~~" + projectPath);
#if UNITY_2020_2_OR_NEWER
        var targetGuid = pbxProject.GetUnityMainTargetGuid();
#else
        var targetGuid = pbxProject.TargetGuidByName("Unity-iPhone");
#endif
        // pbxProject.SetBuildProperty(targetGuid, "DEVELOPMENT_TEAM","53BVK9ETZD");
        // pbxProject.AddBuildProperty(targetGuid, "PRODUCT_BUNDLE_IDENTIFIER","com.qq.gdt.GDTMobAppDemo");
        pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_STYLE", "Automatic");
        pbxProject.SetBuildProperty(targetGuid, "DEVELOPMENT_TEAM", "46MF6JK5A9");
        pbxProject.SetBuildProperty(targetGuid, "PRODUCT_BUNDLE_IDENTIFIER", "com.qq.gdt.GDTMobAppDemo");
        pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
        pbxProject.SetBuildProperty(targetGuid, "ARCHS", "arm64");
        pbxProject.AddBuildProperty(targetGuid, "EXCLUDED_ARCHS[sdk=iphonesimulator*]", "arm64");
        pbxProject.SetBuildProperty(targetGuid, "PRODUCT_NAME", "UnityDemo");

        pbxProject.WriteToFile(projectPath);

    }
}

