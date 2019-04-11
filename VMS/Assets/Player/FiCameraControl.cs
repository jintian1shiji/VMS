//-----------------------------------------------------------------  
//1，摄像机要聚焦在GameObjectA上，那么就把本脚本作为一个组件，添加到GameObjectA中。  
//2，GameObjectA中必须包含 FiMainPlayerMoveCtrl 组件。  
//3，左手坐标系。  
//-----------------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------------------------------  
public class FiCameraControl : MonoBehaviour
{
    //本Object必须有一个 FiMainPlayerMoveCtrl 组件。  
    private FiMainPlayerMoveCtrl m_kMainPlayerMoveCtrl = null;
    //摄像机的 Transform 组件。  
    private Transform m_kMainCameraTransform = null;

    //记录是否为第一帧。  
    //如果是第一帧，必须更新摄像机的Transform，使得摄像机聚焦在本Object上。  
    private bool m_bFirstFrame = true;

    //-----------------------------------------------------------------  
    void Start()
    {
        m_kMainPlayerMoveCtrl = GetComponent<FiMainPlayerMoveCtrl>();
        m_kMainCameraTransform = Camera.main.transform;
        m_bFirstFrame = true;
    }
    //-----------------------------------------------------------------  
    void Update()
    {

    }
    //-----------------------------------------------------------------  
    void LateUpdate()
    {
        if (m_kMainPlayerMoveCtrl == null)
        {
            return;
        }
        if (m_kMainPlayerMoveCtrl.IsTransformChanged() == false)
        {
            if (m_bFirstFrame == false)
            {
                return;
            }
        }
        m_bFirstFrame = false;

        //设置摄像机的旋转。  
        float fEulerAngleRightDir = m_kMainPlayerMoveCtrl.GetEulerAngleRightDir();
        float fEulerAngleUpDir = m_kMainPlayerMoveCtrl.GetEulerAngleUpDir();
        Vector3 kEulerAngle = new Vector3(fEulerAngleRightDir, fEulerAngleUpDir, 0.0f);
        m_kMainCameraTransform.eulerAngles = kEulerAngle;

        //设置摄像机的位置。  
        Vector3 kPos = transform.position;
        float fDistance = m_kMainPlayerMoveCtrl.GetDistanceFromCamera();
        kPos -= m_kMainCameraTransform.forward * fDistance;
        m_kMainCameraTransform.position = kPos;
    }
}
//-----------------------------------------------------------------  