using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Diagnostics;
using RTEditor;

public class Menu : MonoBehaviour
{
    //单例
    public static Menu _Instance;
    //资源管理器
    private ManagerVars vars;
    //是否是IE浏览器打开标识
    private bool isIECmBool = false;

    #region 各种按钮组件
    //退出组件
    private GameObject QuitBack;
    //菜单按钮
    private Button btnMenu;
    //中间菜单定位按钮
    private List<Button> btnDwList = new List<Button>();
    //复位按钮
    private Button reBtn;
    //退出按钮
    private Button quitBtnTrue;
    //取消 退出
    private Button quitBtnFalse;
    //播放 关闭按钮
    private Button closeViewBtn;
    //新增按钮
    private Button addBtn;
    //设备列表界面关闭按钮
    private Button closeCmBtn;
    //增加保存按钮
    private Button addSaveBtn;
    //增加保存取消按钮
    private Button addCloseBtn;
    //修改保存按钮
    private Button changeSaveBtn;
    //修改保存取消按钮
    private Button changeSaveCloseBtn;
    //删除保存按钮
    private Button deleteSaveBtn;
    //删除保存取消按钮
    private Button deleteSaveCloseBtn;
    //主菜单下有config man quit xiala按钮
    private GameObject setMenu;
    //配置：ConfigCamera ViewPanel AddCamera ChangeCamera DeleteCamera
    private List<GameObject> menuConfig = new List<GameObject>();
    //主菜单打开状态
    private bool Setbool = false;
    //配置菜单打开状态
    public static bool configbool = false;
    //下拉状态
    private bool xlbool = false;
    //UI打开状态
    public static bool isUIopen = false;
    //时间text
    private Text timeText;
    //退出按钮
    private Button quitBtn;
    //设备配置按钮
    private Button configBtn;
    //下拉按钮
    private Button xlBtn;
    //Tips按钮
    private Button tipsBtn;
    //新增拖拽图片按钮
    private Button dragBtn;
    //修改界面定位按钮
    private Button dwChangeBtn;
    //删除界面定位按钮
    private Button dwDeleteBtn;
    //新增界面类型下拉按钮
    private Dropdown xlDropdown;
    #endregion

    #region 玩家相机数据记录
    /**************中间菜单*************/
    //玩家身上的相机组件
    private GameObject CameraMenu;
    //玩家
    private GameObject player;
    //玩家初始坐标
    private Vector3 PlayerStartPos;
    //玩家初始朝向
    private Vector3 PlayerStartRot;
    //相机初始坐标
    private Vector3 CameraStartPos;
    //相机初始朝向
    private Vector3 CameraStartRot;
    //相机上坐标列表
    private List<Vector3> CameraTopPosList = new List<Vector3>();
    //相机上朝向列表
    private List<Vector3> CameraTopRotList = new List<Vector3>();
    //相机下坐标列表
    private List<Vector3> CameraButtomPosList = new List<Vector3>();
    //玩家下坐标列表
    private List<Vector3> PlayerButtomPosList = new List<Vector3>();
    //相机下朝向列表
    private List<Vector3> CameraButtomRotList = new List<Vector3>();
    //玩家下朝向列表
    private List<Vector3> PlayerButtomRotList = new List<Vector3>();
    //玩家四元数
    Quaternion mPlayerRot = Quaternion.identity;
    //相机四元素
    Quaternion mCameraRot = Quaternion.identity;
    //欧拉角
    Vector3 eulerAngle = Vector3.zero;

    //中央菜单
    private Vector3 NowPlayerpos;
    private Vector3 NowCamerapos;
    private Vector3 NowPlayerrot;
    private Vector3 NowCamerarot;
    /**************中间菜单*************/

    /**************方向行走*************/
    //方向键移动标识
    private bool movebool = false;
    //方向行走步数
    private int moveint = 0;
    /**************方向行走*************/
    #endregion

    #region 监控相关组件及信息
    /***************新增监控界面****************/
    //设备编号输入框组件
    private InputField sbIdInput;
    //设备名称输入框组件
    private InputField sbNameInput;
    //设备类型组件
    private Dropdown sbTypeDpd;
    //设备立杆组件
    private Dropdown hwTypeDpd;
    //tips组件
    [HideInInspector]
    public GameObject tipsOb;
    //创建监控标识
    private bool isCreateCm = false;
    private int cmType = 0;//监控种类 球机 枪机 其他
    //场景监控父对象
    [HideInInspector]
    public GameObject cmABParent;
    //选择监控的对象
    private GameObject selectCm;
    //存储选择监控对象（需要临时存储）
    private GameObject isDeleteob;

    //创建监控组件图片
    private Image createCm;
    /***************新增监控界面****************/

    /***************修改监控界面****************/
    private Transform transFormCd = null;
    //修改界面设备编号下拉组件
    private Dropdown cgCmDropDownId_change;
    //修改界面设备名称组件
    private InputField cgCmName_change;
    //修改界面设备类型下拉组件
    private Dropdown cgcmDropDownType_change;
    //监控编号列表
    [HideInInspector]
    public List<string> cm_Id_List = new List<string>();
    //监控名称列表
    [HideInInspector]
    public List<string> cm_Name_List = new List<string>();
    //监控类型列表
    [HideInInspector]
    public List<int> cm_Type_List = new List<int>();
    //监控立杆列表
    [HideInInspector]
    public List<int> cm_hwType_List = new List<int>();
    //监控父物体坐标列表
    [HideInInspector]
    public List<Vector3> parentposList = new List<Vector3>();
    //监控父物体朝向列表
    [HideInInspector]
    public List<Vector3> parentrotList = new List<Vector3>();
    //监控父物体大小列表
    [HideInInspector]
    public List<Vector3> parentscaleList = new List<Vector3>();
    //监控坐标列表
    [HideInInspector]
    public List<Vector3> cmposList = new List<Vector3>();
    //监控朝向列表
    [HideInInspector]
    public List<Vector3> cmrotList = new List<Vector3>();
    //监控大小列表
    [HideInInspector]
    public List<Vector3> cmscaleList = new List<Vector3>();
    //监控立杆横坐标列表
    [HideInInspector]
    public List<Vector3> hposList = new List<Vector3>();
    //监控立杆横朝向列表
    [HideInInspector]
    public List<Vector3> hrotList = new List<Vector3>();
    //监控立杆横大小列表
    [HideInInspector]
    public List<Vector3> hscaleList = new List<Vector3>();
    //监控立杆竖坐标列表
    [HideInInspector]
    public List<Vector3> wposList = new List<Vector3>();
    //监控立杆竖朝向列表
    [HideInInspector]
    public List<Vector3> wrotList = new List<Vector3>();
    //监控立杆竖大小列表
    [HideInInspector]
    public List<Vector3> wscaleList = new List<Vector3>();
    //监控几路下标
    [HideInInspector]
    public int c_index = 0;
    /***************修改监控界面****************/

    /***************删除监控界面****************/
    //删除界面设备编号下拉组件
    private Dropdown cgCmDropDownId_delete;
    //删除界面设备名称组件
    private InputField cgCmName_delete;
    //删除界面设备类型下拉组件
    private Dropdown cgcmDropDownType_delete;
    /***************删除监控界面****************/
    #endregion

    #region 数据库
    /***************数据库操作****************/
    //数据库地址
    private string cmUrl = "";
    /***************数据库操作****************/
    #endregion

    #region 外部EXE平台
    /***************外部海康EXE平台****************/
    //EXE地址
    private string exePath;
    //EXE开关
    private bool isHKEXEbool = false;
    /***************外部海康EXE平台****************/
    #endregion

    #region 场景中监控视频相关
    /***************场景中打开视频****************/
    //第几路监控
    [HideInInspector]
    public int cameraId = 0;
    //播放按钮背景
    private Texture bcCmBtn;
    //是否单选了监控对象
    private bool isSelectCm = false;
    //几路监控播放按钮初始点击位置
    private Vector2 startBtnPos;
    //字体
    private Font font;
    //记类框选的录像ID
    private List<int> cmListJs = new List<int>();
    //是否框选标识
    private bool isSelectBox = false;
    //框选开始标识
    private bool isSelectBoxMove = true;
    /***************场景中打开视频****************/
    #endregion


    // Use this for initialization
    private void Awake()
    {
        _Instance = GetComponent<Menu>();
        vars = ManagerVars.GetManagerVars();

        #region 初始化，获取各种组件及判断是否打开外部EXE
        //玩家数据函数
        ClearList();
        //获取组件函数
        GetConponent_Menu();

        //exePath = "@" + GetJson.hkPath + GetJson.hkName + ".exe";
        exePath = GetJson.hkPath + GetJson.hkName + ".exe";
        if (GetJson.hkBool == "true" || GetJson.hkBool == "True")
        {
            isHKEXEbool = true;
        }
        else isHKEXEbool = false;
        #endregion
    }

