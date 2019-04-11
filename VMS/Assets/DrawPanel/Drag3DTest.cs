using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag3DTest : MonoBehaviour {
    RaycastHit hit;
    //被拖拽物体
    GameObject dragGameobject;
    //被允许拖拽的物体layer层
    public LayerMask draglayer;
    //在什么layer层上进行拖拽
    public LayerMask planelayer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, draglayer))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl))
            {
                dragGameobject = hit.collider.gameObject;
                //Debug.Log("11111111111111" + dragGameobject.name);
            }
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, planelayer))
        {
            if (Input.GetKey(KeyCode.Mouse0) && dragGameobject && Input.GetKey(KeyCode.LeftControl))
            {
                dragGameobject.transform.position = hit.point + hit.normal;
                //dragGameobject.transform.position = hit.point + Vector3.up;
                dragGameobject.transform.position = new Vector3(dragGameobject.transform.position.x, dragGameobject.transform.position.y - dragGameobject.transform.localScale.x/2, dragGameobject.transform.position.z);
                //Debug.Log("22222222222" + dragGameobject.name);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            dragGameobject = null;
        }
        
	}
}
