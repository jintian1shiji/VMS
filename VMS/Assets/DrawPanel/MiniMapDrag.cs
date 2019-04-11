using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapDrag : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    #region 移动小地图相关组件
    //移动条初始位置
    private Vector3 staloc;
    //小地图背景框
    private GameObject miniMap;
    //移动条
    private GameObject moveImage;
    //小地图相机
    private Camera miniCamera;
    //玩家
    private GameObject mPlayer;
    //四元数
    Quaternion mCameraRot = Quaternion.identity;
    #endregion

    void Start () {
        #region 获取移动小地图相关组件
        miniMap = transform.parent.transform.parent.gameObject;
        moveImage = gameObject;
        miniCamera = FindObjectOfType<Jump>().transform.Find("Main Camera/CameraMini").gameObject.GetComponent<Camera>();
        mPlayer = FindObjectOfType<Jump>().gameObject;
        #endregion
    }

    void Update () {
        #region 点击小地图跳转
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Input.mousePosition.x > Screen.width - miniMap.transform.GetComponent<RectTransform>().sizeDelta.x && Input.mousePosition.y > Screen.height - miniMap.transform.GetComponent<RectTransform>().sizeDelta.y)
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = miniCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.name == "PlanePlane")
                {
                    return;
                }

                //GameObject gameObj = hitInfo.collider.gameObject;
                mPlayer.transform.localPosition = hitInfo.point;
                mCameraRot.eulerAngles = new Vector3(90, 0, 0.0f);
                Camera.main.transform.localRotation = mCameraRot;
            }
        }
        #endregion
    }
    #region 移动拖拽
    //结束拖拽移动
    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    //开始拖拽移动
    public void OnBeginDrag(PointerEventData eventData)
    {
        staloc = Input.mousePosition;
    }
    //拖拽移动中
    public void OnDrag(PointerEventData eventData)
    {
        //if (PlayerAllController.mviewbool)
        //{
        //    return;
        //}
        Vector3 miniMapSize = miniMap.transform.GetComponent<RectTransform>().sizeDelta;
        Vector3 moveImageSize = moveImage.transform.GetComponent<RectTransform>().sizeDelta;
        if (Input.mousePosition.x > Screen.width - miniMapSize.x + 2 && Input.mousePosition.x < Screen.width - 2)//在小地图X轴框内
        {
            if (Input.mousePosition.y > Screen.height - miniMapSize.y + 1 && Input.mousePosition.y < Screen.height - 1)//在小地图Y轴框内
            {
                transform.localPosition = Input.mousePosition - staloc;

                Camera.main.transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * -7);
                Camera.main.transform.Translate(Vector3.down * Input.GetAxis("Mouse Y") * -7);
                //Camera.main.transform.Translate(Vector3.forward * Input.GetAxis("Mouse Y") * -7);
            }
        }
    }
    #endregion
}
