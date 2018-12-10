using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
    public Transform target = null;
    Transform origenTarget = null;
    // Update is called once per frame
    private void Start()
    {
        if(target == null)
        {
            target = GameObject.Find("Player").transform;
        }
        origenTarget = target;
    }
    void Update () {
        if(target != null)
        {
            
            //transform.right = new Vector3(0,0, (target.position - transform.position).z);
            transform.right = target.position - transform.position;
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
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
