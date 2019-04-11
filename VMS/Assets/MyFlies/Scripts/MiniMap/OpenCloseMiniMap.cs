using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseMiniMap : MonoBehaviour {
    //单例
    public static OpenCloseMiniMap instance;
    #region 小地图组件
    //小地图是否打开
    public static bool Topbool = false;
    //小地图显示rawimage
    private GameObject miniImage;
    //小地图Camera
    private GameObject miniCameraOB;
    //小地图开关按钮
    private Button btnMiniMap;
    #endregion

    void Start () {
        #region 获取小地图相关组件
        miniImage = transform.Find("UI/CanvasTwo/ImageMiniMap").gameObject;
        miniCameraOB = FindObjectOfType<Jump>().transform.Find("Main Camera/CameraMini").gameObject;
        btnMiniMap = transform.Find("UI/CanvasOne/ButtonPublic").GetComponent<Button>();
        btnMiniMap.onClick.AddListener(publicClick);
        instance = this.GetComponent<OpenCloseMiniMap>();
        #endregion
        Topbool = false;
    }

    #region 小地图开关函数
    public void publicClick()
    {
        if (!Topbool)
        {
            miniImage.SetActive(true);
            Topbool = true;
            miniCameraOB.SetActive(true);
            miniCameraOB.SetActive(false);
            Invoke("enebledCameraMini", 0.1f);
        } 
        else
        {
            miniImage.SetActive(false);
            Topbool = false;
            miniCameraOB.SetActive(false);
        }
    }

    void enebledCameraMini()
    {
        miniCameraOB.SetActive(true);
        CancelInvoke("enebledCameraMini");
    }
    #endregion

    #region 小地图复位
    /********************小地图复位********************/
    public void reSetMiniMap()
    {
        miniImage.SetActive(false);
        Topbool = false;
        miniCameraOB.SetActive(false);
    }
    /********************小地图复位********************/
    #endregion
}
