using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyTips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //IPointerEnterHandler 当鼠标进入对象时
    //IPointerExitHandler 当鼠标退出对象时
    //IPointerDownHandler 当鼠标点下对象时
    //IPointerUpHandler 当鼠标抬起时
    //IPointerClickHandler 当鼠标点击时
    //IBeginDragHandler 鼠标开始拖动时
    //IDragHandler 鼠标拖动时
    //IEndDragHandler 拖动结束时
    //IScrollHandler 鼠标滚轮时
    [SerializeField]
    private Texture img;

    private string uiNameTips = null;
    private string u3DNameTips = null;

    public static bool isShowTips = false;

    public Font font;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("---------------------- " + name);
        //Invoke("CanEnabledTips", 1);
        //isShowTips = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("---------------------" + name);
        CancelInvoke("CanEnabledTips");

        isShowTips = false;
    }

    void CanEnabledTips()
    {
        isShowTips = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CancelInvoke("CanEnabledTips");
            isShowTips = false;
        }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnMouseEnterXXX();
        }
        
    }

   

    private void OnGUI()
    {
        if (isShowTips)
        {
            
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("u3DNameTips-=====" + u3DNameTips);
                GUI.Label(new Rect(Input.mousePosition.x-20, Screen.height - Input.mousePosition.y-30, 100, 40), img);
                GUI.Label(new Rect(Input.mousePosition.x-12, Screen.height - Input.mousePosition.y-32, 100, 40), u3DNameTips);
                GUI.skin.label.font = font;
            }
            else
            {
                GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y-20, 100, 60), uiNameTips);
                GUI.skin.label.font = font;
            }
        }
    }

    private bool inBool = false;
    private void OnMouseEnterXXX()
    {
        if (Menu.configbool || Menu.isUIopen) return;

        if (!EventSystem.current.IsPointerOverGameObject() )
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log(hitInfo.collider.name);
                string nm = hitInfo.collider.name;
                //Debug.Log("nm==== " + nm + "---- " + nm.Length);
                if (nm.Length >= 13)
                {
                    switch (nm.Substring(0,13))
                    {
                        case "build23(Clone":
                            u3DNameTips = "接待室";
                            inBool = true;
                            break;
                        case "build5(Clone)":
                            u3DNameTips = "仓库";
                            inBool = true;
                            break;
                        case "build15(Clone":
                            u3DNameTips = "休息室";
                            inBool = true;
                            break;
                        
                        case "build36(Clone":
                            u3DNameTips = "茶水间";
                            inBool = true;
                            break;
                        case "build22(Clone":
                            u3DNameTips = "档案室";
                            inBool = true;
                            break;
                        case "build6(Clone)":
                            u3DNameTips = "活动室";
                            inBool = true;
                            break;
                        case "build30(Clone":
                            u3DNameTips = "门卫处";
                            inBool = true;
                            break;
                        case "build25(Clone":
                            u3DNameTips = "门卫处";
                            inBool = true;
                            break;
                        default:
                            inBool = false;
                            CancelInvoke("CanEnabledTips");
                            isShowTips = false;
                            break;
                    }
                }
                else
                {
                    if (nm.Length > 6)
                    {
                        switch (nm.Substring(0, 6))
                        {
                            case "Floor1":
                                u3DNameTips = "卫生室";
                                inBool = true;
                                break;
                            case "Floor2":
                                u3DNameTips = "监区";
                                inBool = true;
                                break;
                            case "Floor3":
                                u3DNameTips = "办公区";
                                inBool = true;
                                break;
                            case "Floor4":
                                u3DNameTips = "办公区";
                                inBool = true;
                                break;
                            case "Floor5":
                                u3DNameTips = "办公区";
                                inBool = true;
                                break;
                            case "Floor6":
                                u3DNameTips = "办公区";
                                inBool = true;
                                break;
                            case "Floor7":
                                u3DNameTips = "盥洗室";
                                inBool = true;
                                break;
                            default:
                                inBool = false;
                                CancelInvoke("CanEnabledTips");
                                isShowTips = false;
                                break;
                        }
                    }else
                    {
                        inBool = false;
                        CancelInvoke("CanEnabledTips");
                        isShowTips = false;
                        return;
                    }
                }
            }
            if (inBool && !isShowTips)
            {
                Invoke("CanEnabledTips", 1);
                isShowTips = false;
            }
            
        }

        
    }

    private void OnMouseExit()
    {
        uiNameTips = null;
        CancelInvoke("CanEnabledTips");
        isShowTips = false;
    }

    public void TipsNameStart(string namex)
    {
        Invoke("CanEnabledTips", 1);
        isShowTips = false;
        uiNameTips = namex;
    }

    public void TipsNameEnd()
    {
        CancelInvoke("CanEnabledTips");
        isShowTips = false;
    }
}
