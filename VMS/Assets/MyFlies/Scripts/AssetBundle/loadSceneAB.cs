using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class loadSceneAB : MonoBehaviour
{
    private AsyncOperation async;
    private string ul = "";
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ul = Application.streamingAssetsPath + "/AssetBundles/PC/scene/MS_F.unity3d";
        }
        else
        {
            ul = Application.streamingAssetsPath + "/AssetBundles/Web/scene/MS_F.unity3d";
        }

        StartCoroutine(LoadScene());
    }

    private void Update()
    {
    }

    /******************AB加载地图**********************/
    private IEnumerator LoadScene()
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(ul);
        
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            AsyncOperation async = SceneManager.LoadSceneAsync("FirstLoad");
            yield return async;
            ab.Unload(false);
        }

    }
    /******************AB加载地图**********************/
}
