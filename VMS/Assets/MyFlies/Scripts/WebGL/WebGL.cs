using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebGL : MonoBehaviour {
    public Text logo;
    public static bool isIEBool = false;
    private WWW[] www = new WWW[10];
    public static Texture2D handCursor;
    public static Texture2D mouseCursor;
    private string urlCursor = "";
    void Start()
    {
        logo.text = GetJson.logoName;
        if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
            Application.ExternalCall("IsIE", 1);
        urlCursor = Application.streamingAssetsPath;
        for(int i = 0;i <= 1;i++)
        {
            //StartCoroutine(LoadWWW(i));
        }
    }

    public void ResultIsIE(int mint)
    {
       if(mint ==0)//IE
        {
            isIEBool = true;
        }
    }

    private IEnumerator LoadWWW(int i)
    {
        string imgName = "";
        if(i == 0)
        {
            imgName = "/Cursor/hand.png";
        }
        else imgName = "/Cursor/mouse.png";
        www[i] = new WWW(urlCursor + imgName);//只能放URL
        yield return www[i];
        if (www[i] != null && string.IsNullOrEmpty(www[i].error))
        {
            if(i == 0)
                handCursor = www[i].texture;
            else mouseCursor = www[i].texture;
        }
        //Sprite sprite = Sprite.Create(handCursor, new Rect(0, 0, handCursor.width, handCursor.height), new Vector2(0.5f, 0.5f));
    }

    public void ResultIsOpenCm(int it)
    {
        if(it == -1)
        {
            Menu._Instance.CmConfig(0);
        }
    }
}
