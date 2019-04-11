using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//要控制Button旗下的文本Text，因此需要这个头文件！

public class DropDownList : MonoBehaviour {

    private GameObject Candidate;
    public GameObject panelgame;
    public Text textname;

    void Start()//上来先将候选项隐藏  
    {
        Candidate = GameObject.Find("zhengguosong/ButtonAll/CanvasCamera/CanvasSet/PaneALL/DropDownList/Candidate");
        Candidate.SetActive(false);
    }

    //给予默认按钮Button0的点击事件  
    public void Button0_OnClick()
    {
        Candidate.SetActive(!Candidate.activeSelf);//如果候选项是隐藏的，就弄到显示  
        panelgame.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 480);
    }

    //给予其它按钮的点击事件  
    public void Candidate_OnClick(string button_name)//点击按钮的名称将被传入  
    {
        //将被默认按钮Button0的文本替换成用户点击的按钮  
        GameObject.Find("zhengguosong/ButtonAll/CanvasCamera/CanvasSet/PaneALL/DropDownList/Button00/Text").GetComponent<Text>().text =
            GameObject.Find("zhengguosong/ButtonAll/CanvasCamera/CanvasSet/PaneALL/DropDownList/Candidate/" + button_name + "/Text").GetComponent<Text>().text;
        Candidate.SetActive(false);//隐藏候选项  
        panelgame.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 40);
        textname.name = button_name;

        switch (button_name)
        {
            case "Button00":
                Debug.Log(button_name);
                break;
            case "Button01":
                Debug.Log(button_name);
                break;
            case "Button02":
                Debug.Log(button_name);
                break;
            case "Button03":
                Debug.Log(button_name);
                break;
            case "Button04":
                Debug.Log(button_name);
                break;
            case "Button05":
                Debug.Log(button_name);
                break;
            case "Button06":
                Debug.Log(button_name);
                break;
            case "Button07":
                Debug.Log(button_name);
                break;
            case "Button08":
                Debug.Log(button_name);
                break;
            case "Button09":
                Debug.Log(button_name);
                break;
            case "Button10":
                Debug.Log(button_name);
                break;
            default:
                Debug.Log("默认");
                break;
        }
    }
}
