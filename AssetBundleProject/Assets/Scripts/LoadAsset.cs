using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsset : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        //   //LoadFromFile
        //   Object obj =AssetBundleHelper.LoadAssetFromAB("CubeWall");
        //if (obj!=null)
        //{
        //    GameObject wallCube=Object.Instantiate(obj) as GameObject;
        //}
        //Object obj1 = AssetBundleHelper.LoadAssetFromAB("SphereWall");
        //if (obj1 != null)
        //{
        //    GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
        //}

        ////LoadFromMemory
        //   Object obj = AssetBundleHelper.LoadAssetFromABMemory("CubeWall");
        //if (obj != null)
        //{
        //    GameObject wallCube = Object.Instantiate(obj) as GameObject;
        //}
        //Object obj1 = AssetBundleHelper.LoadAssetFromABMemory("SphereWall");
        //if (obj1 != null)
        //{
        //    GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
        //}

        ////LoadFromMemoryAsync
        //AssetBundleHelper.LoadAssetFromABMemoryAsync(this,"CubeWall", (obj) =>
        //{
        //    GameObject wallCube = Object.Instantiate(obj) as GameObject;
        //       //加载完一个加载第二个，因为AssetBundle只能有一个
        //    AssetBundleHelper.LoadAssetFromABMemoryAsync(this, "SphereWall", (obj1) =>
        //    {
        //        GameObject sphereCube = Object.Instantiate(obj1) as GameObject;
        //    });
        //   });

        ////LoadFromCacheOrDownload
        //   StartCoroutine(AssetBundleHelper.LoadFromCacheOrDownload());

        ////AssetBundleHelper.LoadFromCacheOrDownloadFromServer()
        //StartCoroutine(AssetBundleHelper.LoadFromCacheOrDownloadFromServer());

        ////AssetBundleHelper.LoadByUnityWebRequest()
        //   StartCoroutine(AssetBundleHelper.LoadByUnityWebRequest());

	    //LoadAllAssetBundleByDepend加载所有依赖
        AssetBundleHelper.LoadAllAssetBundleByDepend();


        //卸载关卡，场景切换的时候卸载。


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
