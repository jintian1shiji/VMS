//-----------------------------------------------------------------  
//1，客户端主角的移动控制，使用WSAD键控制上下左右移动，使用鼠标右键控制旋转。  
//2，左手坐标系。  
//3，欧拉角使用的单位是角度，不是弧度。  
//-----------------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------------------------------  
public class FiMainPlayerMoveCtrl : MonoBehaviour
{
    public float moveSpeed = 30.0f;
    public float rotateSpeed = 0.2f;
    public float mouseWheelSpeed = 50.0f;

    public static Vector3 kUpDirection = new Vector3(0.0f, 1.0f, 0.0f);

    //记录角色的旋转角度。  
    //以上方向为轴的旋转角度。  
    private float m_fEulerAngleUpDir = 0.0f;
    //以右方向为轴的旋转角度。  
    //这个欧拉角不会施加在角色身上，当摄像机聚焦在本角色上时，会施加到摄像机上。  
    private float m_fEulerAngleRightDir = 0.0f;
    //当摄像机聚焦在本角色上时，记录本角色与摄像机之间的直线距离。  
    private float m_fDistanceFromCamera = 100.0f;

    //控制角色旋转的成员变量。  
    private float m_fLastMousePosX = 0.0f;
    private float m_fLastMousePosY = 0.0f;
    private bool m_bMouseRightKeyDown = false;

    //记录本帧内Transform是否发生了变化。  
    //当摄像机聚焦在本角色上时，只有本Transform发生了变化，才更新摄像机的Transform。  
    private bool m_bTransformChanged = false;

    //-----------------------------------------------------------------  
    void Start()
    {
        Vector3 kEulerAngles = transform.eulerAngles;
        m_fEulerAngleRightDir = kEulerAngles.x;
        m_fEulerAngleUpDir = kEulerAngles.y;
    }
    //-----------------------------------------------------------------  
    void Update()
    {
        m_bTransformChanged = false;

        //判断旋转  
        if (Input.GetMouseButtonDown(1)) //鼠标右键刚刚按下了  
        {
            if (m_bMouseRightKeyDown == false)
            {
                m_bMouseRightKeyDown = true;
                Vector3 kMousePos = Input.mousePosition;
                m_fLastMousePosX = kMousePos.x;
                m_fLastMousePosY = kMousePos.y;
            }
        }
        else if (Input.GetMouseButtonUp(1)) //鼠标右键刚刚抬起了  
        {
            if (m_bMouseRightKeyDown == true)
            {
                m_bMouseRightKeyDown = false;
                m_fLastMousePosX = 0;
                m_fLastMousePosY = 0;
            }
        }
        else if (Input.GetMouseButton(1)) //鼠标右键处于按下状态中  
        {
            if (m_bMouseRightKeyDown)
            {
                Vector3 kMousePos = Input.mousePosition;
                float fDeltaX = kMousePos.x - m_fLastMousePosX;
                float fDeltaY = kMousePos.y - m_fLastMousePosY;
                m_fLastMousePosX = kMousePos.x;
                m_fLastMousePosY = kMousePos.y;

                m_fEulerAngleRightDir -= (fDeltaY * rotateSpeed);
                m_fEulerAngleUpDir -= -(fDeltaX * rotateSpeed);

                Vector3 kNewEuler = new Vector3(0.0f, m_fEulerAngleUpDir, 0.0f);
                transform.eulerAngles = kNewEuler;

                m_bTransformChanged = true;
            }
        }

        //判断位移  
        float fMoveDeltaX = 0.0f;
        float fMoveDeltaZ = 0.0f;
        float fDeltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            fMoveDeltaX -= moveSpeed * fDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            fMoveDeltaX += moveSpeed * fDeltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            fMoveDeltaZ += moveSpeed * fDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            fMoveDeltaZ -= moveSpeed * fDeltaTime;
        }
        if (fMoveDeltaX != 0.0f || fMoveDeltaZ != 0.0f)
        {
            Vector3 kForward = transform.forward;
            Vector3 kRight = Vector3.Cross(kUpDirection, kForward);
            Vector3 kNewPos = transform.position;
            kNewPos += kRight * fMoveDeltaX;
            kNewPos += kForward * fMoveDeltaZ;
            //把Y值设置成0，只能在平面上移动。  
            kNewPos.y = 0.0f;
            transform.position = kNewPos;

            m_bTransformChanged = true;
        }

        float fWheel = Input.GetAxis("Mouse ScrollWheel");
        if (fWheel != 0.0f)
        {
            m_fDistanceFromCamera -= fWheel * mouseWheelSpeed;
            m_bTransformChanged = true;
        }
    }
    //-----------------------------------------------------------------  
    public float GetEulerAngleRightDir()
    {
        return m_fEulerAngleRightDir;
    }
    //-----------------------------------------------------------------  
    public float GetEulerAngleUpDir()
    {
        return m_fEulerAngleUpDir;
    }
    //-----------------------------------------------------------------  
    public float GetDistanceFromCamera()
    {
        return m_fDistanceFromCamera;
    }
    //-----------------------------------------------------------------  
    public bool IsTransformChanged()
    {
        return m_bTransformChanged;
    }
}
//-----------------------------------------------------------------  