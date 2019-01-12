using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class APositionEx : MonoBehaviour {
	
    public Vector3 speed;
    public Vector3 distance;
    float lastX = 0;
    float lastY = 0;
    float lastZ = 0;
    float dX = 1;
    float dY = 1;
    float dZ = 1;
    [HideInInspector]
    public Vector3 originPosition;
    // Use this for initialization
    void Start () {
        originPosition = transform.position;
	}
	void setSpeed(float _speedX,float _speedY,float _speedZ){
        speed = new Vector3(_speedX, _speedY, _speedZ);
	}
    void setDistance(float _speedX, float _speedY, float _speedZ)
    {
        distance = new Vector3(_speedX, _speedY, _speedZ);
    }
    // Update is called once per frame
    void Update () {
        //transform.Ta
        float x = speed.x * Time.deltaTime*dX;
        float y = speed.y * Time.deltaTime*dY;
        float z = speed.z * Time.deltaTime*dZ;
        
        if((x + lastX) * dX > distance.x)
        {
            //x = (lastX  - x)* dX;
            dX = -dX;
        }
        if ((y + lastY)*dY > distance.y)
        {
            //y = (lastY - y) * dY;
            dY = -dY;
        }
        if ((z + lastZ)*dZ > distance.z)
        {
            //z = (lastZ - z) * dZ;
            dZ = -dZ;
        }
        transform.Translate(x,y,z);
        lastX += x;
        lastY += y;
        lastZ += z;
    }
}
