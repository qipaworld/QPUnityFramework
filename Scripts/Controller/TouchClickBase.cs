using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 添加这个组件后，就可以点击到对应layerId 的物体了。
/// 可以自己写一个类继承该组件重写OnClick方法
/// </summary>
[RequireComponent(typeof(AudioManagerBase))]
public class TouchClickBase :  TouchBase
{

    //public int layerId = 9; //射线碰撞层编号
    public LayerMask layerMask = 0; //射线碰撞层
    
    public int rayDraction = 30; //射线长度
    public Camera eyeCamera = null; // 视图相机
    public int MaxDraction = 55;
    
    public bool isAudio = false;
    float onlickTime;
    AudioManagerBase audioManager;
    void Start()
    {
        if (eyeCamera == null)
        {
            eyeCamera = Camera.main;
        }
        //layerMask = (1 << layerId);
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

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit, Vector2 point)
    {
        Ray ray = eyeCamera.ScreenPointToRay(point);
        return Physics.Raycast(ray, out hit, rayDraction, layerMask);
    }
    ///初始化点击位置
    public override void TouchBegin(Vector3 point)
    {
        if (DataManager.Instance.getData("TouchStatus").GetNumberValue("pickUp") == 1)
        {
            return;
        }
        beginP = point;
        endP = point;
        isTouch = true;
    }

    ///点击结束，做点击处理
    public override void TouchEnd(Vector3 point)
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
    public override void TouchCanceled(Vector3 point) { isTouch = false; }
    public override void TouchMove(Vector3 point) { endP = point; }

    public virtual void OnClick(RaycastHit hit) {
        //Debug.Log("QIPAWORLD:TouchClickBase.OnClick");
        if (isAudio)
        {
            audioManager.play();
        }
    }
}
