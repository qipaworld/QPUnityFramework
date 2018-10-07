using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 添加这个组件后，该物体就可以被拖动
/// 也可以把他加到一个背景上，手动指定被拖动的物体
/// </summary>
public class TouchMove2D : TouchBase
{
    public BoxCollider2D Bounds = null; //移动的边界
    bool isBounds;
    public Vector3 deceleration = new Vector3(1,1,0);//减速度
    public Vector3
        minVec3,
        maxVec3;
    public bool isReverse = false;//移动目标是否比边界大；
    private Vector3 speed = Vector3.zero;
    public Camera eyeCamera = null; // 视图相机
    Vector3 targetSize;//移动物体大小
    public int layerId = 8; //射线碰撞层编号
    int layerMask = 0; //射线碰撞层
    public int rayDraction = 30; //射线长度
    Transform target = null; //移动目标
    public Transform eternalTarget = null; //永远的目标。设置后将不在自动切换目标
    TouchStatusCallback touchStatusCallback = null;
    public bool isSlide = false;
    Vector2 tempMin;//临时的最小边界。根据目标大小而变化
    Vector2 tempMax;
    public void Start()
    {
        isBounds = false;
        if (eyeCamera == null) {
            eyeCamera = Camera.main;
        }
        if (Bounds)
        {
            minVec3 = Bounds.bounds.min;//包围盒  
            maxVec3 = Bounds.bounds.max;
            isBounds = true;
        }
        else {
            DataBase GameBounds = DataManager.Instance.getData("GameBounds");
            if (GameBounds != null)
            {
                minVec3 = GameBounds.GetVectorValue("min");//包围盒  
                maxVec3 = GameBounds.GetVectorValue("max");
                isBounds = true;
            }
        }
        layerMask = (1 << layerId);
    }
    
    ///初始化位置，为接下来的move做准备
    public override void TouchBegin(Vector3 point) {
        RaycastHit hit;
        if(eternalTarget != null){
            target = eternalTarget;
        }else if(target == null && RayDetection(out hit)){
            target = hit.transform;
        }
        if (target!=null)
        {
            touchStatusCallback = target.GetComponent<TouchStatusCallback>();
            if (touchStatusCallback != null)
            {
                touchStatusCallback.TouchBegin(point);
                target = touchStatusCallback.GetMoveTransform();
            }
            
            beginP = point;
            endP = point;
            speed = Vector3.zero;
            DataManager.Instance.getData("TouchStatus").SetNumberValue("pickUp", 1);
            Renderer rend = target.GetComponent<Renderer>();
            if (rend != null)
            {
                targetSize = rend.bounds.size;
            }
            else
            {
                Image image = target.GetComponent<Image>();
                if (image != null){
                    targetSize = image.GetComponent<RectTransform>().sizeDelta;
                }
                else{
                    targetSize = Vector3.zero;
                }
            }
            isTouch = true;
            if(isBounds){
                if (isReverse)
                {
                    //保证不会移出包围盒  
                    tempMin = new Vector2(minVec3.x - (targetSize.x - Bounds.bounds.size.x), minVec3.y - (targetSize.y - Bounds.bounds.size.y));
                    tempMax = new Vector2(maxVec3.x + (targetSize.x - Bounds.bounds.size.x), maxVec3.y + (targetSize.y - Bounds.bounds.size.y));
                }
                else
                {
                    //保证不会移出包围盒  
                    tempMin = new Vector2(minVec3.x + targetSize.x / 2, minVec3.y + targetSize.y / 2);
                    tempMax = new Vector2(maxVec3.x - targetSize.x / 2, maxVec3.y - targetSize.y / 2);

                }
            }
             
        }

        
    }
    ///更新目标位置
    public override void TouchMove(Vector3 point)
    {
        if (target == null) {
            return;
        }
        //记录鼠标拖动的位置 　　  
        endP = point;

        Vector3 fir = beginP;
        Vector3 sec = endP;
        if (!isUI)
        {
            fir = eyeCamera.ScreenToWorldPoint(new Vector3(beginP.x, beginP.y, eyeCamera.nearClipPlane));//转换至世界坐标  
            sec = eyeCamera.ScreenToWorldPoint(new Vector3(endP.x, endP.y, eyeCamera.nearClipPlane));
        }
        speed = sec - fir;//需要移动的 向量  
        if(touchStatusCallback != null){
            touchStatusCallback.TouchMove(point);
        }
        UpdatePosition();
    }
    ///Move结束，清除数据
    public override void TouchEnd(Vector3 point)
    {
        if (!isSlide)
        {
            target = null;
            speed = Vector3.zero;
        }
        beginP = point;
        DataManager.Instance.getData("TouchStatus").SetNumberValue("pickUp",0);
        if(touchStatusCallback != null){
            touchStatusCallback.TouchEnd(point);
            touchStatusCallback = null;
        }
        isTouch = false;
    }
    public override void TouchCanceled(Vector3 point) { TouchEnd(point); }

    void UpdatePosition() {
        var x = target.position.x;
        var y = target.position.y;
        x = x + speed.x;//向量偏移  
        y = y + speed.y;
        Debug.Log(speed);
        if (isBounds)
        {

            x = Mathf.Clamp(x, tempMin.x, tempMax.x);
            y = Mathf.Clamp(y, tempMin.y, tempMax.y);

        }
        target.position = new Vector3(x, y, target.position.z);
        beginP = endP;
    }
    public void Update()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        UpdateTargetPositon();
#else
        if(!isUI&&EventSystem.current.IsPointerOverGameObject()){
            return;
        }
#endif
        
        if (speed == Vector3.zero||isTouch)
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

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit)
    {
        Ray ray = eyeCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, rayDraction, layerMask);
    }
    
}
