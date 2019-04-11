using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(menuName = "CreateManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{

    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("Manager/ManagerPres");
    }
    //监控UI对象
    public GameObject cm_UI_Pre;
    //上一页按钮对象
    public GameObject prePageBtn;
    //下一页按钮对象
    public GameObject nextPageBtn;
    //遮罩对象
    public GameObject maskPanel;
    //中心图标对象
    public GameObject qua;
    //双击目标点对象
    public GameObject doubleTarget;
    //默认鼠标手
    public Texture2D defaultcursorTexture;
    //默认鼠标
    public Texture2D newcursorTexture;
    //楼层按钮默认背景图
    public Sprite Defaultimage;
    //楼层按钮点击后背景图
    public Sprite NewImage;
    //播放监控按钮背景图
    public Texture bcCmBtn;
    //字体
    public Font font;

    //监控类型对应图片
    public List<Texture2D> cmTypeImageList;
}