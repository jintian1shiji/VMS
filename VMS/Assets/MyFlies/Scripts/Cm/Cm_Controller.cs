using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cm_Controller : MonoBehaviour {
    #region 监控组件
    //双击监控列表定位-按钮
    private Button bk_Btn;
    //双击初始时间
    private float bk_DoubleClick_Start = 0f;
    //双击最大间隔时间
    private float bk_DoubleClick_Last = 1f;
    //双击次数-步数
    private int isDoubleClick = 0;
    //预览按钮
    private Button view_Btn;
    //修改按钮
    private Button change_Btn;
    //删除按钮
    private Button deleta_Btn;
    //序号Text
    private Text txt_XH;
    //设备编号Text
    private Text txt_SBBH;
    //设备名称Text
    private Text txt_SBMC;
    //设备类型Text
    private Text txt_SBLX;
    //立杆Text
    private Text txt_LG;
    //地理坐标Text
    private Text txt_DLZB;
    #endregion

    private void Awake()
    {
        #region 获取各种组件
        //获取双击监控列表定位按钮组件
        bk_Btn = transform.GetComponent<Button>();
        //监听双击监控列表定位按钮组件
        bk_Btn.onClick.AddListener(BkBtnClick);
        //获取预览按钮组件
        view_Btn = transform.Find("ButtonView").gameObject.GetComponent<Button>();
        //监听预览按钮组件
        view_Btn.onClick.AddListener(ViewBtn);
        //获取修改按钮组件
        change_Btn = transform.Find("ButtonChange").gameObject.GetComponent<Button>();
        //监听修改按钮组件
        change_Btn.onClick.AddListener(ChangeBtn);
        //获取删除按钮组件
        deleta_Btn = transform.Find("Buttondelete").gameObject.GetComponent<Button>();
        //监听删除按钮组件
        deleta_Btn.onClick.AddListener(DeletaBtn);
        //获取序号组件
        txt_XH = transform.Find("Text_XH").gameObject.GetComponent<Text>();
        //获取设备编号组件
        txt_SBBH = transform.Find("Text_SBBH").gameObject.GetComponent<Text>();
        //获取设备名称组件
        txt_SBMC = transform.Find("Text_SBMC").gameObject.GetComponent<Text>();
        //获取设备类型组件
        txt_SBLX = transform.Find("Text_SBLX").gameObject.GetComponent<Text>();
        //获取立杆组件
        txt_LG = transform.Find("Text_LG").gameObject.GetComponent<Text>();
        //获取地理坐标组件
        txt_DLZB = transform.Find("Text_DLZB").gameObject.GetComponent<Text>();
        #endregion
    }

    private void Start()
    {
        #region 初始化监控相关数据
        string nmID = gameObject.name.Substring(2, gameObject.name.Length - 2);
        int id = int.Parse(nmID);
        txt_XH.text = nmID;
        txt_SBBH.text = Menu._Instance.cm_Id_List[id - 1];
        txt_SBMC.text = Menu._Instance.cm_Name_List[id - 1];
        string type = "";
        if(Menu._Instance.cm_Type_List[id - 1] == 0)
        {
            type = "枪机";
        }else type = "球机";
        txt_SBLX.text = type;
        if (Menu._Instance.cm_hwType_List[id - 1] == 0)
        {
            type = "无";
        }
        else
        {
            //type = "有";
            string hm = Menu._Instance.hscaleList[id - 1].y.ToString();
            string wm = Menu._Instance.wscaleList[id - 1].y.ToString();
            if (hm.Length > 4)
            {
                hm = hm.Substring(0, 4);
            }
            else addStringLength(hm, 4);
            if (wm.Length > 4)
            {
                wm = wm.Substring(0, 4);
            }
            else addStringLength(wm, 4);
            type = "横：" + wm + "米" + " 竖：" + hm + "米";
        }
            
        txt_LG.text = type;
        Vector3 tPos = Menu._Instance.parentposList[id - 1];
        string x = tPos.x.ToString();
        string y = tPos.y.ToString();
        string z = tPos.z.ToString();
        if(x.Length < 8)
        {
            x = addStringLength(x,8);
        }else if(x.Length > 8)
        {
            x = x.Substring(0, 8);
        }
        if (y.Length < 8)
        {
            y = addStringLength(y, 8);
        }
        else if (y.Length > 8)
        {
            y = y.Substring(0, 8);
        }
        if (z.Length < 8)
        {
            z = addStringLength(z, 8);
        }
        else if (z.Length > 8)
        {
            z = z.Substring(0, 8);
        }
        txt_DLZB.text = "<color=#FF0000>X</color>：" + x + " <color=#00ff00>Y</color>：" + y + " <color=#0066ff>Z</color>：" + z;
        #endregion
    }

    private void Update()
    {
        #region 双击定位操作
        if (isDoubleClick > 0)
        {
            bk_DoubleClick_Start += Time.deltaTime;
            if (bk_DoubleClick_Start <= bk_DoubleClick_Last && isDoubleClick == 2)
            {
                Cm_Location();
                bk_DoubleClick_Start = 0;
                isDoubleClick = 0;
            }
            else if(bk_DoubleClick_Start > bk_DoubleClick_Last)
            {
                bk_DoubleClick_Start = 0;
                isDoubleClick = 0;
            }
            else
            {
                ChangeColor();
            }
        }
        #endregion
    }

    #region 末尾加0操作
    //长度不够补充0
    private string addStringLength(string st,int length)
    {
        for (int i = 0; i < length; i++)
        {
            st += "0";
            if (st.Length == 8)
            {
                break;
            }
        }
        return st;
    }
    #endregion

    #region 预览，修改，删除操作
    //预览
    private void ViewBtn()
    {
        string nmID = txt_SBBH.text.Substring(2, txt_SBBH.text.Length - 2);
        Menu._Instance.cameraId =  int.Parse(nmID);
        EventCenter.Broadcast(EventDefine.CloseCmPanel);
        Menu._Instance.CmConfig(0);
    }
    //修改
    private void ChangeBtn()
    {
        string nmID = gameObject.name.Substring(2, gameObject.name.Length - 2);
        int id = int.Parse(nmID);
        Menu._Instance.c_index = id;
        EventCenter.Broadcast(EventDefine.CloseCmPanel);
        Menu._Instance.CmConfig(2);
    }
    //删除
    private void DeletaBtn()
    {
        string nmID = gameObject.name.Substring(2, gameObject.name.Length - 2);
        int id = int.Parse(nmID);
        Menu._Instance.c_index = id;
        //EventCenter.Broadcast(EventDefine.CloseCmPanel);
        //Menu._Instance.CmConfig(3);
        string idIndex = Menu._Instance.cm_Id_List[id - 1];
        Menu._Instance.DeletaCm(idIndex);
        Destroy(gameObject);
    }
    #endregion

    #region 双击定位操作
    //双击与否
    private void BkBtnClick()
    {
        if (isDoubleClick < 2)
        {
            isDoubleClick++;
        }
        else isDoubleClick = 0;
    }
    //双击后改变列表颜色
    private void ChangeColor()
    {
        foreach (GameObject item in Cm_Manager.cm_Ui_Pres)
        {
            if(item.name == gameObject.name)
            {
                item.GetComponent<Image>().color = new Color(1 / 255f, 1 / 255f, 1 / 255f, 255 / 255f);
            }
            else item.GetComponent<Image>().color = new Color(31 / 255f, 179 / 255f, 249 / 255f, 1 / 255f);
        }
    }
    //双击后定位操作
    private void Cm_Location()
    {
        string nmID = gameObject.name.Substring(2, gameObject.name.Length - 2);
        int id = int.Parse(nmID);
        string cmId = Menu._Instance.cm_Id_List[id - 1];
        GameObject temobD = Menu._Instance.cmABParent.transform.Find("Parent").gameObject;
        Transform[] grandFaD = temobD.GetComponentsInChildren<Transform>();
        foreach (Transform child in grandFaD)
        {
            if (child.name == cmId)
            {
                Camera.main.transform.position = child.position + new Vector3(0, 8, 0);
                Camera.main.transform.LookAt(child.gameObject.transform);
                EventCenter.Broadcast(EventDefine.CloseCmPanel);
                break;
            }
        }
    }
    #endregion
}
