using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiaLa : MonoBehaviour {
    /************************视频协议下拉**************************/
    [SerializeField]
    private GameObject AddXL;
    [SerializeField]
    private GameObject ChangeXL;
    [SerializeField]
    private InputField AddInputText;
    [SerializeField]
    private InputField ChangeInputText;

    private int Addint = 0;
    private int Changeint = 0;

    public static string addSpxyText = "RTSP";
    public static string ChangeSpxyText = "RTSP";
    /************************视频协议下拉**************************/

    /************************视频协议下拉**************************/
    public void AddXL_Void(int xxx)
    {
        switch(xxx)
        {
            case 0://打开选择RTSP协议界面
                if (AddXL != null)
                {
                    AddXL.SetActive(true);
                }
                
                break;
            case 1://选择RTSP协议
                Addint = 0;
                if (AddXL != null)
                {
                    AddXL.SetActive(false);
                }
                break;
            default://默认RTSP协议
                if (AddXL != null)
                {
                    AddXL.SetActive(false);
                }
                Addint = 0;
                break;
        }
        AddText();
    }

    void AddText()
    {
        switch(Addint)
        {
            case 0:
                addSpxyText = "RTSP";
                if (AddInputText != null)
                {
                    AddInputText.text = addSpxyText;
                }
                break;
        }     
    }

    public void ChangeXL_Void(int xxx)
    {
        switch (xxx)
        {
            case 0://打开选择RTSP协议界面
                if (ChangeXL != null)
                {
                    ChangeXL.SetActive(true);
                }
                
                break;
            case 1://选择RTSP协议
                Changeint = 0;
                if (ChangeXL != null)
                {
                    ChangeXL.SetActive(false);
                }
                break;
            default://默认RTSP协议
                Changeint = 0;
                if (ChangeXL != null)
                {
                    ChangeXL.SetActive(false);
                }
                break;
        }
        ChangeText();
    }

    void ChangeText()
    {
        switch (Changeint)
        {
            case 0:
                ChangeSpxyText = "RTSP";
                if (ChangeInputText != null)
                {
                    ChangeInputText.text = ChangeSpxyText;
                }
                break;
        }
    }
    /************************视频协议下拉**************************/
}
