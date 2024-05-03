using System;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle 
{
    [MenuItem("Assets/Create Asset Bundle")]
    private static void BuildAssetBundle()
    {
        string assetBundleDirectoryPath = Application.dataPath + "/../../AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, BuildTarget.Android);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

}


