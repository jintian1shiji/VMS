using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 将此脚本挂载到需要高亮的物体上
/// required package: Highlighting System
/// </summary>
public class HighlightObject : MonoBehaviour
{
    protected Highlighter h;

    public enum HighlightType
    {
        Once,//变化一次
        Constant,//持续
        Flash//闪烁
    }

    public static bool hightbool = false;
    public HighlightType _Hightlighter = HighlightType.Once;

    public Color highlightColor = Color.red;

    void Awake()
    {
        h = this.gameObject.AddComponent<Highlighter>();
        if (Camera.main.GetComponent<HighlightingRenderer>() == null)
        {
            Camera.main.gameObject.AddComponent<HighlightingRenderer>();
        }
    }

    /// <summary>
    /// API:修改物体是否开启高亮
    /// </summary>
    /// <param name="showHightlight">是否开启高亮</param>
    public void SwithHightlight(bool showHightlight)
    {
        SwithHightlight(showHightlight, HighlightType.Once);
    }

    /// <summary>
    /// API:修改物体是否开启高亮
    /// </summary>
    /// <param name="showHightlight">是否开启高亮</param>
    /// <param name="type">高亮类型</param>
    public void SwithHightlight(bool showHightlight, HighlightType type)
    {
        switch (type)
        {
            case HighlightType.Once:
                if (showHightlight)
                    h.ConstantOnImmediate(highlightColor);
                else
                    h.ConstantOffImmediate();
                break;

            case HighlightType.Constant:
                if (showHightlight)
                    h.ConstantOn(highlightColor);
                else
                    h.ConstantOff();
                break;

            case HighlightType.Flash:
                if (showHightlight)
                    h.FlashingOn(highlightColor, new Color(0, 0, 0, 0));
                else
                    h.FlashingOff();
                break;
        }
    }

    /*
    void Update()
    {
       if (!PlayerAllController.DQbool) return;
       if (Menu.isUIopen) return;
        
        //获取鼠标位置  
        Vector3 mPos = Input.mousePosition;
        //向物体发射射线  
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mHit;
        //射线检验  
        if (Physics.Raycast(mRay, out mHit))
        {
            //Cube  
            if (mHit.collider.gameObject.layer == 19)
            {
                SwithHightlight(true);
            }
            else
            {
                SwithHightlight(false);
            }
        }
    }*/
}