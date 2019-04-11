using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IDragHandler
{

    // 定义事件代理
    public delegate void UIEventProxy(GameObject gb);

    // 鼠标点击事件
    public event UIEventProxy OnClick;

    // 鼠标进入事件
    public event UIEventProxy OnMouseEnter;

    // 鼠标滑出事件
    public event UIEventProxy OnMouseExit;

    // 鼠标按下事件
    public event UIEventProxy OnMouseDown;

    //鼠标抬起事件
    public event UIEventProxy OnMouseUp;

    //鼠标移动事件
    public event UIEventProxy OnMouseDrag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
            OnClick(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnMouseEnter != null)
            OnMouseEnter(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnMouseExit != null)
            OnMouseExit(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnMouseDrag != null)
            OnMouseDrag(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnMouseDown != null)
            OnMouseDown(this.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnMouseUp != null)
            OnMouseUp(this.gameObject);
    }
}