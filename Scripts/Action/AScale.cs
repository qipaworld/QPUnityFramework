using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AScale : MonoBehaviour {
	public Vector3 scaleBy = new Vector3(1,1,1);
	public Vector3 startScale = new Vector3(1,1,1);
	Vector3 speed;
	Vector3 max;
	Vector3 min;
	public float time = 1;
	float direction = 1;
	// Use this for initialization
	void Start () {
		UpdateStatus();
	}
	public void SetScaleData(Vector3 _scaleBy,Vector3 _startScale){
		scaleBy = _scaleBy;
		startScale = _startScale;
        UpdateStatus();
	}
    public void SetTime(float _time)
    {
        time = _time;
        UpdateStatus();
    }
    public void UpdateStatus(){
		transform.localScale = startScale;
		speed = scaleBy/time;
		max = scaleBy + transform.localScale;
		min = transform.localScale;
	}
	// Update is called once per frame
	void Update () {
		Vector3 v = transform.localScale + speed * Time.deltaTime *direction;
		if (v.x > max.x){
			direction = -direction;
			transform.localScale = max;

		}else if(v.x < min.x){
			direction = -direction;
			transform.localScale = min;
		}
		else{
			transform.localScale = v;
		}
	}

}
