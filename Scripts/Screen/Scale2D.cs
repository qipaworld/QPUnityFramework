using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale2D {

	// Use this for initialization
	static public float outScreenScale = 1f;
	static public float intScreenScale = 1f;
	static public float resAspect = 1f;
    static public Vector2 cameraSize;
    static public DataBase DataScale2D = null;

    static public void Init(Camera camera = null) {
        DataScale2D = DataManager.Instance.getData("Scale2D");
        Update(camera);
	}

    static public void Update(Camera camera) {
        if (camera == null) {
            camera = Camera.main;
        }
        float cameraHeight = camera.orthographicSize * 2;
        cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);

        Vector2 referenceResolution = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution;
        resAspect = referenceResolution.x/referenceResolution.y;    
                
        if(camera.aspect>=resAspect)
        {
            outScreenScale = Screen.width / referenceResolution.x;
            intScreenScale = Screen.height/referenceResolution.y;
        }
        else
        {
            intScreenScale = Screen.width / referenceResolution.x;
            outScreenScale = Screen.height/referenceResolution.y;
        }

    }
    ///设置正交相机的大小的时候请用该方法，他会更新该本类数据
    static public void SetCameraOrthographicSize(float value,Camera camera = null) {
        if (camera == null) {
            camera = Camera.main;
        }
        camera.orthographicSize = value;
        Update(camera);
        DataScale2D.Send();
    }
	
}
