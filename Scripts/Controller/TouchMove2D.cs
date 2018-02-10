using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 添加这个组件后，该物体就可以被拖动
/// 也可以把他加到一个背景上，手动指定被拖动的物体
/// </summary>
public class TouchMove2D : MonoBehaviour {



    public BoxCollider2D Bounds = null; //移动的边界
    public Vector3 deceleration = new Vector3(1,1,0);//减速度
    public Vector3
        minVec3,
        maxVec3;
    private Vector2 beginP = Vector2.zero;//鼠标第一次落下点  
    private Vector2 endP = Vector2.zero;//鼠标第二次位置（拖拽位置）  
    private Vector3 speed = Vector3.zero;
    public Camera eyeCamera = null; // 视图相机
    public bool isUpdateTouch = true; //是否更新touch 

    public int layerId = 8; //射线碰撞层编号
    int layerMask = 0; //射线碰撞层
    public int rayDraction = 30; //射线长度
    Transform target = null; //移动目标
    TouchStatusCallback touchStatusCallback = null;
    public bool isSlide = false;
    public void Start()
    {

        if (eyeCamera == null) {
            eyeCamera = Camera.main;
        }
        if (Bounds)
        {
            minVec3 = Bounds.bounds.min;//包围盒  
            maxVec3 = Bounds.bounds.max;
        }
        else {
            DataBase GameBounds = DataManager.Instance.getData("GameBounds");
            if (GameBounds != null)
            {
                minVec3 = GameBounds.GetVectorValue("min");//包围盒  
                maxVec3 = GameBounds.GetVectorValue("max");
            }
        }
        layerMask = (1 << layerId);
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
            RaycastHit hit;
            if (RayDetection(out hit))
            { 
                MoveBegin(Input.mousePosition,hit.transform);
            }
        }
        else if (Event.current.type == EventType.MouseDrag && target != null)
        {
            Moveing(Input.mousePosition);
        }else if(Event.current.type == EventType.MouseUp||Event.current.type == EventType.MouseLeaveWindow){
            if(!isSlide){
                MoveEnd(Input.mousePosition);
            }
        }
    }
    //移动对象
    void UpdateTargetPositon()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        if(!isUpdateTouch){
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Canceled || Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    isUpdateTouch = true;
                    break;
                }
            }
        }
        if (Input.touchCount == 1){
            if (isUpdateTouch)
            {
                RaycastHit hit;
                if (RayDetection(out hit))
                { 
                    MoveBegin(Input.GetTouch(0).position,hit.transform);
                    isUpdateTouch = false;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && target != null)
            {
                Moveing(Input.GetTouch(0).position);
            }else if(Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended){
                if(!isSlide){
                    MoveEnd(Input.mousePosition);
                }
            }
        }

    }
    ///初始化位置，为接下来的move做准备
    void MoveBegin(Vector3 point,Transform t) {
        target = t;
        beginP = point;
        speed = Vector3.zero;
        touchStatusCallback = t.GetComponent<TouchStatusCallback>();
        if(touchStatusCallback != null){
            touchStatusCallback.TouchBegin(point);
        } 
        DataManager.Instance.getData("TouchStatus").SetNumberValue("pickUp",1);
    }
    ///更新目标位置
    void Moveing(Vector3 point)
    {
        //记录鼠标拖动的位置 　　  
        endP = point;
        Vector3 fir = eyeCamera.ScreenToWorldPoint(new Vector3(beginP.x, beginP.y, eyeCamera.nearClipPlane));//转换至世界坐标  
        Vector3 sec = eyeCamera.ScreenToWorldPoint(new Vector3(endP.x, endP.y, eyeCamera.nearClipPlane));
        speed = sec - fir;//需要移动的 向量  
        if(touchStatusCallback != null){
            touchStatusCallback.TouchMove(point);
        } 
    }
    ///Move结束，清除数据
    void MoveEnd(Vector3 point)
    {
        target = null;
        beginP = point;
        speed = Vector3.zero;
        DataManager.Instance.getData("TouchStatus").SetNumberValue("pickUp",0);
        if(touchStatusCallback != null){
            touchStatusCallback.TouchEnd(point);
            touchStatusCallback = null;
        } 
    }

    public void Update()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;
        }
        UpdateTargetPositon();
#else
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
#endif
        
        if (speed == Vector3.zero)
        {
            return;
        }
        var x = target.position.x;
        var y = target.position.y;
        x = x + speed.x;//向量偏移  
        y = y + speed.y;

        if (Bounds)
        {
            var cameraHalfWidth = eyeCamera.orthographic ? Scale2D.cameraSize.x / 2 : 0;
            var cameraHalfHeight = eyeCamera.orthographic ? Scale2D.cameraSize.y / 2 : 0;
            //保证不会移出包围盒  

            x = Mathf.Clamp(x, minVec3.x + cameraHalfWidth, maxVec3.x - cameraHalfWidth);
            y = Mathf.Clamp(y, minVec3.y + cameraHalfHeight, maxVec3.y - cameraHalfHeight);
        }
        target.position = new Vector3(x, y, target.position.z);
        
        if (System.Math.Abs(speed.x) < 0.01f)
        {
            speed.x = 0;
        }
        else
        {
            if (speed.x > 0)
            {
                speed.x -= deceleration.x * Time.deltaTime;
                if (speed.x < 0) {
                    speed.x = 0;
                }
            }
            else
            {
                speed.x += deceleration.x * Time.deltaTime;
                if (speed.x > 0)
                {
                    speed.x = 0;
                }
            }
        }
        if (System.Math.Abs(speed.y) < 0.01f)
        {
            speed.y = 0;
        }
        else
        {
            if (speed.y > 0)
            {
                speed.y -= deceleration.y * Time.deltaTime;
                if (speed.y < 0)
                {
                    speed.y = 0;
                }
            }
            else
            {
                speed.y += deceleration.y * Time.deltaTime;
                if (speed.y > 0)
                {
                    speed.y = 0;
                }
            }
        }
        beginP = endP;
        if (speed.x == 0 && speed.y == 0)
        {
            speed = Vector3.zero;
        }
    }

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit)
    {
        Ray ray = eyeCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 30, layerMask);
    }
    
}
