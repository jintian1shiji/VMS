using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloorAll : MonoBehaviour {
    public static FloorAll instance;
    //资源管理器
    private ManagerVars vars;

    #region 楼层组件
    //所有楼层初始位置
    private List<Vector3> floorPos = new List<Vector3>();
    //楼层按钮列表组件
    private List<GameObject> floorobject = new List<GameObject>();
    //楼层移动标识，对外
    public static bool foolbool = false;
    //楼中层标识
    private int iDex = 0;
    //楼层移动完成标识
    private bool finshk;
    //移动楼层标识
    private bool movebool = false;
    //楼层一次移动步数
    private int speed = 5;
    //楼层标识
    private int FloorInt = 0;
    private List<GameObject> FloorLayer = new List<GameObject>();
    //所有可以看见室内的楼层列表 需要实列化的时候赋值
    public static List<GameObject> FloorallList = new List<GameObject>();
    //最初楼层开始坐标
    private List<Vector3> FloorLayerStartPos = new List<Vector3>();
    //最初楼层结束坐标
    private List<Vector3> FloorLayerEndPos = new List<Vector3>();
    //临时楼层开始坐标
    private List<Vector3> floorStartpos = new List<Vector3>();
    //临时楼层结束坐标
    private List<Vector3> flooreEndpos = new List<Vector3>();
    //楼层移动速度
    float mMoveTime = 1;
    //楼层移动初始时间点
    float timex = 0;
    //楼层移动状态
    bool movefloor = false;
    //楼层是否可移动标识
    bool canmove = false;
    //存储该移动楼层位置
    private List<Vector3> posfloor = new List<Vector3>();
    //点击的楼标识
    public static int floorintX = 0;
    //楼层按钮列表
    private List<Button> btnList = new List<Button>();
    //中间按钮背景底图
    private Sprite deimga;
    #endregion

    private void Awake()
    {
        //获取资源管理器
        vars = ManagerVars.GetManagerVars();

        #region 获取楼层组件
        //获取UI组件
        GameObject uiParent = FindObjectOfType<OpenCloseMiniMap>().transform.Find("UI").gameObject;
        //获取楼层按钮列表组件
        for (int i = 1; i < 6; i++)
        {
            Button obBtn = uiParent.transform.Find("CanvasOne/MidMenu/FloorAll/Floor" + i).GetComponent<Button>();
            obBtn.onClick.AddListener(FloorBT);
            floorobject.Add(obBtn.gameObject);
            btnList.Add(obBtn);
        }
        //获取中间按钮背景底图
        deimga = vars.Defaultimage;

        //获取楼层初始坐标
        for (int i = 0; i < 6; i++)
        {
            Vector3 tempPos;
            switch (i)
            {
                case 0:
                    tempPos = new Vector3(0, 40, 0);
                    break;
                case 1:
                    tempPos = new Vector3(-160, 40, 0);
                    break;
                case 2:
                    tempPos = new Vector3(-80, 40, 0);
                    break;
                case 3:
                    tempPos = new Vector3(0, 40, 0);
                    break;
                case 4:
                    tempPos = new Vector3(80, 40, 0);
                    break;
                case 5:
                    tempPos = new Vector3(160, 40, 0);
                    break;
                default:
                    tempPos = new Vector3(0, 40, 0);
                    break;
            }
            floorPos.Add(tempPos);
        }

        #endregion


    }

    void Start () {
        instance = this.GetComponent<FloorAll>();

        #region 初始化一些数据
        FloorLayerStartPos.Clear();
        FloorLayerEndPos.Clear();
        #endregion
    }

    #region 楼层移动函数
    void Update () {
        MoveFloorFunction();//楼层移动

        if (movebool)
        {
            if (!foolbool)
            {
                iDex = 0;
                finshk = true;
                foreach (GameObject element in floorobject)
                {
                    Vector3 pos = element.GetComponent<RectTransform>().anchoredPosition3D;  
                    if (pos.x != floorPos[iDex + 1].x)
                    {
                        if (iDex < 2)
                        {
                            element.transform.Translate(-speed, 0, 0);
                        }

                        else if (iDex > 2)
                        {
                            element.transform.Translate(speed, 0, 0);
                        }
                        else
                        {

                        }
                        finshk = false;
                        
                    }
                    iDex++;
                }
                if (finshk)
                {
                    foolbool = true;
                    movebool = false;
                }
            }
            else
            {
                iDex = 0;
                finshk = true;
                foreach (GameObject element in floorobject)
                {
                    Vector3 pos = element.GetComponent<RectTransform>().anchoredPosition3D;
                    if (pos.x != floorPos[3].x)
                    {
                        //element.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x-1, pos.y, pos.z);
                        if (iDex < 2)
                        {
                            element.transform.Translate(speed, 0, 0);
                        }

                        else if (iDex > 2)
                        {
                            element.transform.Translate(-speed, 0, 0);
                        }
                        else
                        {

                        }
                       
                        finshk = false;
                    }
                    iDex++;
                }
                if (finshk)
                {
                    foolbool = false;
                    movebool = false;
                    foreach (GameObject element in floorobject)
                    {
                        element.SetActive(false);
                    }
                }
            }
        }
		
	}

    public void SetFloorObList()
    {
        foreach (GameObject elenment in FloorallList)
        {
            FloorLayerStartPos.Add(elenment.transform.localPosition);
            FloorLayerEndPos.Add(elenment.transform.localPosition + new Vector3(0, 0, 50));
        }
    }

    //点击楼层按钮
    void ClickWahtFloor()
    {
        foreach (Button el in btnList)
        {
            el.GetComponent<Image>().sprite = deimga;
        }
        switch (ClickOpen.ClickFllor)
        {
            case 0:
                break;
            case 1:
                ClickOpen.floor1 = FloorInt;
                break;
            case 2:
                ClickOpen.floor2 = FloorInt;
                break;
            case 3:
                ClickOpen.floor3 = FloorInt;
                break;
            case 4:
                ClickOpen.floor4 = FloorInt;
                break;
            case 5:
                ClickOpen.floor5 = FloorInt;
                break;
            case 6:
                ClickOpen.floor6 = FloorInt;
                break;
            default:break;
        }
    }

    //对应楼层按钮函数
    private void FloorBT()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        switch (obj.name)
        {
            case "Floor1":
                FloorInt = 1;
                MoveFloor(1);
                ClickWahtFloor();
                break;
            case "Floor2":
                FloorInt = 2;
                MoveFloor(3);
                ClickWahtFloor();
                break;
            case "Floor3":
                FloorInt = 3;
                MoveFloor(3);
                ClickWahtFloor();
                break;
            case "Floor4":
                FloorInt = 4;
                MoveFloor(4);
                ClickWahtFloor();
                break;
            case "Floor5":
                FloorInt = 5;
                MoveFloor(5);
                ClickWahtFloor();
                break;
            default:
                break;
        }
    }

    void MoveFloor(int index)
    {
        int i = 0;
        int x = 0;
        canmove = false;
        FloorLayer.Clear();
        floorStartpos.Clear();
        flooreEndpos.Clear();
        movefloor = false;
        timex = 0;
       // Debug.Log(index + "    " + floorintX);
        switch (floorintX)
        {
            case 0:
                for (x=0;x<3;x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);

                }
                break;
            case 1:
                for (x = 3; x < 6; x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);
                }
                break;
            case 2:
                for (x = 6; x < 10; x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);
                }
                break;
            case 3:
                for (x = 10; x < 13; x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);
                }
                break;
            case 4:
                for (x = 13; x < 17; x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);
                }
                break;
            case 5:
                for (x = 17; x < 20; x++)
                {
                    FloorLayer.Add(FloorallList[x]);
                    floorStartpos.Add(FloorLayerStartPos[x]);
                    flooreEndpos.Add(FloorLayerEndPos[x]);
                }
                break;
            default:
                break;
        }
  
        switch (index)
        {
            case 1:
                foreach(GameObject element in FloorLayer)
                {
                    Vector3 pos = element.transform.localPosition;

                    if (i == 0)
                    {
                        if (pos != floorStartpos[i])
                        {
                            element.SetActive(true);
                        }
                    }
                    else
                    {
                    }
                    i++;
                }
                canmove = true;
                break;
            case 2:
                foreach (GameObject element in FloorLayer)
                {
                    Vector3 pos = element.transform.localPosition;
                    if (i <= 1)
                    {
                        if (pos != floorStartpos[i])
                        {
                            element.SetActive(true);
                        }
                    }
                    else
                    {
                    }
                    i++;
                }
                canmove = true;
                break;
            case 3:
                foreach (GameObject element in FloorLayer)
                {
                    Vector3 pos = element.transform.localPosition;
                    if (i <= 2)
                    {
                        if (pos != floorStartpos[i])
                        {
                            element.SetActive(true);
                        }
                    }
                    else
                    {
                    }
                    i++;
                }
                canmove = true;
                break;
            case 4:
                foreach (GameObject element in FloorLayer)
                {
                    Vector3 pos = element.transform.localPosition;
                    if (i <= 3)
                    {
                        if (pos != floorStartpos[i])
                        {
                            element.SetActive(true);
                        }
                    }
                    else
                    {

                    }
                    i++;
                }
                canmove = true;
                break;
            case 5:
                foreach (GameObject element in FloorLayer)
                {
                    Vector3 pos = element.transform.localPosition;
                    if (i <= 4)
                    {
                        if (pos != floorStartpos[i])
                        {
                            element.SetActive(true);
                        }
                    }
                    else
                    {
                    }
                    i++;
                }
                canmove = true;
                break;
            default:
                break;
        }

    }


    void MoveFloorFunction()
    {
        
       int i = 0;
        if (canmove)
        {
            if (!movefloor)
            {
                posfloor.Clear();
                foreach (GameObject element in FloorLayer)
                {
                    posfloor.Add(element.transform.localPosition);
                }
                movefloor = true;
            }
            else
            {
                int xx = FloorLayer.Count;
                foreach (GameObject element in FloorLayer)
                {
                    //Debug.Log(floor01pos[i] + "    " + posfloor[i] + "   " + i);
                    if (i < FloorInt)
                    {
                        if (floorStartpos[i] != posfloor[i])
                        {
                            timex += 1f / mMoveTime * Time.deltaTime;
                            element.transform.localPosition = Vector3.Lerp(posfloor[i], floorStartpos[i], timex);
                            if (timex >= 1)
                            {
                               canmove = false;
                            }
                        }
                    }
                    else
                    {
                        if (flooreEndpos[i] != posfloor[i])
                        {
                            timex += 1f / mMoveTime * Time.deltaTime;
                            element.transform.localPosition = Vector3.Lerp(posfloor[i], flooreEndpos[i], timex);
                            if (timex >= 1)
                            {
                                canmove = false;
                                int kk = 0;
                                foreach (GameObject elementx in FloorLayer)
                                {
                                    if (kk >= FloorInt)
                                    {
                                        elementx.SetActive(false);
                                    }
                                    kk++;
                                }
                            }
                        }
                    }
                    i++;
                }
                
            }
        } 
    }
    #endregion

    #region 楼层复位
    /*******************复位楼层*******************/
    public void reSetFloor()
    {
        int ik = 0;
        foreach (GameObject elenment in FloorallList)
        {
            elenment.transform.localPosition = FloorLayerStartPos[ik];
            elenment.SetActive(true);
            ik++;
        }

        iDex = 0;
        foolbool = false;
        movebool = false;
        FloorInt = 0;
        mMoveTime = 1;
        timex = 0;
        movefloor = false;
        canmove = false;
        floorintX = 0;
}
    /*******************复位楼层*******************/
    #endregion
}
