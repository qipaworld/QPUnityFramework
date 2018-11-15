using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMoveCamera2D : TouchBase
{
    public BoxCollider2D Bounds = null; //移动的边界
    public Vector3 deceleration = new Vector3(1,1,0);//减速度
    public Vector3
        minVec3,
        maxVec3;
    
    private Vector3 speed = Vector3.zero;
    public Camera eyeCamera = null; // 视图相机

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

    }



    ///初始化位置，为接下来的move做准备
    public override void TouchBegin(Vector3 point) {
        if (DataManager.Instance.getData("TouchStatus").GetNumberValue("pickUp") == 1)
        {
            TouchEnd(beginP);
            return;
        }
        beginP = point;
        speed = Vector3.zero;
        isTouch = true;
    }
    ///更新目标位置
    public override void TouchMove(Vector3 point)
    {
        if (DataManager.Instance.getData("TouchStatus").GetNumberValue("pickUp") == 1)
        {
            TouchEnd(beginP);
            return;
        }
        //记录鼠标拖动的位置 　　  
        endP = point;
        Vector3 fir = eyeCamera.ScreenToWorldPoint(new Vector3(beginP.x, beginP.y, eyeCamera.nearClipPlane));//转换至世界坐标  
        Vector3 sec = eyeCamera.ScreenToWorldPoint(new Vector3(endP.x, endP.y, eyeCamera.nearClipPlane));
        speed = sec - fir;//需要移动的 向量  
        UpdatePosition();
    }
    ///Move结束，清除数据
    public override void TouchEnd(Vector3 point)
    {
        //MoveBegin(point);
        isTouch = false;
    }
    public override void TouchCanceled(Vector3 point) { isTouch = false; }

    public void UpdatePosition()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        x = x - speed.x;//向量偏移  
        y = y - speed.y;

        if (Bounds)
        {
            var cameraHalfWidth = eyeCamera.orthographic ? Scale2D.cameraSize.x / 2 : 0;
            var cameraHalfHeight = eyeCamera.orthographic ? Scale2D.cameraSize.y / 2 : 0;
            //保证不会移出包围盒  

            x = Mathf.Clamp(x, minVec3.x + cameraHalfWidth, maxVec3.x - cameraHalfWidth);
            y = Mathf.Clamp(y, minVec3.y + cameraHalfHeight, maxVec3.y - cameraHalfHeight);
        }
        transform.position = new Vector3(x, y, transform.position.z);
        beginP = endP;

    }
    public void Update()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        UpdateTargetPositon();
#else
        if (!isUI&&EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
#endif

        if (speed == Vector3.zero|| isTouch)
        {
            return;
        }
        UpdatePosition();
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
        if (speed.x == 0 && speed.y == 0)
        {
            speed = Vector3.zero;
        }
    }
}