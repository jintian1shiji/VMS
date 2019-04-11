using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //创建一个射线，该射线从主摄像机中发出，而发出点是鼠标点击的位置 
            Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            //创建一个射线信息集 
            RaycastHit hit; 
            //如果碰到什么，则····  
            if (Physics.Raycast(ray, out hit))
            {
                //此时hit就是你点击的物体，你可以为所欲为了，当然hit.point可以显示此时你点击的坐标值 
                //print(hit.point);
                //print(hit.transform.name);
            }
        }
    }

}
