using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mark : MonoBehaviour {
    private Button btn;
	// Use this for initialization
	void Start () {
        btn = transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(BttN);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void BttN()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        print(obj);
    }
}
