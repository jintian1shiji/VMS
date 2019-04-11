using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModel : MonoBehaviour {

    public GameObject target;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000) && Input.GetMouseButtonDown(0))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("碰撞位置：" + hit.normal);
            target.transform.position = hit.point;
            target.transform.up = hit.normal;
            target.transform.Translate(Vector3.up * 0.1f * target.transform.localScale.y, Space.Self);
        }
    }
}
