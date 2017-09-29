using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class AssetBundleHelper
{
    public const string assetBundlePathPC = "Assets/AssetBundles/PC/prefab/wall.ab";

    private static AssetBundle LoadAssetBundle()//AssetBundle.LoadFromFile
    {
        AssetBundle assetBundle=AssetBundle.LoadFromFile(assetBundlePathPC);//加载AssetBundle
        return assetBundle;
    }

    private static bool isSendRequest = false;//只一次
    private static IEnumerator LoadFromMemoryAsync(Action<AssetBundle> OnCompleted)
    {
        if (isSendRequest==false)
        {
            isSendRequest = true;
            AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(assetBundlePathPC));
            while (!request.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            if (OnCompleted != null)
            {
                OnCompleted(request.assetBundle);
            }
        }
    }
    private static AssetBundle LoadFromMemory()
    {
        AssetBundle assetBundle=AssetBundle.LoadFromMemory(File.ReadAllBytes(assetBundlePathPC));
        return assetBundle;
    }

    private static AssetBundle assetBundleWall=null;
    public static Object LoadAssetFromAB(string assetName)
    {
        if (assetBundleWall==null)
        {
            assetBundleWall=LoadAssetBundle();//资源包只能加载一次，不能重复加载
        }
        Object assetObj = assetBundleWall.LoadAsset(assetName);//使用里边资源
        return assetObj;
    }

    public static Object LoadAssetFromABMemory(string assetName)
    {
        if (assetBundleWall == null)
        {
            assetBundleWall = LoadFromMemory();//资源包只能加载一次，不能重复加载
        }
        Object assetObj = assetBundleWall.LoadAsset(assetName);//使用里边资源
        return assetObj;
    }
    public static void  LoadAssetFromABMemoryAsync(MonoBehaviour behaviour,string assetName,Action<Object> OnCompleted)
    {
        if (assetBundleWall == null)
        {
            behaviour.StartCoroutine(LoadFromMemoryAsync((assetBundle) =>
            {
                if (assetBundleWall!=null)
                {
                }
                else
                {
                    assetBundleWall = assetBundle;
                }

                Object assetObj = assetBundleWall.LoadAsset(assetName);//使用里边资源
                if (OnCompleted != null)
                {
                    OnCompleted(assetObj);
                }

            }));
        }
        else
        {
            Object assetObj = assetBundleWall.LoadAsset(assetName);//使用里边资源
            if (OnCompleted != null)
            {
                OnCompleted(assetObj);
            }
        }
        
    }

    public static IEnumerator LoadFromCacheOrDownload()
    {
        while (!Caching.ready)
            yield return null;
        //file:/// 三个/
        var www = WWW.LoadFromCacheOrDownload(@"file:///D:\Work\AssetBundleProject\Assets\AssetBundles\PC\prefab\wall.ab", 5);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;
        }
        var myLoadedAssetBundle = www.assetBundle;
        Object obj= myLoadedAssetBundle.LoadAsset("CubeWall");
        GameObject wallCube = Object.Instantiate(obj) as GameObject;
        Object obj1 = myLoadedAssetBundle.LoadAsset("SphereWall");
        GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
    }


    public static IEnumerator LoadFromCacheOrDownloadFromServer()
    {
        while (!Caching.ready)
            yield return null;
        //都用/斜杠
        var www = WWW.LoadFromCacheOrDownload(@"http://localhost:59985/AssetBundles/PC/prefab/wall.ab", 5);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;
        }
        var myLoadedAssetBundle = www.assetBundle;
        Object obj = myLoadedAssetBundle.LoadAsset("CubeWall");
        GameObject wallCube = Object.Instantiate(obj) as GameObject;
        Object obj1 = myLoadedAssetBundle.LoadAsset("SphereWall");
        GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
    }

    public static IEnumerator LoadByUnityWebRequest()//可以本地，也可以服务器

    {
        string uri = "http://localhost:59985/AssetBundles/PC/prefab/wall.ab";
        UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();//进行下载
        //***** request.downloadHandler.data File.WriteAllBytes();//可以吧下载的数据保存到本地。
        AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        Object obj = myLoadedAssetBundle.LoadAsset("CubeWall");
        GameObject wallCube = Object.Instantiate(obj) as GameObject;
        Object obj1 = myLoadedAssetBundle.LoadAsset("SphereWall");
        GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
    }

    public static void LoadAllAssetBundleByDepend()
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/PC/PC");//PC AssetBundle
        AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] dependencies = manifest.GetAllDependencies("prefab/wall.ab"); //Pass the name of the bundle you want the dependencies for.从manifest文件中找到
        foreach (string dependency in dependencies)
        {
            AssetBundle.LoadFromFile(Path.Combine("Assets/AssetBundles/PC", dependency));//根目录加依赖
        }
        //上边加载依赖，下边加载资源
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(assetBundlePathPC);
        Object obj1 = myLoadedAssetBundle.LoadAsset("SphereWall");
        GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
    }




}
