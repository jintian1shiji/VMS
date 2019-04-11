using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cm_Manager : MonoBehaviour {
    //资源管理器
    private ManagerVars vars;
    #region 各种组件
    //监控UI预制体
    private GameObject cm_UI_Pre;
    //监控UI预制体列表
    [HideInInspector]
    public static List<GameObject> cm_Ui_Pres = new List<GameObject>();
    //搜索按钮组件
    private Button seachBtn;
    //搜素输入框组件
    private InputField seachInput;
    //翻页父物体组件
    private Transform pageBtnParent;
    //下一页按钮组件
    private Button nextPageBtn;
    //上一页按钮组件
    private Button prePageBtn;
    //显示页码标识
    private int pageIndex = 0;
    //搜索得到的ID列表
    private List<int> intList = new List<int>();
    //翻页临界标识
    private int typeClick = 0;
    #endregion

    private void Awake()
    {
        #region 获取相关组件及监听
        //监听Tips
        EventCenter.AddListener(EventDefine.ShowCmPanel, Show);
        //移除监听Tips
        EventCenter.AddListener(EventDefine.CloseCmPanel, Close);
        //获取资源管理器组件
        vars = ManagerVars.GetManagerVars();
        //获取监控预制体
        cm_UI_Pre = vars.cm_UI_Pre;
        //获取翻页父组件
        pageBtnParent = transform.Find("bk_Seach/Image_sc");
        //获取搜索组件
        seachBtn = transform.Find("bk_Seach/Image_sc/Button_SC").GetComponent<Button>();
        //监听搜素按钮
        seachBtn.onClick.AddListener(SeachCm);
        //获取搜索输入组件
        seachInput = transform.Find("bk_Seach/Image_sc/InputField").GetComponent<InputField>();
        #endregion

        //初始化
        Init();
    }

    #region 移除监听
    //移除监听
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowCmPanel, Show);
        EventCenter.RemoveListener(EventDefine.CloseCmPanel, Close);
    }
    #endregion

    #region 初始化-显示-关闭
    //初始化不显示
    private void Init()
    {
        gameObject.SetActive(false);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region 显示监控列表
    //显示监控列表
    private void Show()
    {
        typeClick = 0;
        ShowCm_UI_Pres(0, Menu._Instance.cm_Id_List.Count, null);
    }

    /// <summary>
    /// 显示所有设备列表
    /// </summary>
    /// <param name="type">0 普通 1 设备类型查询显示 2 设备编号 3 设备名称 4 有无立杆</param>
    /// <param name="count">列表数量</param>
    /// <param name="intLis">int对象列表</param>
    private void ShowCm_UI_Pres(int type,int count,List<int> intLis)
    {
        pageIndex = 0;
        foreach (GameObject item in cm_Ui_Pres)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        cm_Ui_Pres.Clear();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        int tempCount = 0;
        if (count > 8) tempCount = 8;
        else tempCount = count;

        for (int i = 0; i < tempCount; i++)
        {
            GameObject go = Instantiate(cm_UI_Pre, transform);
            go.transform.position -= new Vector3(0, 50 * i, 0);
            int index = 0;
            if(type == 0)
            {
                index = i;
            }else
            {
                index = intLis[i];
            }
            go.name = "cm" + (index + 1).ToString();
            cm_Ui_Pres.Add(go);
        }

        if (count > 8)
        {
            if (nextPageBtn == null)
            {
                GameObject goBtn = Instantiate(vars.nextPageBtn, pageBtnParent);
                goBtn.transform.GetComponent<RectTransform>().localPosition = new Vector3(645 + 200, 0, 0);
                goBtn.name = "nextPageBtn";
                nextPageBtn = goBtn.transform.GetComponent<Button>();
                nextPageBtn.onClick.AddListener(NextPage);
            }
        }
        else
        {
            if (nextPageBtn != null)
            {
                Destroy(nextPageBtn.gameObject);
            }
        }
        if (prePageBtn != null)
        {
            Destroy(prePageBtn.gameObject);
        }
    }
    #endregion

    #region 翻页操作
    //下一页
    private void NextPage()
    {
        pageIndex++;
        if (typeClick == 0)
        {
            NextPageClick(0, Menu._Instance.cm_Id_List.Count - pageIndex * 8, null);
        }
        else
        {
            NextPageClick(typeClick, intList.Count - pageIndex * 8, intList);
        }
    }

    /// <summary>
    /// 下一页显示所有设备列表
    /// </summary>
    /// <param name="type">0 普通 1 设备类型查询显示 2 设备编号 3 设备名称 4 有无立杆</param>
    /// <param name="count">列表数量</param>
    /// <param name="intLis">int对象列表</param>
    private void NextPageClick(int type, int count, List<int> intLis)
    {
        if(count <= 8 && nextPageBtn != null)
        {
            Destroy(nextPageBtn.gameObject);
        }
        ViewCm_UI_Pre(type, count, intLis);
    }

    //上一页
    private void PrePage()
    {
        pageIndex--;
        if (typeClick == 0)
        {
            PrePageClick(0, Menu._Instance.cm_Id_List.Count - pageIndex * 8, null);
        }
        else
        {
            PrePageClick(typeClick, intList.Count - pageIndex * 8, intList);
        }
    }

    /// <summary>
    /// 上一页显示所有设备列表
    /// </summary>
    /// <param name="type">0 普通 1 设备类型查询显示 2 设备编号 3 设备名称 4 有无立杆</param>
    /// <param name="count">列表数量</param>
    /// <param name="intLis">int对象列表</param>
    private void PrePageClick(int type, int count, List<int> intLis)
    {
        if (pageIndex <= 0 && prePageBtn != null)
        {
            pageIndex = 0;
            Destroy(prePageBtn.gameObject);
        }
        ViewCm_UI_Pre(type, count, intLis);
    }

    /// <summary>
    /// 翻页显示所有设备列表
    /// </summary>
    /// <param name="type">0 普通 1 设备类型查询显示 2 设备编号 3 设备名称 4 有无立杆</param>
    /// <param name="count">列表数量</param>
    /// <param name="intLis">int对象列表</param>
    private void ViewCm_UI_Pre(int type, int count, List<int> intLis)
    {
        foreach (GameObject item in cm_Ui_Pres)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        cm_Ui_Pres.Clear();

        int tempCount = 0;
        if (count > 8) tempCount = 8;
        else tempCount = count;


        for (int i = pageIndex * 8; i < tempCount + pageIndex * 8; i++)
        {
            GameObject go = Instantiate(cm_UI_Pre, transform);
            go.transform.position -= new Vector3(0, 50 * (i - pageIndex * 8), 0);
            int index = 0;
            if (type == 0)
            {
                index = i;
            }
            else
            {
                index = intLis[i];
            }
            go.name = "cm" + (index + 1).ToString();
            cm_Ui_Pres.Add(go);
        }

        if (pageIndex > 0)
        {
            if (prePageBtn == null)
            {
                GameObject goBtn = Instantiate(vars.prePageBtn, pageBtnParent);
                goBtn.transform.GetComponent<RectTransform>().localPosition = new Vector3(645 - 80 + 200, 0, 0);
                goBtn.name = "prePageBtn";
                prePageBtn = goBtn.transform.GetComponent<Button>();
                prePageBtn.onClick.AddListener(PrePage);
            }
        }
        if (count > 8)
        {
            if (nextPageBtn == null)
            {
                GameObject goBtn = Instantiate(vars.nextPageBtn, pageBtnParent);
                goBtn.transform.GetComponent<RectTransform>().localPosition = new Vector3(645 + 200, 0, 0);
                goBtn.name = "nextPageBtn";
                nextPageBtn = goBtn.transform.GetComponent<Button>();
                nextPageBtn.onClick.AddListener(NextPage);
            }
        }
    }
    #endregion

    #region 搜索操作
    //搜索操作
    private void SeachCm()
    {
        string inputText = seachInput.text;
        if(intList.Count > 0)
        {
            intList.Clear();
        }
        switch (inputText)
        {
            case "球机":
                CmTypeSeach(1);
                break;
            case "球":
                CmTypeSeach(1);
                break;
            case "全景球机":
                CmTypeSeach(1);
                break;
            case "枪机":
                CmTypeSeach(0);
                break;
            case "枪":
                CmTypeSeach(0);
                break;
            case "有":
                CmHWSeach(1);
                break;
            case "立杆":
                CmHWSeach(1);
                break;
            case "有立杆":
                CmHWSeach(1);
                break;
            case "无":
                CmHWSeach(0);
                break;
            case "无立杆":
                CmHWSeach(0);
                break;
            default:
                CmTextSeach(inputText);
                break;
        }
    }

    /// <summary>
    /// 设备类型查找
    /// </summary>
    /// <param name="type">0 枪机 1 球机</param>
    private void CmTypeSeach(int type)
    {
        for (int i = 0; i < Menu._Instance.cm_Type_List.Count; i++)
        {
            if (Menu._Instance.cm_Type_List[i] == type)
            {
                intList.Add(i);
            }
        }

        if(intList.Count == 0)
        {
            //TODO 未搜索到
            TipView();
            return;
        }
        typeClick = 1;
        ShowCm_UI_Pres(1, intList.Count, intList);
    }

    /// <summary>
    /// 立杆查询
    /// </summary>
    /// <param name="type">0 无 1 有</param>
    private void CmHWSeach(int type)
    {
        for (int i = 0; i < Menu._Instance.cm_hwType_List.Count; i++)
        {
            if (Menu._Instance.cm_hwType_List[i] == type)
            {
                intList.Add(i);
            }
        }

        if (intList.Count == 0)
        {
            //TODO 未搜索到
            TipView();
            return;
        }
        typeClick = 1;
        ShowCm_UI_Pres(1, intList.Count, intList);
    }

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="str">查询内容</param>
    private void CmTextSeach(string str)
    {
        //模糊名称查询
        for (int i = 0; i < Menu._Instance.cm_Name_List.Count; i++)
        {
            string strTemp = Menu._Instance.cm_Name_List[i];
            if (strTemp == str)
            {
                //print("----等于名称------");
                if (isHaveCm(i) == false)
                {
                    intList.Add(i);
                }
            }
            else
            {
                if(strTemp.Length >= str.Length)
                {
                    
                    if (strTemp.Substring(0, str.Length) == str)
                    {
                        //print("----不等于名称------");
                        if (isHaveCm(i) == false)
                        {
                            intList.Add(i);
                        }
                    }
                    else
                    {
                        isHaveTextUpLength(i, str, strTemp);
                    }
                }
                else
                {
                    isHaveTextDownLength(i, str, strTemp);
                }
            }
        }
        //模糊编号查询
        for (int i = 0; i < Menu._Instance.cm_Id_List.Count; i++)
        {
            string strTemp = Menu._Instance.cm_Id_List[i];
            if (Menu._Instance.cm_Id_List[i] == str)
            {
                //print("----等于编号------");
                if(isHaveCm(i) == false)
                {
                    intList.Add(i);
                }
                
            }
            else
            {
                if(strTemp.Length >= str.Length)
                {
                    if (strTemp.Substring(0, str.Length) == str)
                    {
                        //print("----不等于编号------");
                        if (isHaveCm(i) == false)
                        {
                            intList.Add(i);
                        }
                    }
                    else
                    {
                        isHaveTextUpLength(i, str, strTemp);
                    }
                }
                else
                {
                    isHaveTextDownLength(i, str, strTemp);
                }
            }
        }

        if (intList.Count == 0)
        {
            //TODO 未搜索到
            TipView();
            return;
        }
        typeClick = 1;
        ShowCm_UI_Pres(1, intList.Count, intList);
    }

    //判断是否已经存在
    private bool isHaveCm(int index)
    {
        bool isHave = false;
        foreach (int item in intList)
        {
            if (item == index)
            {
                isHave = true;
                break;
            }
        }
        return isHave;
    }

    //非正常字符查询 搜索字符在被搜素字符长度以内
    private void isHaveTextUpLength(int index,string str,string strTemp)
    {
        for (int j = 0; j < strTemp.Length; j++)
        {
            //print(" =1= " + strTemp.Substring(j, strTemp.Length - j) + "   " + str);
            string subStr = null;
            if (strTemp.Length - j >= str.Length)
            {
                subStr = strTemp.Substring(j, str.Length);
            }
            else
            {
                subStr = strTemp.Substring(j, strTemp.Length - j);
            }
            //print("subStr str" + subStr + "   " + str);
            if (subStr == str)
            {
                //print("----不等于名称------");
                if (isHaveCm(index) == false)
                {
                    intList.Add(index);
                }
            }
        }
    }

    //非正常字符查询 搜索字符在被搜素字符长度以外
    private void isHaveTextDownLength(int index, string str, string strTemp)
    {
        for (int j = 0; j < str.Length; j++)
        {
            string subStr = null;
            if (str.Length - j >= strTemp.Length)
            {
                subStr = str.Substring(j, strTemp.Length);
            }
            else
            {
                subStr = str.Substring(j, str.Length - j);
            }
            //print("subStr str" + subStr + "   " + str);
            if (subStr == strTemp)
            {
                //print("----不等于名称------");
                if (isHaveCm(index) == false)
                {
                    intList.Add(index);
                }
            }
        }
    }
    #endregion

    #region 搜索结果提示
    //搜索结果提示
    private void TipView()
    {
        Menu._Instance.tipsOb.SetActive(true);
        GameObject Txt = Menu._Instance.tipsOb.transform.Find("Text").gameObject;
        string tt = "";
        if(seachInput.text == "")
        {
            tt = "搜索内容不能为空！";
        }
        else
        {
            tt = "抱歉，未找到，请重新输入关键词！";
        }
        Txt.GetComponent<Text>().text = tt;
    }
    #endregion
}
