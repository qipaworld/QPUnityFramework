using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 添加这个组件后，该物体就可以被拖动
/// 也可以把他加到一个背景上，手动指定被拖动的物体
/// </summary>
public class TouchMove : MonoBehaviour {

    public int layerId = 8; //射线碰撞层编号
    int layerMask = 0; //射线碰撞层
    public int rayDraction = 30; //射线长度
    public Transform target = null; //移动目标
    public Camera eyeCamera = null; // 视图相机

    Vector3? beginP = null;
    Vector3 targetBeginP;

    void Start()
    {
        if (target == null) {
            target = transform;
        }
        if (eyeCamera == null) {
            eyeCamera = Camera.main;
        }
        layerMask = (1 << layerId);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            UpdateTargetPositon();
        }

    }
    //移动对象
    void UpdateTargetPositon()
    {
        RaycastHit hit;
        if (RayDetection(out hit))
        {
            if (beginP == null || Input.GetTouch(0).phase == TouchPhase.Began)
            {
                MoveBegin(hit.point);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Moveing(hit.point);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                MoveEnd();
            }
        }
    }

    public void OnGUI()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            return;
#endif
        if (Event.current.type == EventType.MouseDown)
        {
            RaycastHit hit;
            if (RayDetection(out hit))
            {
                MoveBegin(hit.point);
            }
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
            RaycastHit hit;
            if (RayDetection(out hit))
            {
                if (beginP == null)
                {
                    MoveBegin(hit.point);
                }
                Moveing(hit.point);
            }

        }
        else if(Event.current.type == EventType.MouseUp)
        {
            MoveEnd();
        }
        else if (Event.current.type == EventType.MouseLeaveWindow)
        {
            MoveEnd();
        }
    }

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit)
    {
        Ray ray = eyeCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 30, layerMask);
    }
    ///初始化位置，为接下来的move做准备
    void MoveBegin(Vector3 point) {
        beginP = point;
        
        targetBeginP = target.transform.position;
    }
    ///更新目标位置
    void Moveing(Vector3 point)
    {
        Vector3 discance = point - beginP.Value;
        target.transform.position = targetBeginP + discance;
    }
    ///Move结束，清除数据
    void MoveEnd()
    {
        beginP = null;
    }
}
