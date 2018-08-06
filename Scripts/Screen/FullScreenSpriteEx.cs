using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSpriteEx : MonoBehaviour {

    // Use this for initialization
    public Image image = null;
    public SpriteRenderer spriteRenderer = null;
    Vector2 spriteSize;
    void Awake () {
    	if(image!=null){
    		spriteSize = image.sprite.bounds.size;
    	}else{
    		spriteSize = spriteRenderer.sprite.bounds.size;
    	}
    	Camera camera = Camera.main;
      	float cameraHeight = camera.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);

        float scale = 1;
        if (Camera.main.aspect >= spriteSize.x/ spriteSize.y)
        { 
            scale = cameraSize.x / spriteSize.x;
        }
        else
        { 
            scale = cameraSize.y / spriteSize.y;
        }
        
        transform.localScale = new Vector3(scale,  scale, 1);
    }

}
