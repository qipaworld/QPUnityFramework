using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseCameraSize : TouchCameraSize {

    protected override void UpdateCameraSize()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        return;
#endif
        float scaleNow = Input.GetAxis("Mouse ScrollWheel");
        if(scaleNow == 0){
            return;
        }
        
        touchCenter = Input.mousePosition;
        worldCenter = eyeCamera.ViewportToWorldPoint(eyeCamera.ScreenToViewportPoint(touchCenter));
        
        //放大因子， 一个像素按 0.01倍来算(100可调整)        
        float scaleFactor = scaleNow / zoomFactor;
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

        touchCenter = Input.mousePosition;
        worldCenter = eyeCamera.ViewportToWorldPoint(eyeCamera.ScreenToViewportPoint(touchCenter));
    }
}
