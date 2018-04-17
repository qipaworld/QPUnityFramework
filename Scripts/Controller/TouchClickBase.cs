using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 添加这个组件后，就可以点击到对应layerId 的物体了。
/// 可以自己写一个类继承该组件重写OnClick方法
/// </summary>
[RequireComponent(typeof(AudioManagerBase))]
public class TouchClickBase : MonoBehaviour
{

    public int layerId = 9; //射线碰撞层编号
    int layerMask = 0; //射线碰撞层
    public int rayDraction = 30; //射线长度
    public Camera eyeCamera = null; // 视图相机
    public int MaxDraction = 55;
    Vector2 beginP;
    Vector2 endP;
    bool isTouch = false;
    int fingerId;
    public bool isAudio = false;
    float onlickTime;
    AudioManagerBase audioManager;
    void Start()
    {
        if (eyeCamera == null)
        {
            eyeCamera = Camera.main;
        }
        layerMask = (1 << layerId);
        audioManager = transform.GetComponent<AudioManagerBase>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.touchCount == 1)
        //{
        if (onlickTime > 0)
        {
            onlickTime -= Time.deltaTime;
        }
        UpdateTargetPositon();
        //}
    }
    /// <summary>
    /// 监听点击事件
    /// </summary>
    void UpdateTargetPositon()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            bool isGetTouch = false;
            if (!isTouch)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        touch = Input.GetTouch(i);
                        fingerId = touch.fingerId;
                        break;
                    }
                }
                isGetTouch = true;

            }
            else
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).fingerId == fingerId)
                    {
                        touch = Input.GetTouch(i);
                        isGetTouch = true;

                        break;
                    }
                }
            }
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                // if (isTouch)
                // {
                //    TouchEnd(endP);
                // }

                isTouch = false;
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
            if ((!isTouch||touch.phase == TouchPhase.Began)&&touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {

                TouchBegin(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {

                TouchEnd(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {

                endP = touch.position;
            }
           
        }
    }

    public void OnGUI()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            return;
#endif
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if (Event.current.type == EventType.MouseDown)
        {
            TouchBegin(Input.mousePosition);
        }
      
        else if (Event.current.type == EventType.MouseUp)
        {
            TouchEnd(Input.mousePosition);
        }
    }

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit, Vector2 point)
    {
        Ray ray = eyeCamera.ScreenPointToRay(point);
        return Physics.Raycast(ray, out hit, 30, layerMask);
    }
    ///初始化点击位置
    void TouchBegin(Vector2 point)
    {

        beginP = point;
        endP = point;
        isTouch = true;
    }

    ///点击结束，做点击处理
    void TouchEnd(Vector2 point)
    {

        if (isTouch)
        {

            float dis = (beginP - point).magnitude; //手指移动距离
            if (dis < MaxDraction)
            { //距离太大不做处理
                
                if (onlickTime <= 0)
                {
                    RaycastHit hit;
                    if (RayDetection(out hit, point))
                    {
                        OnClick(hit);
                    }
                    onlickTime = 0.2f;
                }
                
            }
        }
        isTouch = false;
    }
    public virtual void OnClick(RaycastHit hit) {
        //Debug.Log("QIPAWORLD:TouchClickBase.OnClick");
        if (isAudio)
        {
            audioManager.play();
        }
    }
}
