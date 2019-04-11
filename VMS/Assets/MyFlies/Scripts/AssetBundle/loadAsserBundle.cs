using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class loadAsserBundle : MonoBehaviour
{
    //AB资源地址
    private string url = "";
    //此时AB对象
    public static GameObject nowAssetBundleob;
    //单例
    public static loadAsserBundle instance;
    void Start()
    {
        instance = this.GetComponent<loadAsserBundle>();
        StartCoroutine(InstantiateObject("earth", 0));
    }

    #region AB加载函数
    public IEnumerator InstantiateObject(string name, int index)
    {
        string url = null;
        if (index == 0)
        {

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                url = Application.streamingAssetsPath + "/AssetBundles/PC/" + name + "/" + name + "ab";
                //Debug.Log("运行平台为：windows" + "   " + url);
            }
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                url = Application.streamingAssetsPath + "/AssetBundles/Web/" + name + "/" + name + "ab";
                //Debug.Log("运行平台为：webgl");
            }
            //Debug.Log("url ---------------- " + url);

            UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.Log(request.error);
            }
            else
            {
                AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

                //异步加载对象
                AssetBundleRequest rq = ab.LoadAssetAsync(name + "Pre", typeof(GameObject)); //指定了类型为GameObject
                //等待异步完成
                yield return rq;
                //Debug.Log("异步加载完成");
                //获得加载对象的引用,这个时候是在内存中
                GameObject obj = rq.asset as GameObject;
                obj.layer = 0;
                obj.tag = "Untagged";
                nowAssetBundleob = obj;
                //从内存卸载AssetBundle
                ab.Unload(false);
            }

        }
        else
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                url = Application.streamingAssetsPath + "/AssetBundles/PC/" + name + "/" + name + "ab";
                //Debug.Log("运行平台为：windows" + "   " + url);
            }
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                url = Application.streamingAssetsPath + "/AssetBundles/Web/" + name + "/" + name + "ab";
                //Debug.Log("运行平台为：webgl");
            }
            //Debug.Log("url ---------------- " + url);

            UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.Log(request.error);
            }
            else
            {
                AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                int obNum = 0;
                switch (name)
                {
                    case "build":
                        obNum = 52;
                        break;
                    case "floor":
                        obNum = 7;
                        break;
                    case "tree":
                        obNum = 324;
                        break;
                    case "grass":
                        obNum = 234;
                        break;
                }
                for (int i = 1; i <= obNum; i++)
                {
                    //异步加载对象
                    AssetBundleRequest rq = ab.LoadAssetAsync(name + i, typeof(GameObject)); //指定了类型为GameObject                                                
                    //等待异步完成
                    yield return rq;
                    //Debug.Log("异步加载完成");
                    //获得加载对象的引用,这个时候是在内存中
                    GameObject obj = rq.asset as GameObject;
                    PlayerAllController.instance.InstanceVoid(obj, name, index);
                }
                //从内存卸载AssetBundle
                ab.Unload(false);

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        StartCoroutine(InstantiateObject("floor", 2));
                        break;
                    case 2:
                        StartCoroutine(InstantiateObject("tree", 3));
                        break;
                    case 3:
                        StartCoroutine(InstantiateObject("grass", 4));
                        break;
                    case 4://实例化camera
                        break;
                }
            }

        }

    }
    #endregion
}
