using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAll : MonoBehaviour {

    public GameObject Objectview;

    public void CloseButtonView()
    {
        Objectview.SetActive(false);
    }
}
