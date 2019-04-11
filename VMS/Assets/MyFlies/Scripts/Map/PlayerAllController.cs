using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class PlayerAllController : MonoBehaviour {
    //单例
    public static PlayerAllController instance;
    //资源管理器
    private ManagerVars vars;
    //遮罩
    private GameObject maskPanel;

    #region 玩家行走
    /*******************玩家行走******************/
    //玩家Transform
    private Transform m_Transform;
    //玩家Controller
    public static CharacterController controller;
    //监控是否打开
    public static bool viewOpenbool = true;
    //玩家速度
    private float Spped = 3;
    /*******************玩家行走******************/
    #endregion

    #region 玩家视角
    /*******************玩家视角******************/
    //鼠标X速率
    private float sensitivityX = 10f;
    //鼠标Y速率
    private float sensitivityY = 10f;
    //相机Transform
    private Transform mCamTransform;
    //是否时上帝视角
    public static bool mviewbool = true;
    //四元数
    Quaternion mCameraRot = Quaternion.identity;
    //玩家初始相机坐标
    private Vector3 mStartCamPos;
    //欧拉角
    Vector3 meulerAngle = Vector3.up;
    /*******************玩家视角******************/
    #endregion

    #region 相机操作-平移
    /*******************平移相机******************/
    //移动 相机标识
    private bool moveCamera = false;
    //移动 相机坐标
    private Vector3 mousMovepos;
    //中心图标组件
    private GameObject  quasiminded;
    //平移速率
    private float speedmo = 0;
    /*******************平移相机******************/
    #endregion

    #region 相机操作-旋转
    /*****************围绕物体旋转****************/
    //记录鼠标点击的初始位置
    Vector2 first = Vector2.zero;
    //记录鼠标移动时的位置
    Vector2 second = Vector2.zero;
    //左移标识
    bool directorToLeft = false;
    //右移标识
    bool directorToRight = false;
    //上移标识
    bool directorToUp = false;
    //下移标识
    bool directorToDown = false;
    //标记是否鼠标在滑动
    bool dragging = false;
    //ZoomIn ZoomOut数组
    private List<GameObject> ZoomObject = new List<GameObject>();
    //旋转时鼠标位置
    private Vector3 Roundmouspo;
    //选择时对象目标中心位置
    private GameObject targetModel;
    //是否在旋转
    private bool RoundBool = false;
    /*****************围绕物体旋转****************/
    #endregion

    #region 双击地面视角操作
    /*****************双击切换视角****************/
    //射线
    RaycastHit hitxxx;
    //是否双击地面到地面的变化标识
    private bool firstbool = false;
    //双击下降时间
    float timex = 0;
    //双击对象
    private GameObject doublegame;
    //可双击图层
    private LayerMask clicklayer;
    //本身gameObject
    private GameObject mPlayer;
    //双击后移动标识
    private bool moveDouble = false;
    //双击前坐标
    private Vector3 douposstart;
    //双击结束坐标
    private Vector3 douposend;

    //双击地面目标资源
    private GameObject doubleTarget;
    //双击地面目标对象点
    private GameObject targetdob = null;
    //存储双击前摄像机坐标
    private Vector3 cavecter;
    //小地图摄像机
    private GameObject cameraMini;
    //双击旋转速率
    private int hspeed = 0;
    //双击下降最低高度
    private float yyy = 0;
    //双击移动标识
    private bool camebool = false;
    //双击旋转速度
    private int Rotnum = 0;
    /*****************双击切换视角****************/
    #endregion

    #region 地球视角
    /*******************地球视角******************/
    //地球
    private GameObject DQGame;
    //UI总组件
    private GameObject UIGame;
    //地球是否在
    public static bool DQbool = false;
    //logo组件
    private Text loadNameText;
    //地球下降速率
    private double speedearth = 1;
    /*******************地球视角******************/
    #endregion

    #region 鼠标样式
    /*******************鼠标样式******************/
    //鼠标
    private Texture2D defaultcursorTexture;
    //手
    private Texture2D newcursorTexture;
    //鼠标模式
    private CursorMode cursorMode = CursorMode.Auto;
    //鼠标样式位置
    private Vector2 hotSpot = Vector2.zero;
    //手贴图
    private Texture2D handTexture1;
    //鼠标贴图
    private Texture2D handTexture2;
    /*******************鼠标样式******************/
    #endregion

    /*******************AssetBundle******************/
    //AB加载场景中的父物体
    private GameObject assetBundleParent;
    /*******************AssetBundle******************/
    private void Awake()
    {
        #region 初始化组件等
        //获取资源管理器
        vars = ManagerVars.GetManagerVars();
        //实例化遮罩
        maskPanel = Instantiate(vars.maskPanel);
        //获取buttonALL组件
        UIGame = FindObjectOfType<OpenCloseMiniMap>().transform.Find("UI").gameObject;
        //获取父物体
        GameObject maskParent = UIGame.transform.Find("CanvasMask").gameObject;
        //设置遮罩父物体
        maskPanel.transform.SetParent(maskParent.transform);
        //重新设置Left Right 
        maskPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
        //重新设置Top Bottom 
        maskPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
        //实例化中心图标
        quasiminded = Instantiate(vars.qua);
        //设置中心图标父物体
        quasiminded.transform.SetParent(maskParent.transform);
        //重新设置中心图标位置
        quasiminded.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
        //重新设置中心图标大小
        quasiminded.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //初始隐藏
        quasiminded.SetActive(false);

        //获取ZoomIn组件
        GameObject zoomIn = UIGame.transform.Find("CanvasTwo/ZoomINOUT/IN").gameObject;
        //获取ZoomOut组件
        GameObject zoomOut = UIGame.transform.Find("CanvasTwo/ZoomINOUT/OUT").gameObject;
        //ZoomIn加入数组
        ZoomObject.Add(zoomIn);
        //ZoomOut加入数组
        ZoomObject.Add(zoomOut);
        //获取可双击图层
        clicklayer = LayerMask.NameToLayer("Everything");
        //获取本身gameObject
        mPlayer = gameObject;

        //实例化双击目标点对象
        doubleTarget = Instantiate(vars.doubleTarget);
        //获取鼠标图标样式
        defaultcursorTexture = vars.defaultcursorTexture;
        //获取手图标样式
        newcursorTexture = vars.newcursorTexture;
        //获取AB加载场景父物体
        assetBundleParent = FindObjectOfType<AssetBundleParent>().gameObject;
        //实例化旋转中心对象
        targetModel = Instantiate(vars.doubleTarget);

        //获取Logo组件
        loadNameText = UIGame.transform.Find("CanvasOne/ImageLogo/TextLogo").GetComponent<Text>();
        //赋值给logo
        loadNameText.text = GetJson.logoName;
        //获取小地图相机组件
        cameraMini = transform.Find("Main Camera/CameraMini").gameObject;
        #endregion

        //加载地球AB
        InstanceVoid(loadAsserBundle.nowAssetBundleob,"earth", 0);
    }

    void Start()
    {
        #region 初始化一些数据
        maskPanel.SetActive(true);//遮挡显示
        //获取单例
        instance = this.GetComponent<PlayerAllController>();
        //Debug.Log("dqbool ---  " + DQbool);
        //地球未显示过
        if (!DQbool)
        {
            //地球显示
            DQGame.SetActive(true);
            /******************数据保存下*****************/
            //保存切换前坐标
            mStartCamPos = Camera.main.transform.position;
            //保存切换前欧拉角
            meulerAngle = Camera.main.transform.eulerAngles;
            //设置相机欧拉角
            mCameraRot.eulerAngles = new Vector3(60, 0, 0.0f);
            //设置相机朝向
            Camera.main.transform.localRotation = mCameraRot;
            //设置相机位置
            Camera.main.transform.localPosition = new Vector3(mStartCamPos.x, mStartCamPos.y + 550, mStartCamPos.z);
            //设置相机盯的对象
            Camera.main.transform.LookAt(DQGame.transform);
            /******************数据保存下*****************/
        }
        else//地球已显示过，可去掉（为了刷新场景保留的）
        {
            //重置相机坐标
            Camera.main.transform.localPosition = new Vector3(0, 152, 0);
            //重置相机欧拉角
            mCameraRot.eulerAngles = new Vector3(60, 0, 0);
            //重置相机朝向
            Camera.main.transform.localRotation = mCameraRot;
            //UI显示
            UIGame.SetActive(true);
            //加载为真
            FirstLoad.gametime = true;
        }
        //是否为上帝视角
        mviewbool = false;

        /*******************玩家行走******************/
        //获取玩家Transform
        m_Transform = gameObject.GetComponent<Transform>();
        //获取玩家Controller
        controller = this.GetComponent<CharacterController>();
        /*******************玩家行走******************/

        /*******************玩家视角******************/
        //获取相机Transform
        mCamTransform = Camera.main.transform;
        //获取相机Pos
        mStartCamPos = Camera.main.transform.position;
        //获取相机欧拉角
        meulerAngle = Camera.main.transform.eulerAngles;
        /*******************玩家视角******************/

        //获取手指针贴图
        handTexture1 = vars.defaultcursorTexture;
        //获取鼠标指针贴图
        handTexture2 = vars.newcursorTexture;
        #endregion
    }

    void Update()
    {
        #region 帧函数
        /*******************玩家行走******************/
        if (mviewbool && !firstbool)
        {
            MoveControl();
        }
        /*******************玩家行走******************/

        /*******************平移,旋转相机******************/
        if (!mviewbool && !firstbool)
        {
            TranslateCamera();
            RoundObjectFunction();
        }
        /*******************平移,旋转相机******************/


        /*****************双击切换视角****************/
        if (firstbool)
        {
            DoubleMovePlayer();
            CamMoveViewCamera();
        }
        /*****************双击切换视角****************/

        /*******************地球视角******************/
        if (!DQbool)
        {
            UpDataDQ();
        }
        else
        {
            
        }
        /*******************地球视角******************/
        #endregion
    }

    #region 地球视角函数
    /*******************地球视角******************/
    void UpDataDQ()
    {
        if (Camera.main.transform.localPosition.y < 250)
        {
            StartCoroutine(loadAsserBundle.instance.InstantiateObject("build", 1));
            DQbool = true;
            Camera.main.transform.localPosition = new Vector3(0, 152, 0);
            mCameraRot.eulerAngles = new Vector3(60, 0, 0);
            Camera.main.transform.localRotation = mCameraRot;
            UIGame.SetActive(true);
            FirstLoad.gametime = true;
            Destroy(DQGame);
            maskPanel.SetActive(false);//遮挡隐藏
            GameObject sm = GameObject.Find("zhangpeng/Sence01/SM_Ground");
            sm.layer = 9;
            return;
        }
        else
        {
            if (!FirstLoad.gametime)
            {
                speedearth += 0.04;
                Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y - (float)speedearth, Camera.main.transform.localPosition.z);
            }
            else
            {
                DQbool = true;
                Camera.main.transform.localPosition = new Vector3(0, 152, 0);
                mCameraRot.eulerAngles = new Vector3(60, 0, 0);
                Camera.main.transform.localRotation = mCameraRot;
                UIGame.SetActive(true);
                Destroy(DQGame);
                GameObject sm = GameObject.Find("zhangpeng/Sence01/SM_Ground");
                sm.layer = 9;
            }
        }         
    }
    /*******************地球视角******************/
    #endregion

    #region 玩家行走，视角函数
    /*******************玩家行走******************/
    void MoveControl()
    {

        if (!viewOpenbool)
        {
            return;
        }

        if (Menu.configbool) return;
        if (Input.GetKey(KeyCode.Comma))
        {
            if (Spped < 10)
            {
                Spped += 1;
                //Debug.Log("加速");
            }
            
        }
        if (Input.GetKey(KeyCode.Period))
        {
            if (Spped > 1)
            {
                Spped -= 1;
                //Debug.Log("减速");
            } 
        }

        float h = Input.GetAxis("Horizontal")* Spped;
        float v = Input.GetAxis("Vertical")* Spped;

        //Debug.Log(h + "    " + v);
        if (Input.GetKey(KeyCode.W) || v > 0)
       {
            mCameraRot.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0.0f);
            transform.localRotation = mCameraRot;

            //m_Transform.Translate(Vector3.forward * 0.1f, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.SimpleMove(transform.forward * (v+20) * 1);
            }
            else
            {
                controller.SimpleMove(transform.forward * v * 1);
            }
            
            
        }
       if (Input.GetKey(KeyCode.S) || v < 0)
       {
            //m_Transform.Translate(Vector3.back * 0.1f, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.SimpleMove(transform.forward * (v-20) * 1);
            }
            else
            {
                controller.SimpleMove(transform.forward * v * 1);
            }
        }
       if (Input.GetKey(KeyCode.A) || h < 0)
       {
            //m_Transform.Translate(Vector3.left * 0.1f, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.SimpleMove(transform.right * (h-20) * 1);
            }
            else
            {
                controller.SimpleMove(transform.right * h * 1);
            }
        }
       if (Input.GetKey(KeyCode.D) || h > 0)
       {
            //m_Transform.Translate(Vector3.right * 0.1f, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.SimpleMove(transform.right * (h+20) * 1);
            }
            else
            {
                controller.SimpleMove(transform.right * h * 1);
            }
        }
       if (Input.GetKey(KeyCode.Q))
       {
           Camera.main.transform.Rotate(Vector3.up, -1.0f);
       }
       if (Input.GetKey(KeyCode.E))
       {
           Camera.main.transform.Rotate(Vector3.up, 1.0f);
       }
       if (mviewbool && Input.GetMouseButton(1))
       {
           transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
           transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
       }
    }
    /*******************玩家行走******************/

    /*******************玩家视角******************/
    //上帝视角切换
   public void ChangeView()
    {
        if (mviewbool)
        {
            mStartCamPos = Camera.main.transform.position;//保存切换前坐标
            meulerAngle = Camera.main.transform.eulerAngles;//保存切换前欧拉角
            mCameraRot.eulerAngles = new Vector3(60, 0, 0.0f);
            mCamTransform.localRotation = mCameraRot;
            Camera.main.transform.localPosition = new Vector3(mStartCamPos.x, mStartCamPos.y+150, mStartCamPos.z);
            mviewbool = false;
            //Debug.Log("上帝视角");
        } 
        else
        {
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            mCameraRot.eulerAngles = new Vector3(0, 0, 0.0f);
            mCamTransform.localRotation = mCameraRot;
            this.transform.localRotation = mCameraRot;
            Camera.main.transform.localPosition = new Vector3(0, 2, 0);
            //Camera.main.fieldOfView = 60;
            //Camera.main.fieldOfView = 60;
            mviewbool = true;
            //Debug.Log("普通视角");
        }
    }
    /*******************玩家视角******************/
    #endregion

    #region 相机平移，旋转函数
    /*******************平移相机******************/
    void TranslateCamera()
    {
        if (Menu.configbool) return;
        if (Input.GetMouseButtonUp(0))
        {
            if(!WebGL.isIEBool)
            {
                defaultcursorTexture = handTexture1;
                Cursor.SetCursor(defaultcursorTexture, hotSpot, cursorMode);
            }
        }

        ///<说明>  
        /// 通过鼠标X坐标拖动场景  
        ///   
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("不是UI");
            if (Input.GetMouseButtonDown(2))
            {
                float xoxo = Camera.main.transform.localPosition.y;
                speedmo = xoxo / 20;
                //mousMovepos = Input.mousePosition;
                if (!WebGL.isIEBool)
                {
                    newcursorTexture = handTexture2;
                    Cursor.SetCursor(newcursorTexture, hotSpot, cursorMode);
                }
                moveCamera = true;
            }
            if (Input.GetMouseButtonUp(2))
            {
                moveCamera = false;
                if (!WebGL.isIEBool)
                {
                    defaultcursorTexture = handTexture1;
                    Cursor.SetCursor(defaultcursorTexture, hotSpot, cursorMode);
                }
            }
            if (Input.GetMouseButton(2) && moveCamera)
            {
                mousMovepos = Input.mousePosition;
                //Debug.Log(mousMovepos.x +  "    " + Screen.width);
                if (mousMovepos.x > 5 && mousMovepos.x < Screen.width-5 && mousMovepos.y > 5 && mousMovepos.y < Screen.height-5 && speedmo > 0)
                {
                    Camera.main.transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * speedmo);
                    Camera.main.transform.Translate(Vector3.down * Input.GetAxis("Mouse Y") * speedmo);
                }   
            }
        } 
        else
        {

        }

        //Zoom out  
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomObject[1].SetActive(true);
            ZoomObject[0].SetActive(false);
            Vector3 capos = Camera.main.transform.localPosition;
            if (capos.y <= 298)
            {
                Camera.main.transform.localPosition += new Vector3(0, 2, 0);
            }
        }
        //Zoom in  
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            Vector3 capos = Camera.main.transform.position;
            //Debug.Log(capos.y);
            if (capos.y > 3 && capos.y <= 302)
            {
                Camera.main.transform.position += new Vector3(0, -2, 0);
            }
            ZoomObject[1].SetActive(false);
            ZoomObject[0].SetActive(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            ZoomObject[1].SetActive(false);
            ZoomObject[0].SetActive(false);
        }
    }
    /*******************平移相机******************/

    /*****************围绕物体旋转****************/
    void RoundObjectFunction()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RoundBool = true;
            //model.SetActive(true);
            targetModel.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.localPosition.y));
            Roundmouspo = Input.mousePosition;
            quasiminded.SetActive(true);

            first = Input.mousePosition;
            dragging = true;
            if (!WebGL.isIEBool)
            {
                newcursorTexture = handTexture2;
                Cursor.SetCursor(newcursorTexture, hotSpot, cursorMode);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            RoundBool = false;
            quasiminded.SetActive(false);
            dragging = false;
            if (!WebGL.isIEBool)
            {
                defaultcursorTexture = handTexture1;
                Cursor.SetCursor(defaultcursorTexture, hotSpot, cursorMode);
            }
        }
        if (Input.GetMouseButton(1) && RoundBool)
        {
            targetModel.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.localPosition.y));
            second = Input.mousePosition;

            Vector2 slideDirection = second - first;

            float x = slideDirection.x, y = slideDirection.y;

            if (y < x && y > -x)
            {// right

                directorToLeft = false;

                directorToRight = true;

                directorToUp = false;

                directorToDown = false;

            }
            else if (y > x && y < -x)
            {// left

                directorToLeft = true;

                directorToRight = false;

                directorToUp = false;

                directorToDown = false;

            }
            else if (y > x && y > -x)
            {// up

                directorToLeft = false;

                directorToRight = false;

                directorToUp = true;

                directorToDown = false;

            }
            else
            {
                // down

                directorToLeft = false;

                directorToRight = false;

                directorToUp = false;

                directorToDown = true;

            }
            if (!EventSystem.current.IsPointerOverGameObject() && !Input.GetKey(KeyCode.LeftControl))
            {
                float speedx = (Input.mousePosition - Roundmouspo).magnitude;
                Roundmouspo = Input.mousePosition;

                if (dragging == true)
                {

                    Vector3 newPosition = Vector3.zero;

                    float speed = 0;

                    if (directorToLeft == true)
                    {

                        newPosition = new Vector3(0, -1, 0);

                        speed = speedx * 2;

                        //print("left");

                    }

                    if (directorToRight == true)
                    {

                        speed = speedx * 2;

                        newPosition = new Vector3(0, 1, 0);

                        //print("right");

                    }

                    if (directorToUp == true)
                    {

                        //speed = speedx;

                        //newPosition = new Vector3(1, 0, 0);

                        // print("up");

                    }

                    if (directorToDown == true)
                    {

                        //speed = speedx;

                        //newPosition = new Vector3(-1, 0, 0);

                        //print("down");

                    }

                    if (directorToLeft || directorToRight)
                    {
                        mCamTransform.RotateAround(targetModel.transform.localPosition, newPosition, speed * Time.deltaTime);
                    }
                    if (directorToUp || directorToDown)
                    {
                        Camera.main.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y"));
                    }
                }
            }
            first = Input.mousePosition;
        }
    }
    /*****************围绕物体旋转****************/
    #endregion

    #region 双击地面函数
    void OnGUI()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Event Mouse = Event.current;
            if (!mviewbool)
            {
                if (Mouse.isMouse && Mouse.type == EventType.MouseDown && Mouse.clickCount == 2)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitxxx, 1000, clicklayer))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            doublegame = hitxxx.collider.gameObject;
                            //Debug.Log(doublegame.name);
                            if (doublegame.name == "SM_Ground")
                            {
                                douposstart = mPlayer.transform.position - new Vector3(0, 0, 0);
                                douposend = new Vector3(hitxxx.point.x, 2, hitxxx.point.z) - new Vector3(0, 0, 0);
                                targetdob = Instantiate(doubleTarget, hitxxx.point + hitxxx.normal, hitxxx.transform.rotation) as GameObject;

                                cavecter = Camera.main.transform.localPosition;
                                Camera.main.transform.LookAt(targetdob.transform);
                                //mCameraRot.eulerAngles = meulerAngle;
                                //mCamTransform.localRotation = mCameraRot;

                                moveDouble = true;
                                firstbool = true;
                            }
                        }
                    }
                }
            }
        }
        /*****************双击切换视角****************/
    }

    /*****************双击切换视角****************/
    void DoubleMovePlayer()
    {
        if (moveDouble)
        {
            timex += 1f / 2 * Time.deltaTime;
            this.transform.position = Vector3.Lerp(douposstart, douposend, timex);

            Camera.main.transform.localPosition = Vector3.Lerp(cavecter, new Vector3(0,0,0), timex);

            if (douposend == this.transform.position)
            {
                meulerAngle = Camera.main.transform.localEulerAngles;
                //Debug.Log(meulerAngle);
                //camebool = true;
                moveDouble = false;
                
                this.transform.position = new Vector3(douposend.x, 2, douposend.z);
                timex = 0;

                camebool = true;

            }
        }  
    }

    void CamMoveViewCamera()
    {
        if (camebool)
        {
            if (Rotnum >= hspeed && Rotnum < hspeed + 50)
            {
                if (meulerAngle.y > 180)
                {
                    mCameraRot.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x - meulerAngle.x / 50, Camera.main.transform.eulerAngles.y + (360- meulerAngle.y) / 50, Camera.main.transform.eulerAngles.z - meulerAngle.z / 50);
                }
                else
                {
                    mCameraRot.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x - meulerAngle.x / 50, Camera.main.transform.eulerAngles.y -  meulerAngle.y / 50, Camera.main.transform.eulerAngles.z - meulerAngle.z / 50);
                }
                Camera.main.transform.localRotation = mCameraRot;
               
                //Debug.Log(Rotnum + "     " + meulerAngle.x / hspeed);
                if (Camera.main.transform.localPosition.y - yyy / 50 > 0)
                {
                    Camera.main.transform.localPosition = new Vector3(0, Camera.main.transform.localPosition.y - yyy / 50, 0);
                }
                else
                {
                    Camera.main.transform.localPosition = new Vector3(0, 0, 0);
                }
                   
            }
            if(Rotnum >= hspeed + 50)
            {
                Rotnum = 0;

                camebool = false;
                mviewbool = true;
                //mCameraRot.eulerAngles = new Vector3(0, 0, 0.0f);
                //Camera.main.transform.localRotation = mCameraRot;
                Camera.main.transform.localPosition = new Vector3(0, 0, 0);

                firstbool = false;
                if (targetdob)
                {
                    Destroy(targetdob);
                }
                mCameraRot.eulerAngles = new Vector3(90, 0, 0.0f);
                cameraMini.transform.localRotation = mCameraRot;
                cameraMini.transform.localPosition = new Vector3(0, 50, 0);
            }
            Rotnum++;
        }
    }
    /*****************双击切换视角****************/
    #endregion

    #region 转Vector3
    /*******************转换vc3d******************/
    public static Vector3 Parse(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }
    /*******************转换vc3d******************/
    #endregion

    #region 复位操作
    /*******************Player复位******************/
    public void reSetPlayer()
    {
        m_Transform.transform.position = new Vector3(-3, 2, -80);
        mCameraRot.eulerAngles = new Vector3(0, 0, 0);
        m_Transform.transform.localRotation = mCameraRot;
        Camera.main.transform.localPosition = new Vector3(0, 152, 0);
        mCameraRot.eulerAngles = new Vector3(60, 0, 0);
        Camera.main.transform.localRotation = mCameraRot;
        mCameraRot.eulerAngles = new Vector3(0, 0, 0);
        cameraMini.transform.localRotation = mCameraRot;
        cameraMini.transform.localPosition = new Vector3(0, 0, -30);
    }
    /*******************Player复位******************/
    #endregion

    #region 退出游戏
    /*******************退出游戏******************/
    public void QuitGame()
    {
        Application.Quit();
    }
    /*******************退出游戏*****************/
    #endregion

    #region AB资源加载
    /*******************AssetBundle******************/
    int build26 = 0;
    public void InstanceVoid(GameObject obj, string aName,int index)
    {
        //Debug.Log("--------------");
        GameObject ob = Instantiate(obj);
        obj = ob;
        
        switch (index)
        {
            case 0:
                DQGame = ob;
                break;
            case 1:
                build26++;
                break;
            case 2:
                break;
        }

        GameObject temob = assetBundleParent.transform.Find(aName + "Child").gameObject;
        obj.transform.SetParent(temob.transform);
        /*
        if (build26 == 26)//解决草地打包layar加不上的问题
        {
            obj.layer = 0;
        }
        */
        obj.layer = 0;
        //获取所有Floor层 加入到FlooAll里
        if (index == 2)
        {
            obj.tag = "Floor";
            
            int lt = obj.name.Length;
            if ( lt >= 6)
            {
                int inFor = 0;//楼层数
                string strIndex = obj.name.Substring(5, 1);
                Transform[] father = obj.GetComponentsInChildren<Transform>();
                foreach (var child in father)
                {
                    //Debug.Log(child.name);
                    child.tag = "Floor";
                }

                //print("strIndex === " + strIndex);
                switch (strIndex)
                {
                    case "1":
                        inFor = 3;
                        break;
                    case "2":
                        inFor = 3;
                        break;
                    case "3":
                        inFor = 4;
                        break;
                    case "4":
                        inFor = 3;
                        break;
                    case "5":
                        inFor = 4;
                        break;
                    case "6":
                        inFor = 3;
                        break;
                    default:
                        break;

                }

                for (int i = 1; i <= inFor; i++)
                {
                    string f_Path = "";
                    if(i == inFor)
                    {
                        f_Path = "Floor" + strIndex + "_" + "wd";
                    }
                    else
                    {
                        f_Path = "Floor" + strIndex + "_" + i + "c";
                    }
                    GameObject oFloor = obj.transform.Find(f_Path).gameObject;
                    FloorAll.FloorallList.Add(oFloor);
                }
                if(strIndex == "6")
                {
                    FloorAll.instance.SetFloorObList();
                }
            }
        }else
        {
            obj.tag = "Untagged";
        }
    }
    /*******************AssetBundle******************/
    #endregion
}