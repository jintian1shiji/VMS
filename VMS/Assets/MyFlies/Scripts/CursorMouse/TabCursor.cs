using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TabCursor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    EventSystem system;
    private bool _isSelect = false;
    private bool _isGetOneMes = false;
    void Start()
    {
        system = EventSystem.current;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        _isSelect = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        _isSelect = true;
    }

    void Update()
    {
        #region
        //监测Tab按下，此时应该选择下一个UI控件
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;//下一个ui控件
            Selectable current = null;//当前的ui控件
            //验证当前是否有正在选中的物体
            if (system.currentSelectedGameObject != null)
            {   //验证当前选中的物体在层次列表里是否可用
                if (system.currentSelectedGameObject.activeInHierarchy)
                {
                    current = system.currentSelectedGameObject.GetComponent<Selectable>();
                }
            }
            if (current != null)
            {
                //当左(右)shift被按住，并且伴随着，点击tab键，光标依次向上移动
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    next = current.FindSelectableOnUp();
                    if (next == null)
                    {
                        next = current.FindSelectableOnLeft();
                    }
                }
                else
                {
                    next = current.FindSelectableOnDown();
                    if (next == null)
                    {
                        next = current.FindSelectableOnRight();
                    }
                }
            }
            else
            {
                //如果没有 “当前正在选择的物体”，那么选择第一个可选项
                if (Selectable.allSelectables.Count > 0)
                {
                    next = Selectable.allSelectables[0];
                }
            }
            if (next != null)
            {
                next.Select();
            }
        }
        #endregion
    }
}