using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSprite : MonoBehaviour {

    public  SpriteRenderer spriteRenderer;
    void Start () {

        
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        //Vector2 scale = ;
        float scale = 1;
        if (Camera.main.aspect >= spriteSize.x/ spriteSize.y)
        { 
            scale = Scale2D.cameraSize.x / spriteSize.x;
        }
        else
        { 
            scale = Scale2D.cameraSize.y / spriteSize.y;
        }
        
        transform.localScale = new Vector3(transform.localScale.x*scale, transform.localScale.y * scale, 1);
    }

}
