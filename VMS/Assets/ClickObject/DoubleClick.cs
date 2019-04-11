using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour
{

    bool clickbool = false;
    Vector2 starloc;
    public List<GameObject> disenabled;
    private Vector3 startpos;
    private Vector3 scale;
    // Use this for initialization
    void Start () {
        starloc = GetComponent<RectTransform>().sizeDelta;

    }
    Vector2 local;
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        Event Mouse = Event.current;
        if (Mouse.isMouse && Mouse.type == EventType.MouseDown && Mouse.clickCount == 2)
        {
            if (!clickbool)
            {
                startpos = GetComponent<RectTransform>().anchoredPosition3D;
                scale = GetComponent<RectTransform>().sizeDelta;
                GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
                GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
                
                clickbool = true;
                foreach (GameObject elenment in disenabled)
                {
                    elenment.SetActive(false);
                }
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = starloc;
                GetComponent<RectTransform>().anchoredPosition = startpos;
                clickbool = false;
                foreach (GameObject elenment in disenabled)
                {
                    elenment.SetActive(true);
                }
            }
        }
    }
}
