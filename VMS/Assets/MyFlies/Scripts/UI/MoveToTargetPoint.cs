using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetPoint : MonoBehaviour {

    /// <summary>
    /// 目标点
    /// </summary>
   public Vector3 targetPos;
    /// <summary>
    /// 移动速度
    /// </summary>
    float moveSpeed = 10f;
    /// <summary>
    /// 结束移动的时间
    /// </summary>
    float endMoveTm;

    void Start()
    {
        float moveTm = Mathf.Abs(transform.position.z - targetPos.z) / moveSpeed;
        endMoveTm = moveTm + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Time.time >= endMoveTm)
        {
            Debug.Log("到达目标点");
        }
    }
}
