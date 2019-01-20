using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QPScrollViewCell : MonoBehaviour {
    public RectTransform viewport;

    public Vector3 maxScale;
    public Vector3 originScale;
    bool isReady = false;
    float scaleWidth;

    public void Init(RectTransform viewport,Vector3 maxScale, float scaleWidth)
    {
        originScale = transform.localScale;
        this.viewport = viewport;
        this.maxScale = maxScale;
        isReady = true;
        this.scaleWidth = scaleWidth;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (isReady)
        {
            float dis = Mathf.Abs(viewport.position.x - transform.position.x);
            if(dis > scaleWidth)
            {
                transform.localScale = originScale;
            }
            else
            {
                transform.localScale = (maxScale - originScale) * (1 - dis / scaleWidth) + originScale;
            }
        }
	}
    public void Over()
    {
        isReady = false;
        transform.localScale = originScale;
    }
}
