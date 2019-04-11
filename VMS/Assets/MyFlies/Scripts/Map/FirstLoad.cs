using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using Commons;
using UnityEngine.Networking;

public class FirstLoad : MonoBehaviour
{
    #region ---------------------
    //是否从初始地图
    public static bool gametime = false;

    //登录Tips
    private GameObject Tips;
    //测试用可以跳过密码
    private bool testBool = false;
    //用户名输入框
    private InputField userIdField;
    //密码输入框
    private InputField passwordField;
    //登录按钮
    private Button loginBtn;
    //账号
    private string userId = "";
    //密码
    private string password = "";
    //login.php地址
    private string url = "";
    //场景ab地址
    private string abUl = "";
    //登录账号
    public static string user_namep;
    #endregion

    #region 获取相关组件及监听
    private void Awake()
    {
        Tips = transform.Find("ImageTips").gameObject;
        userIdField = transform.Find("Loadframe/InputFieldUser").gameObject.GetComponent<InputField>();
        passwordField = transform.Find("Loadframe/InputFieldPWD").gameObject.GetComponent<InputField>();
        loginBtn = transform.Find("Loadframe/ButtonLoad").gameObject.GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLogin);
    }
    #endregion
    void Start()
    {
        #region 获取AB场景地址
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            abUl = Application.streamingAssetsPath + "/AssetBundles/PC/scene/MS_L.unity3d";
        }
        else
        {
            abUl = Application.streamingAssetsPath + "/AssetBundles/Web/scene/MS_L.unity3d";
        }
        #endregion
        userIdField.ActivateInputField();
    }

    #region 关闭Tips
    public void CloseBT()
    {
        Tips.SetActive(false);
    }
    #endregion

    #region 登录判断
    /******************数据库**********************/
    public void OnLogin()
    {
        if (testBool)
        {
            SceneManager.LoadScene(1);
            return;
        }

        userId = userIdField.text;
        password = passwordField.text;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            Tips.SetActive(true);
            GameObject Txt = Tips.transform.Find("Text").gameObject;
            Txt.GetComponent<Text>().text = "账号和密码不能为空！";
            return;
        }

        if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
        {
            url = Application.streamingAssetsPath + "/php/" + "login.php";
        }
        else
        {
            url = GetJson.ipStr + "/php/login.php";
        }

        StartCoroutine(logining());
    }

    private IEnumerator logining()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", userId);
        //form.AddField("password", password);  
        form.AddField("password", Common.StrEncrypMd5(Common.StrEncrypMd5(password)));  //双重加密  
        //Debug.Log("username ===== " + username + " password ==== " + password);

        //WWW www = new WWW(url, form);
        //
        //
        //yield return www;
        //
        //if (www.error != null)
        //{
        //    print(www.error);
        //}
        //else
        //{
        //    if (www.text == "true")
        //    {
        //        StartCoroutine(LoadScene());
        //    }
        //    else
        //    {
        //        Tips.SetActive(true);
        //        GameObject Txt = Tips.transform.Find("Text").gameObject;
        //        Txt.GetComponent<Text>().text = "账号或密码错误！";
        //    }
        //}
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            //Debug.Log(" ----------- " + request.downloadHandler.text);
            if (request.downloadHandler.text == "true")
            {
                StartCoroutine(LoadScene());
            }
            else
            {
                Tips.SetActive(true);
                GameObject Txt = Tips.transform.Find("Text").gameObject;
                Txt.GetComponent<Text>().text = "账号或密码错误！";
            }
        }

    }
    /******************数据库**********************/
    #endregion

    #region AB地图加载
    /******************AB加载地图**********************/
    private IEnumerator LoadScene()
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(abUl);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            AsyncOperation async = SceneManager.LoadSceneAsync("Loading");
            yield return async;
            ab.Unload(false);
        }
    }
    /******************AB加载地图**********************/
    #endregion
}
