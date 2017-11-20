using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APosition : MonoBehaviour {
	public float speedX = 0;
	public float speedY = 0;
	public float speedZ = 0;

	// Use this for initialization
	void Start () {
		
	}
	void setSpeed(float _speedX,float _speedY,float _speedZ){
		speedX = _speedX;
		speedY = _speedY;
		speedZ = _speedZ;
	}
	// Update is called once per frame
	void Update () {
        //transform.Ta
        transform.Translate(speedX*Time.deltaTime,speedY*Time.deltaTime,speedZ*Time.deltaTime);
	}
}
