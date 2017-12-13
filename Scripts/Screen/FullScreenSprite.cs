using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSprite : MonoBehaviour {

    public  SpriteRenderer spriteRenderer;
    public DataBase DataScale2D = null;
    Vector2 spriteSize;
    void Start () {
        DataScale2D = DataManager.Instance.getData("Scale2D");
        spriteSize = spriteRenderer.sprite.bounds.size;
        DataScale2D.Bind(UpdateSize);
    }

    void UpdateSize(DataBase data){

        float scale = 1;
        if (Camera.main.aspect >= spriteSize.x/ spriteSize.y)
        { 
            scale = Scale2D.cameraSize.x / spriteSize.x;
        }
        else
        { 
            scale = Scale2D.cameraSize.y / spriteSize.y;
        }
        
        transform.localScale = new Vector3(scale,  scale, 1);
    }

    void OnDestroy()
    {
        LocalizationManager.Instance.Unbind(UpdateSize);
    }

}
