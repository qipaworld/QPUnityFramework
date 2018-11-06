using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

//给空间添加监听事件要实现的一些接口
public class ClickPopUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	
    //当鼠标按下时调用 接口对应  IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
    }
    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    public void OnPointerUp(PointerEventData eventData)
    {
        CloseUI();
    }
    public void CloseUI()
    {
        UIData uiData = transform.parent.GetComponent<UIData>();
        if (uiData.GetOnClickPop())
        {
            UIController.Instance.Pop(transform.parent.GetComponent<UIData>().uiName);
        }
    }
}