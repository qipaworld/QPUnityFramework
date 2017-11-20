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

    static public void Init() {
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);

        Vector2 referenceResolution = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution;
		resAspect = referenceResolution.x/referenceResolution.y;	
				
		if(Camera.main.aspect>=resAspect)
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
	
}