    #region 获取各种组件
    //获取各种组件
    void GetConponent_Menu()
    {
        //获取UI组件
        GameObject uiOb = FindObjectOfType<OpenCloseMiniMap>().transform.Find("UI").gameObject;
        //获取设置菜单
        setMenu = uiOb.transform.Find("CanvasOne/NewAllButton/SetMenu").gameObject;
        //获取菜单按钮组件
        btnMenu = uiOb.transform.Find("CanvasOne/NewAllButton/ImageMenuBack/ButtonMenu").GetComponent<Button>();
        //监听菜单按钮
        btnMenu.onClick.AddListener(MenuClick);

        //获取配置组件
        for (int i = 0; i < 5; i++)
        {
            string pt = null;
            string btnMid = null;
            switch (i)
            {
                case 0:
                    pt = "ConfigCamera";
                    btnMid = "ButtonBGQ";
                    break;
                case 1:
                    pt = "ViewPanel";
                    btnMid = "ButtonJQ";
                    break;
                case 2:
                    pt = "CameraController/AddCamera";
                    btnMid = "ButtonYDC";
                    break;
                case 3:
                    pt = "CameraController/ChangeCamera";
                    btnMid = "ButtonJLS";
                    break;
                case 4:
                    pt = "CameraController/DeleteCamera";
                    btnMid = "ButtonDoor";
                    break;
            }
            if(pt != null)
                menuConfig.Add(uiOb.transform.Find("CanvasOne/" + pt).gameObject);
            if (btnMid != null)
            {
                Button btnM = uiOb.transform.Find("CanvasOne/MidMenu/MidMenu/" + btnMid).GetComponent<Button>();
                btnDwList.Add(btnM);
                btnM.onClick.AddListener(MidMenu);
            }
        }
        //获取复位按钮组件
        reBtn = uiOb.transform.Find("CanvasOne/Direction/ButtonFW").GetComponent<Button>();
        //监听复位按钮
        reBtn.onClick.AddListener(RestPostion);
        //获取退出按钮组件
        quitBtnTrue = uiOb.transform.Find("CanvasOne/QuitGame/Imageback/ButtonTrue").GetComponent<Button>();
        //获取取消 退出按钮组件
        quitBtnFalse = uiOb.transform.Find("CanvasOne/QuitGame/Imageback/ButtonFalse").GetComponent<Button>();
        //监听退出按钮
        quitBtnTrue.onClick.AddListener(QuitGameGo);
        //监听退出取消按钮
        quitBtnFalse.onClick.AddListener(QuitGameGo);

        //获取预览关闭按钮组件
        closeViewBtn = uiOb.transform.Find("CanvasOne/ViewPanel/ImageBack/ButtonViewClose").GetComponent<Button>();
        //监听预览关闭按钮
        closeViewBtn.onClick.AddListener(ViewClose);

        //获取新增按钮组件
        addBtn = uiOb.transform.Find("CanvasOne/ConfigCamera/ImageBack/bk_Seach/Image_sc/ButtonAdd").GetComponent<Button>();
        //监听新增按钮
        addBtn.onClick.AddListener(AddBtnCm);

        //获取监控列表关闭按钮组件
        closeCmBtn = uiOb.transform.Find("CanvasOne/ConfigCamera/ImageBack/ButtonClose").GetComponent<Button>();
        //监听监控列表关闭按钮
        closeCmBtn.onClick.AddListener(CmListClose);

        //获取增加保存按钮组件
        addSaveBtn = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ButtonSave").GetComponent<Button>();
        //获取增加保存取消按钮组件
        addCloseBtn = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageDrag/ButtonX").GetComponent<Button>();
        //监听增加保存按钮
        addSaveBtn.onClick.AddListener(AddSaveBtn);
        //监听增加保存取消按钮
        addCloseBtn.onClick.AddListener(AddSaveCloseBtn);

        //获取修改保存按钮组件
        changeSaveBtn = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/ImageBack/ButtonSave").GetComponent<Button>();
        //获取修改保存取消按钮组件
        changeSaveCloseBtn = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/ImageBack/ImageDrag/ButtonX").GetComponent<Button>();
        //监听修改保存按钮
        changeSaveBtn.onClick.AddListener(ChangeSaveBtn);
        //监听修改保存取消按钮
        changeSaveCloseBtn.onClick.AddListener(ChangeSaveCloseBtn);

        //获取修改保存按钮组件
        deleteSaveBtn = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/ImageBack/ButtonDelete").GetComponent<Button>();
        //获取修改保存取消按钮组件
        deleteSaveCloseBtn = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/ImageBack/ImageDrag/ButtonX").GetComponent<Button>();
        //监听修改保存按钮
        deleteSaveBtn.onClick.AddListener(DeleteSaveBtn);
        //监听修改保存取消按钮
        deleteSaveCloseBtn.onClick.AddListener(DeleteSaveCloseBtn);
        //获取退出按钮组件
        quitBtn = uiOb.transform.Find("CanvasOne/NewAllButton/SetMenu/ImageBack/ButtonQuit").GetComponent<Button>();
        //监听退出按钮
        quitBtn.onClick.AddListener(QuitBtn);
        //获取设备配置按钮组件
        configBtn = uiOb.transform.Find("CanvasOne/NewAllButton/SetMenu/ImageBack/ButtonConfig").GetComponent<Button>();
        //监听设备配置按钮
        configBtn.onClick.AddListener(ConfigBtn);
        //获取下拉按钮组件
        configBtn = uiOb.transform.Find("CanvasOne/NewAllButton/SetMenu/ImageBack/ButtonDwon").GetComponent<Button>();
        //监听下拉按钮
        configBtn.onClick.AddListener(XlBtn);
        //获取Tips按钮组件
        tipsBtn = uiOb.transform.Find("CanvasTwo/ImageTips/ButtonTrue").GetComponent<Button>();
        //监听Tips按钮
        tipsBtn.onClick.AddListener(CloseTips);
        //获取新增类型下拉按钮组件
        xlDropdown = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageSBLX/Dropdown").GetComponent<Dropdown>();
        //监听新增类型下拉按钮
        xlDropdown.onValueChanged.AddListener((int value) => OnDropDownChange());


        //获取拖拽按钮组件
        dragBtn = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/Image/ImageCreate").GetComponent<Button>();
        //监听拖拽按钮
        UIEventListener btnListener = dragBtn.gameObject.AddComponent<UIEventListener>();
        btnListener.OnMouseDown += delegate (GameObject gb) {
            OnBeginDrag();
        };

        btnListener.OnMouseUp += delegate (GameObject gb) {
            OnEndDrag();
        };

        btnListener.OnMouseDrag += delegate (GameObject gb) {
            OnDrag();
        };

        //获取修改界面定位按钮组件
        dwChangeBtn = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/Image/ImageDW").GetComponent<Button>();
        //监听修改界面定位按钮
        dwChangeBtn.onClick.AddListener(DwChangeBtn);
        //获取删除界面定位按钮组件
        dwDeleteBtn = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/Image/ImageDW").GetComponent<Button>();
        //监听删除界面定位按钮
        dwDeleteBtn.onClick.AddListener(DwDeleteBtn);


        //获取时间text组件
        timeText = uiOb.transform.Find("CanvasOne/ImageLogo/Time").GetComponent<Text>();
        //获取场景玩家
        player = FindObjectOfType<Jump>().gameObject;
        //获取玩家身上的相机组件
        CameraMenu = player.transform.Find("Main Camera").gameObject;
        
        //获取设备编号输入框组件
        sbIdInput = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageSBBH/InputField").GetComponent<InputField>();
        //获取设备名称输入框组件
        sbNameInput = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageSBMC/InputField").GetComponent<InputField>();
        //获取设备类型组件
        sbTypeDpd = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageSBLX/Dropdown").GetComponent<Dropdown>();
        //获取设备立杆组件
        hwTypeDpd = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/ImageHW/Dropdown").GetComponent<Dropdown>();
        //获取tips组件
        tipsOb = uiOb.transform.Find("CanvasTwo/ImageTips").gameObject;
        //获取场景监控父对象
        cmABParent = FindObjectOfType<AssetBundleParent>().transform.Find("cameraParent").gameObject;
        //获取创建场景监控按钮图片
        createCm = uiOb.transform.Find("CanvasOne/CameraController/AddCamera/ImageBack/Image/ImageCreate").GetComponent<Image>();
        //获取修改界面设备编号下拉组件
        cgCmDropDownId_change = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/ImageBack/ImageSBBH/DropdownID").GetComponent<Dropdown>();
        //获取修改界面设备名称组件
        cgCmName_change = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/ImageBack/ImageSBMC/InputField").GetComponent<InputField>();
        //获取修改界面设备类型下拉组件
        cgcmDropDownType_change = uiOb.transform.Find("CanvasOne/CameraController/ChangeCamera/ImageBack/ImageSBLX/DropdownType").GetComponent<Dropdown>();
        //获取删除界面设备编号下来组件
        cgCmDropDownId_delete = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/ImageBack/ImageSBBH/DropdownID").GetComponent<Dropdown>();
        //获取删除界面设备名称组件
        cgCmName_delete = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/ImageBack/ImageSBMC/InputField").GetComponent<InputField>();
        //获取删除界面设备类型下拉组件
        cgcmDropDownType_delete = uiOb.transform.Find("CanvasOne/CameraController/DeleteCamera/ImageBack/ImageSBLX/DropdownType").GetComponent<Dropdown>();
        //获取播放组件
        bcCmBtn = vars.bcCmBtn;
        //字体
        font = vars.font;
        //获取退出组件
        QuitBack = uiOb.transform.Find("CanvasOne/QuitGame").gameObject;

    }
    #endregion

