using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScaleSize : MonoBehaviour
{

    private Touch oldTouch1;  //上次触摸点1(手指1)       
    private Touch oldTouch2;  //上次触摸点2(手指2)       
    public float zoomFactor = 100; //缩放因子
    public Vector3 maxScale = new Vector3(2, 2, 2);
    public Vector3 minScale = Vector3.zero;
    void Update()
    {
        
        if (Input.touchCount < 2)
        {
            return;
        }
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
            {
                return;
            }
        }
        //多点触摸, 放大缩小         
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);
        //第2点刚开始接触屏幕, 只记录，不做处理      
        if (newTouch1.phase == TouchPhase.Began || newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }
        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型     
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势    
        float offset = newDistance - oldDistance;
        //放大因子， 一个像素按 0.01倍来算(100可调整)        
        float scaleFactor = offset / zoomFactor;
        Vector3 localScale = transform.localScale;
        Vector3 scale = new Vector3(localScale.x + scaleFactor, localScale.y + scaleFactor, localScale.z + scaleFactor);
        if (scale.x > minScale.x && scale.y > minScale.y && scale.z > minScale.z
            && scale.x < maxScale.x && scale.y < maxScale.y && scale.z < maxScale.z)
        {
            transform.localScale = scale;
        }
        //记住最新的触摸点，下次使用       
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}
