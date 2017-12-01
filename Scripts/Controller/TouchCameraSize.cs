using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraSize : MonoBehaviour {

    private Touch oldTouch1;  //上次触摸点1(手指1)       
    private Touch oldTouch2;  //上次触摸点2(手指2)       
    public float zoomFactor = 1000; //缩放因子
    public float maxScale = 2;
    public float minScale = 0.5f;
    public Camera eyeCamera = null; // 视图相机
    public BoxCollider2D Bounds = null; //移动的边界
    public Vector2 touchCenter = Vector2.zero; //缩放的屏幕中心点
    public Vector3 worldCenter = Vector3.zero; //缩放的世界中心点
    public Vector3
        minVec3,
        maxVec3;
    public void Start()
    {
        if (eyeCamera == null)
        {
            eyeCamera = Camera.main;
        }
        if (Bounds)
        {
            minVec3 = Bounds.bounds.min;//包围盒  
            maxVec3 = Bounds.bounds.max;
        }

    }

    void Update()
    {
        
        if (Input.touchCount != 2)
        {
            return;
        }
        //多点触摸, 放大缩小         
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Canceled || Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                touchCenter = (oldTouch1.position + oldTouch2.position)/2;
                worldCenter = eyeCamera.ViewportToWorldPoint(eyeCamera.ScreenToViewportPoint(touchCenter));
                return;
            }
        }
        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型     
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势    
        float offset = oldDistance - newDistance;
        //放大因子， 一个像素按 0.01倍来算(100可调整)        
        float scaleFactor = offset / zoomFactor;
        float localScale = eyeCamera.orthographicSize;
        float scale = localScale + scaleFactor;
        if (scale < minScale) 
        {
            scale = minScale;
        }
        else if (scale > maxScale)
        {
             scale = maxScale;   
        }
        Scale2D.SetCameraOrthographicSize(scale, eyeCamera);
        Vector3 nowWorldCenter = eyeCamera.ViewportToWorldPoint(eyeCamera.ScreenToViewportPoint(touchCenter));
        transform.position = transform.position + (worldCenter - nowWorldCenter);
        if (Bounds)
        {
            var x = transform.position.x;
            var y = transform.position.y;
            var cameraHalfWidth = eyeCamera.orthographic ? Scale2D.cameraSize.x / 2 : 0;
            var cameraHalfHeight = eyeCamera.orthographic ? Scale2D.cameraSize.y / 2 : 0;
            //保证不会移除包围盒  
            // Debug.Log(transform.position);
            x = Mathf.Clamp(x, minVec3.x + cameraHalfWidth, maxVec3.x - cameraHalfWidth);
            y = Mathf.Clamp(y, minVec3.y + cameraHalfHeight, maxVec3.y - cameraHalfHeight);

            transform.position = new Vector3(x, y, transform.position.z);
        }

        //记住最新的触摸点，下次使用       
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
        touchCenter = (oldTouch1.position + oldTouch2.position)/2;
        worldCenter = eyeCamera.ViewportToWorldPoint(eyeCamera.ScreenToViewportPoint(touchCenter));
    }
}
