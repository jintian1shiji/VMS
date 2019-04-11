using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {
    private Vector3 staloc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDrag()
    {
        staloc = Input.mousePosition - transform.localPosition;
    }

    public void Drag()
    {
        transform.localPosition = Input.mousePosition - staloc;
    }

}
