using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DBlick : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    UnityEvent doubleClick = new UnityEvent();
    public float Interval = 0.5f;

    private float firstClicked = 0;
    private float secondClicked = 0;

    public GameObject rawImage;
    private bool rawImageIsfull = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        secondClicked = Time.realtimeSinceStartup;

        if (secondClicked - firstClicked < Interval)
        {
            doubleClick.Invoke();
            if(!rawImageIsfull)
            {
                rawImage.GetComponent<RectTransform>().sizeDelta = new Vector3(Screen.width, Screen.height);
                rawImage.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 14,0);
                rawImageIsfull = true;
            }
            else
            {
                rawImage.GetComponent<RectTransform>().sizeDelta = new Vector3(791, 390);
                rawImage.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
                rawImageIsfull = false;
            }
        }
        else
        {
            firstClicked = secondClicked;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
