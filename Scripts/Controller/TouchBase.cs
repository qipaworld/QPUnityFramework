using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using QipaWorld;
public  class TouchBase: MonoBehaviour
{
    public Vector3 beginP;
    public Vector3 endP;
    public bool isTouch = false;
    public int fingerId;
    /// <summary>
    /// 监听点击事件
    /// </summary>
    public void UpdateTargetPositon()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            bool isGetTouch = false;
            if (!isTouch)
            {
                Utils.GetBeginTouch(out touch);
                fingerId = touch.fingerId;
                isGetTouch = true;
            }
            else
            {
                isGetTouch = Utils.GetTouchByFingerId(fingerId, out touch);
            }
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (isTouch)
                {
                    TouchCanceled(endP);
                }
                return;
            }
            if (!isGetTouch)
            {
                if (isTouch)
                {
                    TouchEnd(endP);
                }
                return;
            }
            if ((!isTouch || touch.phase == TouchPhase.Began) && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                TouchBegin(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                TouchEnd(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                TouchMove(touch.position);
            }
        }
    }
    public void OnGUI()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        return;
#endif
        //if (DataManager.Instance.getData("TouchStatus").GetNumberValue("pickUp") == 1)
        //{
        //    return;
        //}
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Event.current.type == EventType.MouseDown)
        {
            TouchBegin(Input.mousePosition);
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
            TouchMove(Input.mousePosition);
        }
        else if (Event.current.type == EventType.MouseUp || Event.current.type == EventType.MouseLeaveWindow)
        {
            TouchEnd(Input.mousePosition);
        }
    }
    public virtual void TouchBegin(Vector3 point) { }
    public virtual void TouchMove(Vector3 point) { }
    public virtual void TouchEnd(Vector3 point) { }
    public virtual void TouchCanceled(Vector3 point) { }
}
