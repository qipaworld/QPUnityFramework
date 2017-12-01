using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaleTest : MonoBehaviour {
    public float zoomFactor = 0.1f; //缩放因子
    public Camera eyeCamera = null; // 视图相机
    private void Start()
    {
        if (eyeCamera == null)
        {
            eyeCamera = Camera.main;
        }
    }
    // Use this for initialization
    public void EnlargeCamera() {
        float localScale = eyeCamera.orthographicSize;
        float scale = localScale - zoomFactor;
        Scale2D.SetCameraOrthographicSize(scale, eyeCamera);
    }
    public void DecreaseCamera()
    {
        float localScale = eyeCamera.orthographicSize;
        float scale = localScale + zoomFactor;
        Scale2D.SetCameraOrthographicSize(scale, eyeCamera);
    }
}
