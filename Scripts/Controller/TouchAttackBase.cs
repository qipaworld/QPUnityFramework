using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttackBase : TouchBase
{

    public LayerMask layerMask = 0; //射线碰撞层

    public int rayDraction = 30; //射线长度
    public Camera eyeCamera = null; // 视图相机
    public Vector3 lastPoint;
    public bool isAudio = false;
    public bool isAttack = false;
    public bool isDouble = false; // 是否可以持续攻击
    public bool isStartAtk = true;
    //public bool isEndAtk = false;
    AudioStatusBase audioManager;
    void Start()
    {
        if (eyeCamera == null)
        {
            eyeCamera = Camera.main;
        }
        audioManager = transform.GetComponent<AudioStatusBase>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTargetPositon();
    }

    ///检测是否点击到了要移动的物体，并返回射线碰撞与是否发生碰撞
    bool RayDetection(out RaycastHit hit, Vector2 point)
    {
        Ray ray = eyeCamera.ScreenPointToRay(point);
        return Physics.Raycast(ray, out hit, rayDraction, layerMask);
    }
    public void Attack(Vector3 point)
    {
        if (isDouble||!isAttack)
        {
            RaycastHit hit;
            if (RayDetection(out hit, point))
            {
                OnClick(hit, point - lastPoint);
                isStartAtk = false;
            }else if (isAttack)
            {
                EndAtk(point - lastPoint);
            }
        }
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
        lastPoint = point;
        isTouch = true;
        Attack(point);
    }

    ///点击结束，做点击处理
    public override void TouchEnd(Vector3 point)
    {
        EndAtk(point);
    }
    public override void TouchCanceled(Vector3 point) {
        EndAtk(point);
    }
    public virtual void EndAtk(Vector3 point)
    {
        if (isTouch)
        {
            //isEndAtk = true;
            //Attack(point);
            //isEndAtk = false;
        }
        isTouch = false;
        isAttack = false;
        isStartAtk = true;
    }
    public override void TouchMove(Vector3 point) {
        Attack(point);
        lastPoint = endP;
        endP = point;
    }

    public virtual void OnClick(RaycastHit hit,Vector3 dis)
    {
        //Debug.Log("QIPAWORLD:TouchClickBase.OnClick");
        isAttack = true;
        if (isAudio)
        {
            audioManager.play();
        }
    }
}
