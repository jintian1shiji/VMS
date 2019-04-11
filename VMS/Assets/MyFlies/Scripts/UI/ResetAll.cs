using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAll : MonoBehaviour {

    public List<GameObject> listgameobject;
    public List<Vector3> StratPos;
    public Vector3 CameraRot;
    public GameObject playero;
    public Camera MiniObject;
    public Camera MainCame;
    public GameObject mpGame;

    Quaternion rotations = Quaternion.identity;
    Vector3 eulerAngle = Vector3.zero;

    public GameObject Camera001;
    public GameObject AllGame001;
    public GameObject AllGame002;

    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () {
        //foreach (GameObject element in listgameobject)
        //{
        //    Vector3 pos = element.transform.position;
        //    StratPos.Add(pos);
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetStart()
    {
        int i = 0;
        PlayerAllController.mviewbool = true;
        MiniObject.transform.localPosition = new Vector3(0, 150, 0);
        mpGame.SetActive(true);
        //MiniObject.fieldOfView = 60;
        //MainCame.fieldOfView = 60;

        //AllGame001.transform.localPosition = new Vector3(0, 0, 0);
        //AllGame002.transform.localPosition = new Vector3(0, 0, 0);
        Camera001.SetActive(true);
        Camera001.transform.localPosition = new Vector3(0, 2, 0);

        foreach (GameObject element in listgameobject)
        {
            if (i==0)
            {
                rotations.eulerAngles = new Vector3(0, 0, 0.0f);
                element.transform.localRotation = rotations;
                element.transform.localPosition = new Vector3(0, 0, 0);
                playero.transform.localRotation = rotations;

            }
            else
            {
                element.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(StratPos[i].x, StratPos[i].y, StratPos[i].z);
            }   
            i++;
        }
    }
}
