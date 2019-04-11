using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapAPI : MonoBehaviour
{
    //屏幕宽度
    private float screenX = 0;
    //屏幕高度
    private float screenY = 0;

    private void Start()
    {
        //监听显示小地图
        EventCenter.AddListener(EventDefine.ShowMiniMap, Show);
        //监听关闭小地图
        EventCenter.AddListener(EventDefine.CloseMiniMap, Close);
    }

    private void OnDestroy()
    {
        //移除监听显示小地图
        EventCenter.RemoveListener(EventDefine.ShowMiniMap, Show);
        //移除监听关闭小地图
        EventCenter.RemoveListener(EventDefine.CloseMiniMap, Close);
    }

    void Update()
    {
        #region 调整小地图大小
        //判断屏幕大小是否改变
        if (Screen.width != screenX || Screen.height != screenY)
        {
            //更改小地图大小
            transform.GetComponent<RectTransform>().sizeDelta = new Vector3(Screen.width / 4, Screen.height / 4, 0);
            //更改小地图位置
            transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-Screen.width / 8, -Screen.height / 8, 0);
            //获取当前屏幕宽度
            screenX = Screen.width;
            //获取当前屏幕高度
            screenY = Screen.height;
        }
        #endregion
    }

    #region 开关小地图
    //显示小地图
    private void Show()
    {
        gameObject.SetActive(true);
    }
    //关闭小地图
    private void Close()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
