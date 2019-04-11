using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCmLine : MonoBehaviour {
    private Slider[] sliders = new Slider[3]; //滑块集合
    private GameObject lineOb;
    private GameObject lineCm;
    // Use this for initialization
    void Start () {
        Mark[] enemies = FindObjectsOfType<Mark>();//标记
        for(int i = 0;i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    sliders[i] = enemies[0].transform.Find("Image/SliderX").GetComponent<Slider>();
                    break;
                case 1:
                    sliders[i] = enemies[0].transform.Find("Image/SliderY").GetComponent<Slider>();
                    break;
                case 2:
                    sliders[i] = enemies[0].transform.Find("Image/SliderZ").GetComponent<Slider>();
                    break;
            }
        }
        foreach (Slider item in sliders)    //为各个Slider注册事件；
        {
            //其实Value可以不用传过去，因为这个值可以从item中获取；
            item.onValueChanged.AddListener((float value) => Slider_CG_Value(value, item));
        }
        lineOb = transform.Find("line").gameObject;
        lineCm = transform.Find("LineCm").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Slider_CG_Value(float value, Slider EventSender)
    {
        if (value > 0.5) value = value - 0.5f;
        else if (value < 0.5) value = value - 0.5f;
        else value = 0;
        //原点:0 0 0:0 6 8 10 A:2 1 1:1 5 B:2 1 -1:2 7 C:2 -1 -1:3 9 D:2 -1 1:4 11
        switch (EventSender.name)
        {
            case "SliderX":
                //Debug.Log("Slider01" + ":" + value.ToString("f2"));
                //print("--- " + lineOb.transform.localEulerAngles.z);
                lineOb.transform.localEulerAngles = new Vector3(0, value * 45, lineOb.transform.localEulerAngles.z);
                lineCm.transform.localEulerAngles = new Vector3(0, value * 45, lineCm.transform.localEulerAngles.z);
                break;
            case "SliderY":
                //Debug.Log("Slider02" + ":" + value.ToString("f2"));
                //print("--- " + lineOb.transform.localEulerAngles.y);
                lineOb.transform.localEulerAngles = new Vector3(0, lineOb.transform.localEulerAngles.y, value * 45);
                lineCm.transform.localEulerAngles = new Vector3(0, lineCm.transform.localEulerAngles.z, value * 45);
                break;
            case "SliderZ":
                //Debug.Log("Slider03" + ":" + value.ToString("f2"));
                lineCm.GetComponent<Camera>().fieldOfView = 60 + 100 * value;
                break;
        }
    }
}
