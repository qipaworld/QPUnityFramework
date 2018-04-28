using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSpriteEx : MonoBehaviour {

    // Use this for initialization
    public Image spriteRenderer;

    void Start () {
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        float scale = Screen.width*0.01f / (spriteSize.x* GameObject.Find("Canvas").transform.localScale.x);
        transform.localScale = new Vector3(scale, scale, 1);
    }

}
