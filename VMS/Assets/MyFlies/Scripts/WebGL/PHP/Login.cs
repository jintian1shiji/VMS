using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Commons;


public class Login : MonoBehaviour
{
    public InputField userIdField;
    public InputField passwordField;
    public Text statusText;


    public Button loginBtn;
    private string userId = "";
    private string password = "";
    private string url = "http://10.12.6.28/WE/login.php";

    private void Awake()
    {
        loginBtn.onClick.AddListener(OnLogin);
    }
    public void OnLogin()
    {
        userId = userIdField.text;
        password = passwordField.text;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            print("账户和密码不能为空");

            return;
        }

        StartCoroutine(logining());
    }

    private IEnumerator logining()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", userId);
        //form.AddField("password", password);  
        form.AddField("password", Common.StrEncrypMd5(Common.StrEncrypMd5(password)));  //双重加密  

        WWW www = new WWW(url, form);


        yield return www;

        if (www.error != null)
        {
            print("error is login:" + www.error);
            statusText.text = www.error + "...";
        }
        else
        {
            print(www.text);
            statusText.text = www.text;
        }

    }

}
