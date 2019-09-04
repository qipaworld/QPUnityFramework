using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSprite : MonoBehaviour {

    public  SpriteRenderer spriteRenderer;
    public DataBase DataScale2D = null;
    public bool Tile = false;
    Vector2 spriteSize;
    void Awake () {
        Scale2D.Update();
        DataScale2D = DataManager.Instance.getData("Scale2D");
        DataScale2D.Bind(UpdateSize);
    }

    void UpdateSize(DataBase data){
        UpdateSize();
    }
    public void UpdateSize()
    {
        spriteSize = spriteRenderer.sprite.bounds.size;

        if (Tile)
        {
            spriteRenderer.size = Scale2D.cameraSize;
            return;
        }
        float scale = 1;
        if (Camera.main.aspect >= spriteSize.x / spriteSize.y)
        {
            scale = Scale2D.cameraSize.x / spriteSize.x;
        }
        else
        {
            scale = Scale2D.cameraSize.y / spriteSize.y;
        }

        transform.localScale = new Vector3(scale, scale, 1);
    }
    void OnDestroy()
    {
        DataScale2D.Unbind(UpdateSize);
    }

}
