using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetBundleLoader : MonoBehaviour
{
    //TODO: better way to do this?? Yes! use Action:  https://stackoverflow.com/questions/68889133/how-can-i-await-assetbundle-loadassetasync
    // public GameObject lastLoadedPrefab;

    public enum BundleLocation
    {
        PersistantDataPath,
        StreamingAssets,
        GoogleDrive
    };

    public BundleLocation bundleLocation;
    
 
    public GameObject LoadAssetFromDisk(string bundleName, string assetName)
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleName));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log(">>> Failed to load AssetBundle!");
            return null;
        }

        GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>(assetName);
        Debug.Log(">>> prefab loaded: " + prefab.name);

        myLoadedAssetBundle.Unload(false);

        return prefab;
    }
    public IEnumerator LoadAssetFromDiskAsync(string bundleName, string assetName, Action<GameObject> onFinished)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleName));
        Debug.Log(">>> bundleLoadRequest ...");
        yield return bundleLoadRequest;
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log(">>> Failed to load AssetBundle!");
            yield break;
        }
        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(assetName);
        Debug.Log(">>> assetLoadRequest ... ");
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;
        Debug.Log(">>> prefab loaded: " + prefab.name);

        myLoadedAssetBundle.Unload(false);
        onFinished?.Invoke(prefab);
    }

    public IEnumerator LoadAssetsFromDiskAsync(string bundleName, Action<GameObject[]> onFinished)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleName));
        Debug.Log(">>> bundleLoadRequest ...");
        yield return bundleLoadRequest;
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log(">>> Failed to load AssetBundle!");
            yield break;
        }
        var assetsLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync<GameObject>();

        Debug.Log(">>> assetLoadRequest ... ");
        yield return assetsLoadRequest;

       /*  does not work
        GameObject[] prefabs = new GameObject[assetsLoadRequest.allAssets.Length];
        prefabs = assetsLoadRequest.allAssets as GameObject[];
       */

        UnityEngine.Object[] objects = assetsLoadRequest.allAssets;

        GameObject[] prefabs = new GameObject[objects.Length];
      
        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log(">>> prefab loaded: " + objects[i].name);
            prefabs[i] = objects[i] as GameObject;
        }
       
        myLoadedAssetBundle.Unload(false);
        onFinished?.Invoke(prefabs);
    }

    public IEnumerator LoadWebRequest(Action<GameObject[]> onFinished)
    {
        string url = "https://drive.google.com/uc?export=download&id=18V3lZCqfYT3uvK1qiPNQKLHPNMoNAeZe";
        var request
            = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
        yield return request.SendWebRequest();

        if (request.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError ||
            request.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError ||
            request.result == UnityEngine.Networking.UnityWebRequest.Result.DataProcessingError )
        {
            Debug.Log(request.error);
        }
        AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);

        var assets = bundle.LoadAllAssets<GameObject>();
        GameObject[] prefabs = new GameObject[assets.Length];

        for (int i = 0; i < assets.Length; i++)
        {
            Debug.Log(">>> prefab loaded: " + assets[i].name);
            prefabs[i] = assets[i] as GameObject;
        }
        onFinished?.Invoke(prefabs);
    }


    public void LoadSceneFromBundle(string sceneName)
    {
         
    }
}
