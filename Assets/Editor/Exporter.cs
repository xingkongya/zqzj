using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Exporter : MonoBehaviour
{
    [@MenuItem("ExportAssert/BuildAssertBundles")]
    static void BuildAssertBundles() {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetsBundles",  //打包路径
            BuildAssetBundleOptions.ChunkBasedCompression,  //AB包构建选项，默认情况下只要选择None就可以了
           BuildTarget.Android  //打包目标平台
           );
    
    }
}