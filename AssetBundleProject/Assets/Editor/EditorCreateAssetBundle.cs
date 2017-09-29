using System.IO;
using UnityEditor;

public class EditorCreateAssetBundle : Editor
{
    /// <summary>
    /// 根据我们设置的名字，自动去打包
    /// 依赖打包，是将共有的东西额外设置出来一个包，unity会自己去依赖，但是包体会变小很多
    /// 依赖包没加载，其它资源会丢失。
    /// 所以要先load依赖的包
    /// </summary>
    [MenuItem("AssetBundle/Create Prefab AB Android")]
    static void CreateAssetBundleAndroid()
    {
        string assetBundlePath = "Assets/AssetBundles/Android";
        if (!Directory.Exists(assetBundlePath))
        {
            Directory.CreateDirectory(assetBundlePath);
            BuildPipeline.BuildAssetBundles(assetBundlePath, BuildAssetBundleOptions.None, BuildTarget.Android);
        }
    }

    [MenuItem("AssetBundle/Create Prefab AB PC")]
    static void CreateAssetBundlePC()
    {
        string assetBundlePath = "Assets/AssetBundles/PC";
        if (!Directory.Exists(assetBundlePath))
        {
            Directory.CreateDirectory(assetBundlePath);
            BuildPipeline.BuildAssetBundles(assetBundlePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }
    }
}
