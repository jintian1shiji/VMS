using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickOpen : MonoBehaviour {
    public static ClickOpen instance;
    //资源管理器
    private ManagerVars vars;
    

    #region 楼层组件
    /****************楼层组件***************/
    //当前楼层标识
    private int RoomNum = -1;
    private Vector3 RoomPos;
    //楼层中文列表Text组件
    private List<Text> RoomText = new List<Text>();
    //楼层英文列表Text组件
    private List<Text> RoomTextEN = new List<Text>();
    //楼层按钮列表
    private List<Button> floorBtn = new List<Button>();
    //楼层按钮默认图标
    private Sprite Defaultimage;
    //楼层按钮新图标
    private Sprite NewImage;
    //楼层中文名称
    private string ttt;
    //楼层英文名称
    private string ttten;
    //第一幢楼有三层
    public static int floor1 = 3;
    //第二幢楼有三层
    public static int floor2 = 3;
    //第三幢楼有四层
    public static int floor3 = 4;
    //第四幢楼有三层
    public static int floor4 = 3;
    //第五幢楼有四层
    public static int floor5 = 4;
    //第六幢楼有三层
    public static int floor6 = 3;
    //当前点击的楼层
    public static int ClickFllor = 0;
    /****************楼层组件***************/
    #endregion

    #region 楼层开关
    /*******************楼层开关******************/
    //楼层按钮列表对象
    private List<GameObject> floorBtnOb = new List<GameObject>();
    //楼层是否打开
    private bool floorbool = false;
    //切换楼层标识
    private int floindex = 0;
    //中间按钮组件
    private GameObject midbutton;
    //楼层按钮组件父物体
    private GameObject floobut;
    //射线
    RaycastHit hit;
    //被拖拽物体
    GameObject dragGameobject;
    //可点击楼层layer
    private LayerMask clicklayer;
    /*******************楼层开关******************/
    #endregion
    private void Awake()
    {
        #region 组件获取
        //获取资源管理器
        vars = ManagerVars.GetManagerVars();
        //获取UI组件
        GameObject uiParent = FindObjectOfType<OpenCloseMiniMap>().transform.Find("UI").gameObject;
        //获取楼层中文列表Text组件
        for (int i = 1; i < 6; i++)
        {
            RoomText.Add(uiParent.transform.Find("CanvasOne/MidMenu/FloorAll/Floor" + i + "/Text").GetComponent<Text>());
        }
        //获取楼层英文列表Text组件
        for (int i = 1; i < 6; i++)
        {
            RoomTextEN.Add(uiParent.transform.Find("CanvasOne/MidMenu/FloorAll/Floor" + i + "/TextE").GetComponent<Text>());
        }
        //获取楼层按钮列表组件
        for (int i = 1; i < 6; i++)
        {
            Button btn = uiParent.transform.Find("CanvasOne/MidMenu/FloorAll/Floor" + i).GetComponent<Button>();
            floorBtn.Add(btn);
            floorBtnOb.Add(btn.gameObject);
        }
        //获取楼层按钮默认图标
        Defaultimage = vars.Defaultimage;
        //楼层按钮新图标
        NewImage = vars.NewImage;
        //获取中间按钮组件
        midbutton = uiParent.transform.Find("CanvasOne/MidMenu/MidMenu").gameObject;
        //获取楼层按钮父物体组件
        floobut = uiParent.transform.Find("CanvasOne/MidMenu/FloorAll").gameObject;
        #endregion
    }

    void Start () {
        instance = this.GetComponent<ClickOpen>();
        //获取可点击楼层
        clicklayer = LayerMask.NameToLayer("Floor");
    }
	
	// Update is called once per frame
	void Update () {
        GoinRoom();
    }

    #region 楼层操作函数
    void GoinRoom()
    {

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (floorbool)
            {
                midbutton.SetActive(true);
                floobut.SetActive(false);
                foreach (GameObject elenment in floorBtnOb)
                {
                    elenment.SetActive(false);
                }
                FloorAll.floorintX = 0;
                floorbool = false;
                foreach (Button el in floorBtn)
                {
                    el.GetComponent<Image>().sprite = Defaultimage;
                }
            }
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, clicklayer))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                dragGameobject = hit.collider.gameObject;
                //Debug.Log(dragGameobject.name);
                if(dragGameobject.name.Length >= 5)
                {
                    if(dragGameobject.tag == "Floor")
                    {
                        switch (dragGameobject.name.Substring(0, 6))
                        {
                            case "Floor1":
                                RoomNum = 0;
                                RoomPos = new Vector3(-3, 12, (float)1.19);
                                floindex = 3;
                                ClickFllor = 1;
                                break;
                            case "Floor2":
                                RoomNum = 1;
                                RoomPos = new Vector3((float)(-1.55), 12, (float)1.19);
                                floindex = 3;
                                ClickFllor = 2;
                                break;
                            case "Floor3":
                                RoomNum = 2;
                                RoomPos = new Vector3((float)1.82, 12, (float)1.19);
                                floindex = 4;
                                ClickFllor = 3;
                                break;
                            case "Floor4":
                                RoomNum = 3;
                                RoomPos = new Vector3((float)0.32, 12, (float)2.19);
                                floindex = 3;
                                ClickFllor = 4;
                                break;
                            case "Floor5":
                                RoomNum = 4;
                                RoomPos = new Vector3((float)(-4.1), 12, (float)(3.13));
                                floindex = 4;
                                ClickFllor = 5;
                                break;
                            case "Floor6":
                                RoomNum = 5;
                                ClickFllor = 6;
                                RoomPos = new Vector3((float)(-4.1), 12, (float)(3.13));
                                floindex = 3;
                                break;
                            default:
                                RoomNum = -1;
                                RoomPos = new Vector3((float)(2.4), 12, (float)(2.28));
                                floindex = 0;
                                ClickFllor = 0;
                                break;

                        }
                        switch (ClickFllor)
                        {
                            case 1:
                                floorBtn[floor1 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            case 2:
                                floorBtn[floor2 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            case 3:
                                floorBtn[floor3 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            case 4:
                                floorBtn[floor4 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            case 5:
                                floorBtn[floor5 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            case 6:
                                floorBtn[floor6 - 1].GetComponent<Image>().sprite = NewImage;
                                break;
                            default:
                                break;
                        }
                    }else
                    {
                        RoomNum = -1;
                        RoomPos = new Vector3((float)(2.4), 12, (float)(2.28));
                        floindex = 0;
                        ClickFllor = 0;
                    }
                }else
                {
                    RoomNum = -1;
                    RoomPos = new Vector3((float)(2.4), 12, (float)(2.28));
                    floindex = 0;
                    ClickFllor = 0;
                }
                
                if (RoomNum >= 0 && RoomNum <= 5)
                {
                    if (!floorbool)
                    {
                        int oo = 0;
                        midbutton.SetActive(false);
                        floobut.SetActive(true);
                        foreach (GameObject elenment in floorBtnOb)
                        {
                            if (oo < floindex)
                            {
                                switch(oo)
                                {
                                    case 0:
                                        ttt = "一层";
                                        ttten = "Level one";
                                        break;
                                    case 1:
                                        ttt = "二层";
                                        ttten = "Second floor";
                                        break;
                                    case 2:
                                        ttt = "三层";
                                        ttten = "Third floor";
                                        break;
                                    case 3:
                                        ttt = "四层";
                                        ttten = "Fourth floor";
                                        break;
                                    case 4:
                                        ttt = "五层";
                                        ttten = "Fifth floor";
                                        break;
                                    default:
                                        break;
                                }
                                if (oo == floindex - 1)
                                {
                                    ttt = "楼顶";
                                    ttten = "Roof";
                                }
                                //Debug.Log(ttt);
                                
                                elenment.SetActive(true);
                                RoomText[oo].text = ttt;
                                RoomTextEN[oo].text = ttten;
                            }
                            oo++;
                        }
                        floorbool = true;
                    }  
                    else
                    {
                        int oo = 0;
                        foreach (GameObject elenment in floorBtnOb)
                        {
                            if (oo < floindex)
                            {
                                elenment.SetActive(true);
                            }
                            else
                            {
                                elenment.SetActive(false);
                            }
                            oo++;
                        }
                        floorbool = true;
                    }
                    FloorAll.floorintX = RoomNum;  
                }
                //Debug.Log("单击");
            }
            
        }

       
    }
    #endregion

    #region 楼层复位
    /*******************楼层UI复位**********************/
    public void reSetFloorUI()
    {
        midbutton.SetActive(true);
        floobut.SetActive(false);
        floor1 = 3;
        floor2 = 3;
        floor3 = 4;
        floor4 = 3;
        floor5 = 4;
        floor6 = 3;
        ClickFllor = 0;
        floorbool = false;
        floindex = 0;
    }
    /*******************楼层UI复位**********************/
    #endregion
}
