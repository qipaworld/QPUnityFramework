using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSign : CountDownRecycleBase {
    public SpriteRenderer sp = null;
    private void Awake()
    {
        sp = transform.GetComponent<SpriteRenderer>();
    }
    
    override public void Update()
    {
        base.Update();
        if (time <= 1)
        {

            float a = sp.color.a-0.08f;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, a);
            if (a <= 0)
            {
                isReadyRecycle = true;
            }
        }
    }


    public void Init(float t,Sprite hitSp)
    {
        base.Init(t);
        isReadyRecycle = false;
        float s = Random.value*0.5f+0.5f;
        transform.localScale = new Vector3(s,s,1);
        sp.sprite = hitSp;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);

    }
}
