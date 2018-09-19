using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownRecycleBase : MonoBehaviour {

    // Use this for initialization
    public float time = 1f;
    public bool isReadyRecycle = true;

    // Update is called once per frame
    virtual public void Update () {
        time -= Time.deltaTime;
        if (time <= 0 && isReadyRecycle)
        {
            RecycleObj();
        }
    }
    

    virtual public void Init(float t)
    {
        time = t;
        isReadyRecycle = true;
    }
    void RecycleObj()
    {
        isReadyRecycle = false;
        GameObjManager.Instance.RecycleObj(gameObject);
    }
}
