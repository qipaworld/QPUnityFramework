using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

//给空间添加监听事件要实现的一些接口
public class TouchMove : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public RectTransform canvas;          //得到canvas的ugui坐标
	private RectTransform rectTransform;        //得到图片的ugui坐标
	Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值
	Move moveComponent;

	void Start () {
		rectTransform = GetComponent<RectTransform>();
		moveComponent = transform.GetComponent<Move> ();
	}

	//当鼠标按下时调用 接口对应  IPointerDownHandler
	public void OnPointerDown(PointerEventData eventData)
	{
		Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
		Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
		mouseUguiPos.y = 0;
		offset = rectTransform.anchoredPosition - mouseUguiPos;
		moveComponent.moveKill ();
	}

	//当鼠标拖动时调用   对应接口 IDragHandler
	public void OnDrag(PointerEventData eventData)
	{
		Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
		Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
//		Camera.main.point();
		bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);
		uguiPos.y = 0;
		Vector2 position = offset + uguiPos;
		if (isRect&&position.x<=0&&position.x>=-750)
		{
			rectTransform.anchoredPosition = offset + uguiPos;
		}
	}

	//当鼠标抬起时调用  对应接口  IPointerUpHandler
	public void OnPointerUp(PointerEventData eventData)
	{
		offset = Vector2.zero;
		moveComponent.moveAutoStart ();

//		if (rectTransform.anchoredPosition.x < -150 && rectTransform.anchoredPosition.x > -600) {
//		} else {
//			moveComponent.moveCancel ();
//		}
	}

}