using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASynchronizeScreenPosition : MonoBehaviour {

    public Transform targetTransform;
    public RectTransform canvas;          //得到canvas的ugui坐标
    public Camera mainCamera;
    //public Vector3 scaleVector = Vector3.zero;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        Vector3 uguiPos = new Vector3();   //用来接收转换后的拖动坐标

        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, targetTransform.position, mainCamera, out uguiPos);

        transform.position = uguiPos + new Vector3(Scale2D.cameraSize.x / 2,0,0);
	}
}
