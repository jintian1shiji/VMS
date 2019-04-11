using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScRectView : MonoBehaviour {
    private int intview;
    public List<GameObject> viewList;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnButton00F()
    {
        intview = 0;
        ListViewRun();
    }
    public void OnButton01F()
    {
        intview = 1;
        ListViewRun();
    }
    public void OnButton02F()
    {
        intview = 2;
        ListViewRun();
    }
    public void OnButton03F()
    {
        intview = 3;
        ListViewRun();
    }
    public void OnButton04F()
    {
        intview = 4;
        ListViewRun();
    }
    public void OnButton05F()
    {
        intview = 5;
        ListViewRun();
    }
    public void OnButton06F()
    {
        intview = 6;
        ListViewRun();
    }
    public void OnButton07F()
    {
        intview = 7;
        ListViewRun();
    }
    public void OnButton08F()
    {
        intview = 8;
        ListViewRun();
    }
    public void OnButton09F()
    {
        intview = 9;
        ListViewRun();
    }
    public void OnButton10F()
    {
        intview = 10;
        ListViewRun();
    }

    void ListViewRun()
    {
        int xx = 0;
        foreach (GameObject elenment in viewList)
        {
            if (xx == intview)
            {
                elenment.SetActive(true);
            }
            else
            {
                elenment.SetActive(false);
            }
            xx++;
        }
    }
    
    public void SelectDropdown()
    {

    }
}
