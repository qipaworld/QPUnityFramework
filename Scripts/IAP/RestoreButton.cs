using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
			//gameObject.SetActive (false);
		#endif
	}
	
	public void Restore(){
		IAPManager.Instance.Restore ();
	}
}