    #region 玩家，相机 初始化及记类
    //玩家 相机 初始坐标和朝向
    void ClearList()
    {
        //玩家初始位置
        PlayerStartPos = new Vector3(0.23f, 2, -70);
        //玩家初始朝向
        PlayerStartRot = new Vector3(0, 0, 0);
        //相机初始坐标
        CameraStartPos = new Vector3(0.23f, 152, -70);
        //相机初始朝向
        CameraStartRot = new Vector3(60, 0, 0);

        CameraTopPosList.Clear();
        CameraTopRotList.Clear();
        PlayerButtomPosList.Clear();
        PlayerButtomRotList.Clear();
        CameraButtomPosList.Clear();
        CameraButtomRotList.Clear();

        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    CameraTopPosList.Add(new Vector3(22, 60, 42));
                    PlayerButtomPosList.Add(new Vector3(0, 2, -10));
                    break;
                case 1:
                    CameraTopPosList.Add(new Vector3(36, 78, 75));
                    PlayerButtomPosList.Add(new Vector3(75, 2, -10));
                    break;
                case 2:
                    CameraTopPosList.Add(new Vector3(78, 38, 17));
                    PlayerButtomPosList.Add(new Vector3(70, 2, -50));
                    break;
                case 3:
                    CameraTopPosList.Add(new Vector3(-75, 31, 112));
                    PlayerButtomPosList.Add(new Vector3(-73, 2, 45));
                    break;
                case 4:
                    CameraTopPosList.Add(new Vector3(-1.5f, 18.8f, 0.5f));
                    PlayerButtomPosList.Add(new Vector3(0.3f, 2, -80));
                    break;
            }
            CameraTopRotList.Add(new Vector3(60, 0, 0));
            CameraButtomPosList.Add(new Vector3(0, 0, 0));
            CameraButtomRotList.Add(new Vector3(0, 0, 0));
            PlayerButtomRotList.Add(new Vector3(0, 0, 0));
        }
    }
    #endregion

    void Start()
    {
        #region 初始化监控数据库地址
        if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
        {
            cmUrl = Application.streamingAssetsPath + "/php/" + "check.php";
        }
        else
        {
            cmUrl = GetJson.ipStr +  "/php/check.php";
        }
        StartCoroutine(show_cm(-1));
        #endregion
    }


    void Update()
    {
        #region 帧操作
        //时间获取
        if (PlayerAllController.DQbool)
        {
            timeText.text = string.Format("{0:T}", DateTime.Now);
        }
        //下拉菜单
        if (xlbool)
        {
            XLMove();
        }
        #endregion

        #region 方向键操作
        /**************方向行走*************/
        if (Input.GetMouseButtonUp(0))
        {
            movebool = false;
            moveint = 0;
        }
        if (movebool)
        {
            MoveWASD();
        }
       
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Input.mousePosition;
                //Debug.Log(pos);
                if (pos.x > Screen.width - 128 && pos.x < Screen.width - 64 && pos.y > 128 && pos.y < 193)
                {
                    movebool = true;
                    moveint = 1;
                }

                if (pos.x > Screen.width - 128 && pos.x < Screen.width - 64 && pos.y > 0 && pos.y < 64)
                {
                    movebool = true;
                    moveint = 2;
                }

                if (pos.x > Screen.width - 193 && pos.x < Screen.width - 128 && pos.y > 64 && pos.y < 128)
                {
                    movebool = true;
                    moveint = 3;
                }

                if (pos.x > Screen.width - 64 && pos.x < Screen.width && pos.y > 64 && pos.y < 128)
                {
                    movebool = true;
                    moveint = 4;
                }
            }

        }
        /**************方向行走*************/
        #endregion

        #region 场景点击监控播放相关
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0) && !isSelectCm)
            {
                OpenCmScene();
            }
            if(Input.GetMouseButtonDown(2) && (isSelectCm || isSelectBox))
            {
                CloseCmScene();
            }
            if (Input.GetMouseButtonUp(0) && isSelectBox)
            {
                isSelectBoxMove = false;
            }
        }
        #endregion
    }

    #region 主设置菜单开关函数
    //打开主设置菜单
    private void MenuClick()
    {
        if (!Setbool)
        {
            setMenu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
            setMenu.SetActive(true);
            Setbool = true;
        }
        else
        {
            setMenu.SetActive(false);
            Setbool = false;
            configbool = false;
        }
    }
    #endregion

    #region 主设置菜单功能函数
    //退出监听函数
    private void QuitBtn()
    {
        OneMenu(0);
    }
    //设备配置监听函数
    private void ConfigBtn()
    {
        OneMenu(1);
    }
    //下拉监听函数
    private void XlBtn()
    {
        OneMenu(3);
    }

    //主菜单按钮函数
    public void OneMenu(int dex)
    {
        switch (dex)
        {
            case 0://退出游戏
                QuitBack.SetActive(true);
                break;
            case 1://设置
                if (!configbool)
                {
                    isUIopen = true;
                    if (menuConfig[0].activeSelf)
                    {
                        menuConfig[0].SetActive(false);
                    }
                    else menuConfig[0].SetActive(true);
                    EventCenter.Broadcast(EventDefine.ShowCmPanel);
                }
                else
                {
                    foreach (GameObject element in menuConfig)
                    {
                        element.SetActive(false);
                    }
                    configbool = false;
                    if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
                    {
                        if (isIECmBool)
                        {
                            //-1 关闭 0 UI播放打开界面 >0 3D场景点击播放
                            Application.ExternalCall("OpenCm", -1);
                            isIECmBool = false;
                        }
                    }
                    else
                    {
                        //menuConfig[1].SetActive(false);
                        KillProcess(GetJson.hkName);
                    }
                    isUIopen = false;
                }
                break;
            case 2://个人
                break;
            case 3://下拉
                xlbool = true;
                break;
            default:
                break;
        }
    }
    
    public void QuitGameGo()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if (obj.name == "ButtonTrue")
        {
            KillProcess(GetJson.hkName);
            if (TestMedia_FFmpeg.Instance == null)
            {
                Application.Quit();
            }
            else TestMedia_FFmpeg.Instance.OnDestroy();
            //Application.Quit();
        }
        else QuitBack.SetActive(false);
    }
    #endregion

    #region 中央菜单定位按钮函数
    //中央菜单定位按钮函数
    private void MidMenu()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        int ddd = 0;
        switch (obj.name)
        {
            case "ButtonBGQ":
                ddd = 0;
                break;
            case "ButtonJQ":
                ddd = 1;
                break;
            case "ButtonYDC":
                ddd = 2;
                break;
            case "ButtonJLS":
                ddd = 3;
                break;
            case "ButtonDoor":
                ddd = 4;
                break;
        }

        if (PlayerAllController.mviewbool)
        {
            NowPlayerpos = PlayerButtomPosList[ddd];
            NowPlayerrot = PlayerButtomRotList[ddd];
            NowCamerapos = CameraButtomPosList[ddd];
            NowCamerarot = CameraButtomRotList[ddd];
        }
        else
        {
            NowPlayerpos = PlayerStartPos;
            NowPlayerrot = PlayerStartRot;
            NowCamerapos = CameraTopPosList[ddd];
            NowCamerarot = CameraTopRotList[ddd];
        }
        player.transform.localPosition = NowPlayerpos;
        CameraMenu.transform.localPosition = NowCamerapos;

        mPlayerRot.eulerAngles = NowPlayerrot;
        mCameraRot.eulerAngles = NowCamerarot;
        player.transform.localRotation = mPlayerRot;
        CameraMenu.transform.localRotation = mCameraRot;
    }
    #endregion

    #region 复位按钮操作函数
    //复位按键
    public void RestPostion()
    {
        PlayerAllController.mviewbool = false;
        if (configbool)
        {
            if (TestMedia_FFmpeg.Instance.m_realHanle != -1)
            {
                TestMedia_FFmpeg.Instance.StopRealPlay();

            }
        }
        //SceneManager.LoadScene(2);
        PlayerAllController.instance.reSetPlayer();
        reSetUI();
        ClickOpen.instance.reSetFloorUI();
        FloorAll.instance.reSetFloor();
        OpenCloseMiniMap.instance.reSetMiniMap();

    }

    /***************UI复位****************/
    void reSetUI()
    {
        KillProcess(GetJson.hkName);
        setMenu.SetActive(false);
        setMenu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
        Setbool = false;
        xlbool = false;

        QuitBack.SetActive(false);

        setMenu.SetActive(false);

        foreach (GameObject el in menuConfig)
        {
            el.SetActive(false);
        }

        movebool = false;
        moveint = 0;
        Setbool = false;
        configbool = false; //一级config打开状态
        isUIopen = false;
        xlbool = false;//下拉状态
    }
    /***************UI复位****************/
    #endregion

    #region 方向键操作
    //方向行走
    void MoveWASD()
    {
        switch (moveint)
        {
            case 0:
                break;
            case 1:
                if (PlayerAllController.mviewbool)
                {
                    PlayerAllController.controller.SimpleMove(transform.forward * 1);
                }
                else
                {

                    Camera.main.transform.Translate(Vector3.down * (-1));
                }
                break;
            case 2:
                if (PlayerAllController.mviewbool)
                {
                    PlayerAllController.controller.SimpleMove(transform.forward * (-1));
                }
                else
                {

                    Camera.main.transform.Translate(Vector3.down * 1);
                }
                break;
            case 3:
                if (PlayerAllController.mviewbool)
                {
                    player.transform.Rotate(Vector3.up, -1);
                }
                else
                {
                    Camera.main.transform.Translate(Vector3.left * 1);
                }
                break;
            case 4:
                if (PlayerAllController.mviewbool)
                {
                    player.transform.Rotate(Vector3.up, 1);
                }
                else
                {
                    Camera.main.transform.Translate(Vector3.left * (-1));
                }
                break;
            default: break;
        }
    }
    #endregion

    #region 下拉菜单操作
    //下拉菜单移动
    void XLMove()
    {
        setMenu.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0, -50, 0);
        if (setMenu.GetComponent<RectTransform>().anchoredPosition3D.y <= -400)
        {
            setMenu.SetActive(false);
            setMenu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
            Setbool = false;
            xlbool = false;
        }
    }
    #endregion

    #region 监控配置界面操作
    /***************监控配置界面****************/
    //预览关闭按钮函数
    private void ViewClose()
    {
        CmConfig(0);
    }
    //新增按钮函数
    private void AddBtnCm()
    {
        CmConfig(1);
    }
    //监控列表关闭按钮函数
    private void CmListClose()
    {
        OneMenu(1);
    }
    //新增保存按钮函数
    private void AddSaveBtn()
    {
        AddCm(2);
    }

    //新增保存取消按钮函数
    private void AddSaveCloseBtn()
    {
        AddCm(1);
    }

    //修改保存按钮函数
    private void ChangeSaveBtn()
    {
        changgeCm(1);
    }

    //修改保存取消按钮函数
    private void ChangeSaveCloseBtn()
    {
        changgeCm(0);
    }

    //修改定位按钮函数
    private void DwChangeBtn()
    {
        changgeCm(2);
    }

    //删除保存按钮函数
    private void DeleteSaveBtn()
    {
        deleteCm(1);
    }

    //删除保存取消按钮函数
    private void DeleteSaveCloseBtn()
    {
        deleteCm(0);
    }

    //删除定位按钮函数
    private void DwDeleteBtn()
    {
        deleteCm(2);
    }

    //监控函数
    public void CmConfig(int index)
    {
        int n = 0;
        switch (index)
        {
            case 0://打开预览界面
                if (!configbool)
                {
                    configbool = true;
                    isUIopen = true;
                    if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
                    {
                        if (!isIECmBool)
                        {
                            foreach (GameObject element in menuConfig)
                            {
                                if (element.activeSelf)
                                {
                                    element.SetActive(false);
                                }
                            }
                            //-1 关闭 0 UI播放打开界面 >0 3D场景点击播放
                            //Application.ExternalCall("OpenCm", 0);
                            if (isSelectCm)
                            {
                                Application.ExternalCall("OpenCm", cameraId);
                                isSelectCm = false;
                            }
                            else if (isSelectBox)
                            {
                                if(cmListJs.Count == 1)
                                {
                                    Application.ExternalCall("OpenCm", cmListJs[0]);
                                }
                                else
                                {
                                    Application.ExternalCall("OpenCmList", cmListJs);
                                }
                                isSelectBox = false;
                                cmListJs.Clear();
                            }else
                            {
                                //UI打开
                                Application.ExternalCall("OpenCm", cameraId);
                            }
                            cameraId = 0;
                            isIECmBool = true;
                        }
                    }
                    else
                    { 
                        if(!isHKEXEbool)
                        {
                            foreach (GameObject element in menuConfig)
                            {
                                if (n == 1)
                                {
                                    element.SetActive(true);
                                }
                                else element.SetActive(false);

                                n++;
                            }
                            if (isSelectBox)
                            {
                                cameraId = cmListJs[0];
                                isSelectBox = false;
                                cmListJs.Clear();
                            }
                            isSelectCm = false;
                            Invoke("StartOpenView", 1f);
                        }
                        else
                        {
                            StartCoroutine(ShowNewWindow());
                        }
                    }
                }
                else
                {
                    foreach (GameObject element in menuConfig)
                    {
                        element.SetActive(false);
                    }
                    configbool = false;
                    isUIopen = false;
                    if (GetJson.platform != "Editor" && GetJson.platform != "Windows")
                    {
                        if (isIECmBool)
                        {
                            //-1 关闭 0 UI播放打开界面 >0 3D场景点击播放
                            Application.ExternalCall("OpenCm", -1);
                            isIECmBool = false;
                        }
                    }
                    else
                    {
                        //menuConfig[1].SetActive(false);
                    }
                }
                break;
            case 1://增加场景监控模型
                isUIopen = true;
                foreach (GameObject element in menuConfig)
                {
                    if (n == 2)
                    {
                        element.SetActive(true);
                        Texture2D t2 = vars.cmTypeImageList[0];
                        Sprite sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(1f, 1f));
                        createCm.sprite = sprite;
                    }
                    else element.SetActive(false);

                    n++;
                }
                break;
            case 2://修改场景监控模型
                isUIopen = true;
                foreach (GameObject element in menuConfig)
                {
                    if (n == 3)
                    {
                        element.SetActive(true);
                    }
                    else element.SetActive(false);

                    n++;
                }
                StartCoroutine(show_cm(0));
                break;
            case 3://删除场景监控模型
                isUIopen = true;
                foreach (GameObject element in menuConfig)
                {
                    if (n == 4)
                    {
                        element.SetActive(true);
                    }
                    else element.SetActive(false);

                    n++;
                }
                StartCoroutine(show_cm(1));
                break;
        }
    }
    /***************监控配置界面****************/
    #endregion

    #region 新增监控界面操作
    /***************新增监控界面****************/
    public void CloseTips()
    {
        tipsOb.SetActive(false);
    }

    public void AddCm(int index)
    {
        switch (index)
        {
            case 0://创建camera
                if (isDeleteob != null)
                {
                    Destroy(isDeleteob);
                }
                break;
            case 1://关闭
                if (isDeleteob != null)
                {
                    //Destroy(isDeleteob);
                    isDeleteob.SetActive(false);
                }  
                foreach (GameObject element in menuConfig)
                {
                    element.SetActive(false);
                }
                isUIopen = false;
                break;
            case 2://保存
                if (sbIdInput.text == string.Empty || sbNameInput.text == string.Empty)
                {
                    tipsOb.SetActive(true);
                    GameObject Txt = tipsOb.transform.Find("Text").gameObject;
                    Txt.GetComponent<Text>().text = "设备编号和设备名称不能为空！";
                }
                else
                {
                    if (isDeleteob != null)
                    {
                        int typedp = sbTypeDpd.value;
                        int hwtypedp = hwTypeDpd.value;
                        CmDate(0, isDeleteob, sbIdInput.text, sbNameInput.text, typedp, hwtypedp);

                        isDeleteob = null;

                    }
                    else
                    {
                        tipsOb.SetActive(true);
                        GameObject Txt = tipsOb.transform.Find("Text").gameObject;
                        Txt.GetComponent<Text>().text = "您还未拖放任何监控！";
                    }
                }
                break;
        }
    }

    //新增监控界面更换类型显示类型监控图片操作
    public void OnDropDownChange()
    {
        int d = sbTypeDpd.value;
        Texture2D t2 = vars.cmTypeImageList[d];
        Sprite sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(1f, 1f));
        createCm.sprite = sprite;
    }

    //结束新增拖拽监控操作
    public void OnEndDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            selectCm.transform.Translate(-Vector3.up * 0.008f * selectCm.transform.localScale.y, Space.Self);
        }

        isDeleteob = selectCm;
        //selectCm.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (selectCm.transform.Find("hw") != null)
        {
            selectCm.transform.Find("hw").localEulerAngles = new Vector3(0, 0, 0);
        }
        selectCm = null;
        isCreateCm = false;
    }

    //拖拽新增监控操作
    public void OnDrag()
    {
        if (selectCm != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.name);
                if (hit.transform.name == "cm")
                    return;
                //吸附功能
                selectCm.transform.position = hit.point;
                selectCm.transform.up = hit.normal;
                selectCm.transform.Translate(Vector3.up * 0.05f * selectCm.transform.localScale.y, Space.Self);
                //吸附功能
            }
            else
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectCm.transform.position = pos;
            }
        }
    }

    //开始新增监控拖拽操作
    public void OnBeginDrag()
    {
        StartCoroutine(InstantiateCm("camera", 0));
    }
    #endregion

    #region 实例化监控
    //实例化监控操作
    public IEnumerator InstantiateCm(string name, int index)
    {
        string url = null;

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            url = Application.streamingAssetsPath + "/AssetBundles/PC/" + name + "/" + name + "ab";
            //Debug.Log("运行平台为：windows" + "   " + url);
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            url = Application.streamingAssetsPath + "/AssetBundles/Web/" + name + "/" + name + "ab";
            //Debug.Log("运行平台为：webgl");
        }
        //Debug.Log("url ---------------- " + url);
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

            int obNum = 0;
            int ii = 0;
            switch (index)
            {
                case 0:
                    obNum = 1;
                    break;
                case 1:
                    obNum = cm_Id_List.Count;
                    break;
            }
            //print(" obNum ==== " + obNum + " index ==== " + index);
            AssetBundleRequest rq;
            string nm;
            for (int i = 1; i <= obNum; i++)
            {
                if (index == 0)
                {
                    ii = sbTypeDpd.value;
                    nm = hwTypeDpd.value.ToString();
                }
                else
                {
                    ii = cm_Type_List[i - 1];
                    nm = cm_hwType_List[i - 1].ToString();
                }
                //print("nmName ====== " + name + ii + nm);
                //异步加载对象
                rq = ab.LoadAssetAsync(name + ii + nm, typeof(GameObject)); //指定了类型为GameObject  
                //等待异步完成
                yield return rq;
                //Debug.Log("异步加载完成");
                //获得加载对象的引用,这个时候是在内存中
                GameObject obj = rq.asset as GameObject;
                InstanceVoidCm(obj, name, index, i - 1);
            }
            //从内存卸载AssetBundle
            ab.Unload(false);
        }
    }

    //实例化监控后属性相关操作
    public void InstanceVoidCm(GameObject obj, string aName, int index, int num)
    {
        GameObject ob = Instantiate(obj);
        obj.tag = "Untagged";
        obj = ob;
        GameObject temob = cmABParent.transform.Find("Parent").gameObject;
        obj.transform.SetParent(temob.transform);
        switch (index)
        {
            case 0://增加监控
                isCreateCm = true;
                if (aName == "camera")
                {
                    Transform[] grandFa = obj.GetComponentsInChildren<Transform>();
                    foreach (Transform child in grandFa)
                    {
                        child.gameObject.layer = 8;
                    }
                    obj.layer = 8;
                    selectCm = obj;
                }
                break;
            case 1://初始化监控
                if (aName == "camera")
                {
                    Transform[] grandFa = obj.GetComponentsInChildren<Transform>();
                    obj.layer = 0;
                    foreach (Transform child in grandFa)
                    {
                        child.gameObject.layer = 0;
                    }

                    obj.name = cm_Id_List[num];

                    obj.transform.localPosition = parentposList[num];
                    obj.transform.localEulerAngles = parentrotList[num];
                    obj.transform.localScale = parentscaleList[num];

                    Transform goCm = obj.transform.Find("cm");
                    goCm.localPosition = cmposList[num];
                    goCm.localEulerAngles = cmrotList[num];
                    goCm.localScale = cmscaleList[num];

                    if (cm_hwType_List[num] == 1)
                    {
                        Transform goHW = obj.transform.Find("hw");
                        Transform goH = goHW.Find("h");
                        Transform goW = goHW.Find("w");

                        goHW.localEulerAngles = new Vector3(0, 0, 0);

                        goH.localPosition = hposList[num];
                        goH.localEulerAngles = hrotList[num];
                        goH.localScale = hscaleList[num];

                        goW.localPosition = wposList[num];
                        goW.localEulerAngles = wrotList[num];
                        goW.localScale = wscaleList[num];
                    }
                }
                break;
            case 2:
                break;
        }
    }
    #endregion

    #region 修改监控操作
    /***************修改监控界面****************/
    public void changgeCm(int index)
    {
        if (transFormCd != null)
        {
            foreach (Transform child in transFormCd.gameObject.GetComponentInChildren<Transform>())
            {
                child.gameObject.layer = 0;
            }
            transFormCd = null;
        }
        switch (index)
        {
            case 0://关闭
                foreach (GameObject element in menuConfig)
                {
                    element.SetActive(false);
                }
                isUIopen = false;
                break;
            case 1://保存
                string cId = cm_Id_List[cgCmDropDownId_change.value];
                string cName = cgCmName_change.text;
                int cType = cm_Type_List[cgCmDropDownId_change.value];
                int hwType = cm_hwType_List[cgCmDropDownId_change.value];

                GameObject temob = cmABParent.transform.Find("Parent").gameObject;
                Transform[] grandFa = temob.GetComponentsInChildren<Transform>();
                Transform parentOb = null;
                foreach (Transform child in grandFa)
                {
                    if (child.name == cId)
                    {
                        parentOb = child;
                        
                        break;
                    }
                }

                CmDate(1, parentOb.gameObject, cId, cName, cType, hwType);
                break;
            case 2://定位
                string cmId = cm_Id_List[cgCmDropDownId_change.value];
                GameObject temobD = cmABParent.transform.Find("Parent").gameObject;
                Transform[] grandFaD = temobD.GetComponentsInChildren<Transform>();
                foreach (Transform child in grandFaD)
                {
                    if (child.name == cmId)
                    {
                        Camera.main.transform.position = child.position + new Vector3(0, 8, 0);
                        Camera.main.transform.LookAt(child.gameObject.transform);
                        child.gameObject.layer = 8;
                        transFormCd = child;
                        break;
                    }
                    else
                    {
                        child.gameObject.layer = 0;
                    } 
                }
                foreach(Transform child in transFormCd.gameObject.GetComponentsInChildren<Transform>())
                {
                    child.gameObject.layer = 8;
                }
                break;
            case 3:
                break;
        }
    }

    //修改界面更换编号操作
    public void selectCmId_Change()
    {
        cgCmName_change.text = cm_Name_List[cgCmDropDownId_change.value];
        cgcmDropDownType_change.value = cm_Type_List[cgCmDropDownId_change.value];
    }
    /***************修改监控界面****************/
    #endregion

    #region 删除监控操作
    /***************删除监控界面****************/
    public void deleteCm(int index)
    {
        switch (index)
        {
            case 0://关闭
                foreach (GameObject element in menuConfig)
                {
                    element.SetActive(false);
                }
                isUIopen = false;
                break;
            case 1://确定
                string cId = cm_Id_List[cgCmDropDownId_delete.value];
                StartCoroutine(delete_cm(cId));
                break;
            case 2://定位
                string cmId = cm_Id_List[cgCmDropDownId_delete.value];
                GameObject temob = cmABParent.transform.Find("Parent").gameObject;
                Transform[] grandFa = temob.GetComponentsInChildren<Transform>();
                foreach (Transform child in grandFa)
                {
                    if (child.name == cmId)
                    {
                        Camera.main.transform.position = child.position + new Vector3(0, 8, 0);
                        Camera.main.transform.LookAt(child.gameObject.transform);
                        //child.gameObject.layer = 8;
                        break;
                    }
                    //else child.gameObject.layer = 8;
                }
                break;
            case 3:
                break;
        }
    }

    //删除界面更换编号操作
    public void selectCmId_Delete()
    {
        cgCmName_delete.text = cm_Name_List[cgCmDropDownId_delete.value];
        cgcmDropDownType_delete.value = cm_Type_List[cgCmDropDownId_delete.value];
    }

    //删除操作
    public void DeletaCm(string idIndex)
    {
        StartCoroutine(delete_cm(idIndex));
    }
    /***************删除监控界面****************/
    #endregion

    #region 监控数据库保存更新操作
    /***************监控数据处理****************/
    /// <summary>
    /// 监控数据库保存操作
    /// </summary>
    /// <param name="type">0 增加 1更新 </param>
    /// <param name="ob">对象</param>
    /// <param name="cm_Id">对象编号</param>
    /// <param name="cm_Name">对象名称</param>
    /// <param name="cm_Type">对象摄像机种类，球，枪</param>
    /// <param name="hwType">立杆标识 0无 1有</param>
    private void CmDate(int type,GameObject ob,string cm_Id,string cm_Name, int cm_Type,int hwType)
    {
        Transform cmOb = ob.transform.Find("cm");

        string parentposX = "0";
        string parentposY = "0";
        string parentposZ = "0";
        string parentrotX = "0";
        string parentrotY = "0";
        string parentrotZ = "0";
        string parentscaleX = "0";
        string parentscaleY = "0";
        string parentscaleZ = "0";
        string cmposX = "0";
        string cmposY = "0";
        string cmposZ = "0";
        string cmrotX = "0";
        string cmrotY = "0";
        string cmrotZ = "0";
        string cmscaleX = "0";
        string cmscaleY = "0";
        string cmscaleZ = "0";

        string hposX = "0";
        string hposY = "0";
        string hposZ = "0";
        string hrotX = "0";
        string hrotY = "0";
        string hrotZ = "0";
        string hscalX = "0";
        string hscalY = "0";
        string hscalZ = "0";
        string wposX = "0";
        string wposY = "0";
        string wposZ = "0";
        string wrotX = "0";
        string wrotY = "0";
        string wrotZ = "0";
        string wscalX = "0";
        string wscalY = "0";
        string wscalZ = "0";

        if (ob != null)
        {
            parentposX = ob.transform.localPosition.x.ToString();
            parentposY = ob.transform.localPosition.y.ToString();
            parentposZ = ob.transform.localPosition.z.ToString();
            parentrotX = ob.transform.localEulerAngles.x.ToString();
            parentrotY = ob.transform.localEulerAngles.y.ToString();
            parentrotZ = ob.transform.localEulerAngles.z.ToString();
            parentscaleX = ob.transform.localScale.x.ToString();
            parentscaleY = ob.transform.localScale.y.ToString();
            parentscaleZ = ob.transform.localScale.z.ToString();
            if (cmOb != null)
            {
                cmposX = cmOb.localPosition.x.ToString();
                cmposY = cmOb.localPosition.y.ToString();
                cmposZ = cmOb.localPosition.z.ToString();
                cmrotX = cmOb.localEulerAngles.x.ToString();
                cmrotY = cmOb.localEulerAngles.y.ToString();
                cmrotZ = cmOb.localEulerAngles.z.ToString();
                cmscaleX = cmOb.localScale.x.ToString();
                cmscaleY = cmOb.localScale.y.ToString();
                cmscaleZ = cmOb.localScale.z.ToString();
            }

        }

        if (hwType == 1)
        {
            Transform hGo = ob.transform.Find("hw").Find("h");
            Transform wGo = ob.transform.Find("hw").Find("w");
            hposX = hGo.localPosition.x.ToString();
            hposY = hGo.localPosition.y.ToString();
            hposZ = hGo.localPosition.z.ToString();
            hrotX = hGo.localEulerAngles.x.ToString();
            hrotY = hGo.localEulerAngles.y.ToString();
            hrotZ = hGo.localEulerAngles.z.ToString();
            hscalX = hGo.localScale.x.ToString();
            hscalY = hGo.localScale.y.ToString();
            hscalZ = hGo.localScale.z.ToString();
            wposX = wGo.localPosition.x.ToString();
            wposY = wGo.localPosition.y.ToString();
            wposZ = wGo.localPosition.z.ToString();
            wrotX = wGo.localEulerAngles.x.ToString();
            wrotY = wGo.localEulerAngles.y.ToString();
            wrotZ = wGo.localEulerAngles.z.ToString();
            wscalX = wGo.localScale.x.ToString();
            wscalY = wGo.localScale.y.ToString();
            wscalZ = wGo.localScale.z.ToString();
        }

        if (type == 0)
        {
            StartCoroutine(submit_cm(cm_Id, cm_Name, cm_Type, hwType, parentposX, parentposY, parentposZ, parentrotX, parentrotY, parentrotZ, parentscaleX, parentscaleY, parentscaleZ, cmposX, cmposY, cmposZ, cmrotX, cmrotY, cmrotZ, cmscaleX, cmscaleY, cmscaleZ,hposX, hposY, hposZ, hrotX, hrotY, hrotZ, hscalX, hscalY, hscalZ, wposX, wposY, wposZ, wrotX, wrotY, wrotZ, wscalX, wscalY, wscalZ));
        }
        else
        {
            StartCoroutine(update_cm(cm_Id, cm_Name, cm_Type, hwType, parentposX, parentposY, parentposZ, parentrotX, parentrotY, parentrotZ, parentscaleX, parentscaleY, parentscaleZ, cmposX, cmposY, cmposZ, cmrotX, cmrotY, cmrotZ, cmscaleX, cmscaleY, cmscaleZ, hposX, hposY, hposZ, hrotX, hrotY, hrotZ, hscalX, hscalY, hscalZ, wposX, wposY, wposZ, wrotX, wrotY, wrotZ, wscalX, wscalY, wscalZ));
        }
    }
    /***************监控数据处理****************/
    #endregion

    #region 数据库操作-增
    /***************数据库操作****************/
    //增
    IEnumerator submit_cm(string cm_Id, string cm_Name, int cm_Type, int cm_hw, string parentposX, string parentposY, string parentposZ, string parentrotX, string parentrotY, string parentrotZ, string parentscaleX, string parentscaleY, string parentscaleZ, string cmposX, string cmposY, string cmposZ, string cmrotX, string cmrotY, string cmrotZ, string cmscaleX, string cmscaleY, string cmscaleZ, string hposX,string hposY,string hposZ,string hrotX,string hrotY,string hrotZ,string hscalX,string hscalY,string hscalZ, string wposX, string wposY, string wposZ, string wrotX, string wrotY, string wrotZ, string wscalX, string wscalY, string wscalZ)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "submit_cm");

        form.AddField("cm_Id", cm_Id);
        form.AddField("cm_Name", cm_Name);
        form.AddField("cm_Type", cm_Type);
        form.AddField("cm_hw", cm_hw);

        form.AddField("parentposX", parentposX);
        form.AddField("parentposY", parentposY);
        form.AddField("parentposZ", parentposZ);
        form.AddField("parentrotX", parentrotX);
        form.AddField("parentrotY", parentrotY);
        form.AddField("parentrotZ", parentrotZ);
        form.AddField("parentscaleX", parentscaleX);
        form.AddField("parentscaleY", parentscaleY);
        form.AddField("parentscaleZ", parentscaleZ);

        form.AddField("cmposX", cmposX);
        form.AddField("cmposY", cmposY);
        form.AddField("cmposZ", cmposZ);
        form.AddField("cmrotX", cmrotX);
        form.AddField("cmrotY", cmrotY);
        form.AddField("cmrotZ", cmrotZ);
        form.AddField("cmscaleX", cmscaleX);
        form.AddField("cmscaleY", cmscaleY);
        form.AddField("cmscaleZ", cmscaleZ);

        form.AddField("hposX", hposX);
        form.AddField("hposY", hposY);
        form.AddField("hposZ", hposZ);
        form.AddField("hrotX", hrotX);
        form.AddField("hrotY", hrotY);
        form.AddField("hrotZ", hrotZ);
        form.AddField("hscalX", hscalX);
        form.AddField("hscalY", hscalY);
        form.AddField("hscalZ", hscalZ);
        form.AddField("wposX", wposX);
        form.AddField("wposY", wposY);
        form.AddField("wposZ", wposZ);
        form.AddField("wrotX", wrotX);
        form.AddField("wrotY", wrotY);
        form.AddField("wrotZ", wrotZ);
        form.AddField("wscalX", wscalX);
        form.AddField("wscalY", wscalY);
        form.AddField("wscalZ", wscalZ);

        //WWW www = new WWW(cmUrl, form);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //}

        UnityWebRequest request = UnityWebRequest.Post(cmUrl, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            tipsOb.SetActive(true);
            GameObject Txt = tipsOb.transform.Find("Text").gameObject;
            Txt.GetComponent<Text>().text = "增加成功！";
        }
    }
    #endregion

    #region 数据库操作-删
    //删，全部
    IEnumerator delete_cmAll()
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "delete_cmAll");

        //WWW www = new WWW(cmUrl, form);
        //yield return www;
        //
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //}

        UnityWebRequest request = UnityWebRequest.Post(cmUrl, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            tipsOb.SetActive(true);
            GameObject Txt = tipsOb.transform.Find("Text").gameObject;
            Txt.GetComponent<Text>().text = "删除成功！";
            StartCoroutine(show_cm(-100));
        }

    }

    //删，指定
    IEnumerator delete_cm(string cm_Id)
    {
        //print("cm_Id == " + cm_Id);
        WWWForm form = new WWWForm();
        form.AddField("action", "delete_cm");
        form.AddField("cm_Id", cm_Id);

        UnityWebRequest request = UnityWebRequest.Post(cmUrl, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            GameObject temob = cmABParent.transform.Find("Parent").gameObject;
            Transform[] grandFa = temob.GetComponentsInChildren<Transform>();
            foreach (Transform child in grandFa)
            {
                if (child.name == cm_Id)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
            tipsOb.SetActive(true);
            GameObject Txt = tipsOb.transform.Find("Text").gameObject;
            Txt.GetComponent<Text>().text = "删除成功！";
            StartCoroutine(show_cm(-100));
        }
    }
    #endregion

    #region 数据库操作-改
    //改，指定
    IEnumerator update_cm(string cm_Id, string cm_Name, int cm_Type, int cm_hw, string parentposX, string parentposY, string parentposZ, string parentrotX, string parentrotY, string parentrotZ, string parentscaleX, string parentscaleY, string parentscaleZ, string cmposX, string cmposY, string cmposZ, string cmrotX, string cmrotY, string cmrotZ, string cmscaleX, string cmscaleY, string cmscaleZ, string hposX, string hposY, string hposZ, string hrotX, string hrotY, string hrotZ, string hscalX, string hscalY, string hscalZ, string wposX, string wposY, string wposZ, string wrotX, string wrotY, string wrotZ, string wscalX, string wscalY, string wscalZ)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "update_cm");
        form.AddField("cm_Id", cm_Id);
        form.AddField("cm_Name", cm_Name);
        form.AddField("cm_Type", cm_Type);
        form.AddField("cm_hw", cm_hw);

        form.AddField("parentposX", parentposX);
        form.AddField("parentposY", parentposY);
        form.AddField("parentposZ", parentposZ);
        form.AddField("parentrotX", parentrotX);
        form.AddField("parentrotY", parentrotY);
        form.AddField("parentrotZ", parentrotZ);
        form.AddField("parentscaleX", parentscaleX);
        form.AddField("parentscaleY", parentscaleY);
        form.AddField("parentscaleZ", parentscaleZ);

        form.AddField("cmposX", cmposX);
        form.AddField("cmposY", cmposY);
        form.AddField("cmposZ", cmposZ);
        form.AddField("cmrotX", cmrotX);
        form.AddField("cmrotY", cmrotY);
        form.AddField("cmrotZ", cmrotZ);
        form.AddField("cmscaleX", cmscaleX);
        form.AddField("cmscaleY", cmscaleY);
        form.AddField("cmscaleZ", cmscaleZ);

        form.AddField("hposX", hposX);
        form.AddField("hposY", hposY);
        form.AddField("hposZ", hposZ);
        form.AddField("hrotX", hrotX);
        form.AddField("hrotY", hrotY);
        form.AddField("hrotZ", hrotZ);
        form.AddField("hscalX", hscalX);
        form.AddField("hscalY", hscalY);
        form.AddField("hscalZ", hscalZ);
        form.AddField("wposX", wposX);
        form.AddField("wposY", wposY);
        form.AddField("wposZ", wposZ);
        form.AddField("wrotX", wrotX);
        form.AddField("wrotY", wrotY);
        form.AddField("wrotZ", wrotZ);
        form.AddField("wscalX", wscalX);
        form.AddField("wscalY", wscalY);
        form.AddField("wscalZ", wscalZ);

        UnityWebRequest request = UnityWebRequest.Post(cmUrl, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            tipsOb.SetActive(true);
            GameObject Txt = tipsOb.transform.Find("Text").gameObject;
            Txt.GetComponent<Text>().text = "修改成功！";
            //Debug.Log(www.text);
            StartCoroutine(show_cm(-100));
        }
    }
    #endregion

    #region 数据库操作-查
    //查
    IEnumerator show_cm(int index)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "show_cm");

        UnityWebRequest request = UnityWebRequest.Post(cmUrl, form);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            var received_data = Regex.Split(request.downloadHandler.text, "</next>");
            int dblenth = 40;//数据库数据总列数
            int str = (received_data.Length - 1) / dblenth;
            cm_Id_List.Clear();
            cm_Name_List.Clear();
            cm_Type_List.Clear();
            cm_hwType_List.Clear();

            parentposList.Clear();
            parentrotList.Clear();
            parentscaleList.Clear();
            cmposList.Clear();
            cmrotList.Clear();
            cmscaleList.Clear();

            hposList.Clear();
            hrotList.Clear();
            hscaleList.Clear();
            wposList.Clear();
            wrotList.Clear();
            wscaleList.Clear();

            for (int i = 0; i < str; i++)
            {
                //print("cm_ID: " + received_data[9 * i] + " cm_Name: " + received_data[9 * i + 1]);
                cm_Id_List.Add(received_data[dblenth * i]);
                cm_Name_List.Add(received_data[dblenth * i + 1]);
                cm_Type_List.Add(int.Parse(received_data[dblenth * i + 2]));
                cm_hwType_List.Add(int.Parse(received_data[dblenth * i + 3]));
                parentposList.Add(new Vector3(float.Parse(received_data[dblenth * i + 4]), float.Parse(received_data[dblenth * i + 5]), float.Parse(received_data[dblenth * i + 6])));
                parentrotList.Add(new Vector3(float.Parse(received_data[dblenth * i + 7]), float.Parse(received_data[dblenth * i + 8]), float.Parse(received_data[dblenth * i + 9])));
                parentscaleList.Add(new Vector3(float.Parse(received_data[dblenth * i + 10]), float.Parse(received_data[dblenth * i + 11]), float.Parse(received_data[dblenth * i + 12])));
                cmposList.Add(new Vector3(float.Parse(received_data[dblenth * i + 13]), float.Parse(received_data[dblenth * i + 14]), float.Parse(received_data[dblenth * i + 15])));
                cmrotList.Add(new Vector3(float.Parse(received_data[dblenth * i + 16]), float.Parse(received_data[dblenth * i + 17]), float.Parse(received_data[dblenth * i + 18])));
                cmscaleList.Add(new Vector3(float.Parse(received_data[dblenth * i + 19]), float.Parse(received_data[dblenth * i + 20]), float.Parse(received_data[dblenth * i + 21])));


                hposList.Add(new Vector3(float.Parse(received_data[dblenth * i + 22]), float.Parse(received_data[dblenth * i + 23]), float.Parse(received_data[dblenth * i + 24])));
                hrotList.Add(new Vector3(float.Parse(received_data[dblenth * i + 25]), float.Parse(received_data[dblenth * i + 26]), float.Parse(received_data[dblenth * i + 27])));
                hscaleList.Add(new Vector3(float.Parse(received_data[dblenth * i + 28]), float.Parse(received_data[dblenth * i + 29]), float.Parse(received_data[dblenth * i + 30])));
                wposList.Add(new Vector3(float.Parse(received_data[dblenth * i + 31]), float.Parse(received_data[dblenth * i + 32]), float.Parse(received_data[dblenth * i + 33])));
                wrotList.Add(new Vector3(float.Parse(received_data[dblenth * i + 34]), float.Parse(received_data[dblenth * i + 35]), float.Parse(received_data[dblenth * i + 36])));
                wscaleList.Add(new Vector3(float.Parse(received_data[dblenth * i + 37]), float.Parse(received_data[dblenth * i + 38]), float.Parse(received_data[dblenth * i + 39])));
            }

            if (index == 0)//修改
            {
                cgCmDropDownId_change.options.Clear();
                Dropdown.OptionData tempData;
                for (int i = 0; i < cm_Id_List.Count; i++)
                {
                    tempData = new Dropdown.OptionData();
                    tempData.text = cm_Id_List[i];
                    cgCmDropDownId_change.options.Add(tempData);
                }
                if (cm_Id_List.Count <= 0)
                {
                    cgCmDropDownId_change.captionText.text = "";
                    cgCmName_change.text = "";
                    cgcmDropDownType_change.value = 0;
                }
                else
                {
                    cgCmDropDownId_change.captionText.text = cm_Id_List[c_index - 1];
                    cgCmDropDownId_change.value = c_index - 1;
                    cgCmName_change.text = cm_Name_List[c_index - 1];
                    cgcmDropDownType_change.value = cm_Type_List[c_index - 1];
                }

            }
            else if (index == 1)//删除
            {
                cgCmDropDownId_delete.options.Clear();
                Dropdown.OptionData tempData;
                for (int i = 0; i < cm_Id_List.Count; i++)
                {
                    tempData = new Dropdown.OptionData();
                    tempData.text = cm_Id_List[i];
                    cgCmDropDownId_delete.options.Add(tempData);
                }
                if (cm_Id_List.Count <= 0)
                {
                    cgCmDropDownId_delete.captionText.text = "";
                    cgCmName_delete.text = "";
                    cgcmDropDownType_delete.value = 0;
                }
                else
                {
                    cgCmDropDownId_delete.captionText.text = cm_Id_List[c_index - 1];
                    cgCmDropDownId_delete.value = c_index - 1;
                    cgCmName_delete.text = cm_Name_List[c_index - 1];
                    cgcmDropDownType_delete.value = cm_Type_List[c_index - 1];
                }

            }
            else if (index == -1)//实例化
            {
                StartCoroutine(InstantiateCm("camera", 1));
            }
            else if (index == -100)
            {
                if (menuConfig[4].activeSelf)
                {
                    cgCmDropDownId_delete.options.Clear();
                    Dropdown.OptionData tempData;
                    for (int i = 0; i < cm_Id_List.Count; i++)
                    {
                        tempData = new Dropdown.OptionData();
                        tempData.text = cm_Id_List[i];
                        cgCmDropDownId_delete.options.Add(tempData);
                    }
                    if (cm_Id_List.Count <= 0)
                    {
                        cgCmDropDownId_delete.captionText.text = "";
                        cgCmName_delete.text = "";
                        cgcmDropDownType_delete.value = 0;
                    }
                    else
                    {
                        cgCmDropDownId_delete.captionText.text = cm_Id_List[0];
                        cgCmName_delete.text = cm_Name_List[0];
                        cgcmDropDownType_delete.value = cm_Type_List[0];
                    }
                }
                EventCenter.Broadcast(EventDefine.ShowCmPanel);
            }
        }


    }
    /***************数据库操作****************/
    #endregion

    #region 外部调用EXE操作
    /***************外部海康EXE平台****************/
    IEnumerator ShowNewWindow()
    {
        ToolControlTaskBar.HideTaskBar();
        Process Process1 = new Process();
        Process1.StartInfo.FileName = exePath;
        Process1.StartInfo.UseShellExecute = false;
        Process1.Start();
        Process1.WaitForExit();
        ToolControlTaskBar.ShowTaskBar();
        //UnityEngine.Debug.Log("----------");
        yield return 0;
    }

    void KillProcess(string processName)
    {
        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            try
            {
                if (!process.HasExited)
                {
                    //UnityEngine.Debug.Log(process.ProcessName);
                    if (process.ProcessName == processName)
                    {
                        process.Kill();
                        UnityEngine.Debug.Log("已杀死进程");
                        ToolControlTaskBar.ShowTaskBar();
                        //UnityEngine.Debug.Log("-----2-----");
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                UnityEngine.Debug.Log(ex);
            }
        }
    }
    /***************外部海康EXE平台****************/
    #endregion

    #region 场景中打开监控操作
    /***************场景中打开视频****************/
    //关闭场景监控
    void CloseCmScene()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject ob = hit.transform.gameObject;
            if (ob.name != "cm")
            {
                if(isSelectCm)
                {
                    isSelectCm = false;
                }
                if (isSelectBox)
                {
                    isSelectBox = false;
                    cmListJs.Clear();
                }
                CloseLight();
            }
        }
    }

    //打开场景监控
    void OpenCmScene()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //print("hit ------------ " + hit.transform.gameObject.name);
            GameObject ob = hit.transform.gameObject;
            if (ob.name == "cm" && ob.layer != 8)
            {
                //print(" -----------  " + ob.transform.parent.name);
                int id = int.Parse(ob.transform.parent.name.Substring(2, ob.transform.parent.name.Length - 2));
                //print("id ------------ " + id);
                cameraId = id;
                isSelectCm = true;
                startBtnPos = Input.mousePosition;
                OpenLight(ob);
            }
        }
    }

    //播放场景监控
    private void OnGUI()
    {
        //print("isSelectCm + isSelectBox" + isSelectCm + " " + isSelectBox);
        if(isSelectCm)
        {
            //GUI.Label(new Rect(startBtnPos.x - 15, Screen.height - startBtnPos.y - 25, 200, 60), bcCmBtn);
            GUI.color = Color.white;  //按钮文字颜色  
            GUI.backgroundColor = new Color(15,214,253,255); //按钮背景颜色
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.alignment = TextAnchor.UpperCenter;
            fontStyle.fontSize = 20;
            fontStyle.font = font;
            fontStyle.normal.textColor = Color.white;
            fontStyle.normal.background = (Texture2D)bcCmBtn;

            if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 49, 49), "",fontStyle))
            {
                //print(" 预览 " + cameraId);
                CmConfig(0);
                CloseLight();
            }
        }
        else
        {
            if (isSelectBox)
            {
                GUI.color = Color.white;  //按钮文字颜色  
                GUI.backgroundColor = new Color(15, 214, 253, 255); //按钮背景颜色
                GUIStyle fontStyle = new GUIStyle();
                fontStyle.alignment = TextAnchor.UpperCenter;
                fontStyle.fontSize = 20;
                fontStyle.font = font;
                fontStyle.normal.textColor = Color.white;
                fontStyle.normal.background = (Texture2D)bcCmBtn;
                if(isSelectBoxMove)
                {
                    startBtnPos = Input.mousePosition;
                }
                if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 49, 49), "", fontStyle))
                {
                    CmConfig(0);
                    CloseLight();
                }
            }
        }
    }

    //播放CS监控
    private void StartOpenView()
    {
        TestMedia_FFmpeg.Instance.isAutoPlay = false;
        TestMedia_FFmpeg.Instance.m_viewDropdown.value = cameraId - 1;
        TestMedia_FFmpeg.Instance.selectedChannel = cameraId - 1;
        TestMedia_FFmpeg.Instance.StartRealPlay();
    }

    //框选的监控增加到列表
    public void AddCm(GameObject ob)
    {
        isSelectBox = true;
        isSelectBoxMove = true;
        int id = int.Parse(ob.transform.parent.name.Substring(2, ob.transform.parent.name.Length - 2));
        
        bool isHave = false;
        foreach (int item in cmListJs)
        {
            if(item == id)
            {
                isHave = true;
            }
        }
        if(!isHave)
        {
            //print("name + id ==== " + ob.name + "  " + id);
            cmListJs.Add(id);
            OpenLight(ob);
        }
    }
    /***************场景中打开视频****************/
    #endregion

    #region 监控高亮操作
    //打开监控高亮
    public void OpenLight(GameObject ob)
    {
        Transform fGo = ob.transform.Find("cm");
        if(fGo != null)
        {
            if (fGo.GetComponent<HighlightObject>() != null)
            {
                fGo.GetComponent<HighlightObject>().SwithHightlight(true);
            }
        }
        if (ob.transform.Find("cm") != null)
        {
            ob.GetComponent<HighlightObject>().SwithHightlight(true);
        }
        if(ob.name == "cm")
        {
            ob.GetComponent<HighlightObject>().SwithHightlight(true);
        }
    }

    //关闭监控高亮
    public void CloseLight()
    {
        GameObject temob = cmABParent.transform.Find("Parent").gameObject;
        Transform[] grandFa = temob.GetComponentsInChildren<Transform>();
        foreach (Transform child in grandFa)
        {
            if (child.name == "cm")
            {
                child.GetComponent<HighlightObject>().SwithHightlight(false);
            }
        }
    }
    #endregion
}
