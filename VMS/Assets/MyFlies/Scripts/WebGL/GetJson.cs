using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GetJson : MonoBehaviour
{
    #region 平台配置列表
    //IP Port
    public static string ipStr = "";
    //AB地址
    private string abUrl = "";
    //json地址
    private string urlJson = "";
    //配置客户端监控对应名称地址
    private string urlcName = "";
    //运行平台名称
    public static string platform = string.Empty;
    //logo组件
    public Text logo;
    //IP
    private string mIp = "";
    //Port
    private string mIport = "";
    //Logo名称
    public static string logoName = "";
    //公司名称
    public static string companyName = "";
    //对应监控名称列表
    public static List<string> temCnameList = new List<string>();
    //对应监控名称数量
    public static int intCnameNum = 0;
    #endregion

    #region 监控配置列表
    /***************NVR或者DVR平台*******************/
    public static string ndIp = "";
    public static string ndPort = "";
    public static string ndName = "";
    public static string ndPwd = "";
    /***************NVR或者DVR平台*******************/

    /***************海康EXE平台*******************/
    public static string hkPath = "";
    public static string hkName = "";
    public static string hkBool = "";
    /***************海康EXE平台*******************/
    #endregion

    #region 数据库配置列表
    /***************数据库*******************/
    public static string dbIp = "";
    public static string dbPort = "";
    public static string dbName = "";
    public static string dbPwd = "";
    /***************数据库*******************/
    #endregion



    void Start()
    {
        #region 初始化及获取一些配置地址
        abUrl = "";
        DontDestroyOnLoad(gameObject);//不销毁GetJson挂载物体
        DebugPlatformMesaage();
        if (platform == "Editor" || platform == "Windows")
        {
            urlJson = Application.streamingAssetsPath + "/Config/config.json";
            urlcName = Application.streamingAssetsPath + "/Config/cname.json";
            abUrl = Application.streamingAssetsPath + "/AssetBundles/PC/ui/FirstLoadCanvasab";
        }
        else
        {
            urlJson = Application.streamingAssetsPath + "/Config/config.json";
            urlcName = Application.streamingAssetsPath + "/Config/cname.json";
            abUrl = Application.streamingAssetsPath + "/AssetBundles/Web/ui/FirstLoadCanvasab";
        }
        #endregion

        StartCoroutine(LoadUI());
        StartCoroutine(Getcnamejson());//启协程
    }

    void DebugPlatformMesaage()
    {

#if UNITY_EDITOR
        platform = "Editor";
#elif UNITY_XBOX360
       platform="XBOX360";  
#elif UNITY_IPHONE
       platform="IPHONE";  
#elif UNITY_ANDROID
       platform="ANDROID";  
#elif UNITY_STANDALONE_OSX
       platform="OSX";  
#elif UNITY_STANDALONE_WIN
       platform="Windows";  
#endif
        //Debug.Log("Current Platform:" + platform);
    }

    #region 获取配置文件函数
    /*********************获取配置文件***********************/
    IEnumerator GetConfigJson()
    {

        WWWForm form = new WWWForm();
        form.AddField("id", "321");

        UnityWebRequest request = UnityWebRequest.Get(urlJson);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            //Debug.Log(request.error);
        }
        else
        {
            string jsonStr = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data, 3, request.downloadHandler.data.Length - 3);  // Skip thr first 3 bytes (i.e. the UTF8 BOM)
            //Debug.Log("jsonStr == " + jsonStr);
            JsonData jd = JsonMapper.ToObject(jsonStr);
            for (int i = 0; i < jd.Count; i++)
            {
                mIp = jd[i]["ip"].ToString();
                mIport = jd[i]["iport"].ToString();
                logoName = jd[i]["logo"].ToString();
                companyName = jd[i]["company"].ToString();
                ndIp = jd[i]["ndIp"].ToString();
                ndPort = jd[i]["ndPort"].ToString();
                ndName = jd[i]["ndName"].ToString();
                ndPwd = jd[i]["ndPwd"].ToString();

                dbIp = jd[i]["dbIp"].ToString();
                dbPort = jd[i]["dbPort"].ToString();
                dbName = jd[i]["dbName"].ToString();
                dbPwd = jd[i]["dbPwd"].ToString();

                hkPath = jd[i]["hkPath"].ToString();
                hkName = jd[i]["hkName"].ToString();
                hkBool = jd[i]["hkBool"].ToString();
            }
            logo.text = logoName;
            ipStr = "http://" + mIp + ":" + mIport;
            //print(ipStr);
            //Debug.Log(companyName);
            StopCoroutine(GetConfigJson());//关闭协程
        }
    }
    /*********************获取配置文件***********************/

    /*********************获取通道名称***************/
    IEnumerator Getcnamejson()
    {

        WWWForm form = new WWWForm();
        form.AddField("id", "123");

        UnityWebRequest request = UnityWebRequest.Get(urlcName);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            string jsonStr = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data, 3, request.downloadHandler.data.Length - 3);  // Skip thr first 3 bytes (i.e. the UTF8 BOM)

            //Debug.Log(jsonStr);
            JsonData jd = JsonMapper.ToObject(jsonStr);
            //Debug.Log("------------");
            for (int i = 0; i < jd.Count; i++)
            {
                if (i == 0)
                {
                    string tStr = jd[i]["code"].ToString();
                    intCnameNum = int.Parse(tStr);
                    for (int j = 0; j < intCnameNum; j++)
                    {
                        temCnameList.Add(jd[i][j + 1].ToString());
                    }
                }
            }
            StopCoroutine(Getcnamejson());//关闭协程
        }
    }
    /*********************获取通道名称***********************/
    #endregion


    #region AB加载函数
    /******************AB加载UI**********************/
    private IEnumerator LoadUI()
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(abUrl);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            AssetBundleRequest rq;
            rq = ab.LoadAssetAsync("FirstLoadCanvas", typeof(GameObject)); //指定了类型为GameObject  
            yield return rq;
            //获得加载对象的引用,这个时候是在内存中
            GameObject obj = rq.asset as GameObject;
            GameObject obUI = Instantiate(obj);
            logo = obUI.transform.Find("ImageBack/TextLogo").GetComponent<Text>();
            ab.Unload(false);
        }
        StartCoroutine(GetConfigJson()); // 开启协程
        StopCoroutine(LoadUI());
    }
    /******************AB加载UI**********************/
    #endregion
}
