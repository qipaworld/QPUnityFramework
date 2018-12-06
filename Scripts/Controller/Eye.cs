using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
    public Transform target = null;
    Transform origenTarget = null;
    // Update is called once per frame
    private void Start()
    {
        origenTarget = target;
    }
    void Update () {
        if(target != null)
        {
            transform.right = target.position - transform.position;
        }
    }
    public void SetTarget(Transform t)
    {
        origenTarget = target;
        target = t;
    }
    public void RestoreTarget()
    {
         target = origenTarget;
    }
}
