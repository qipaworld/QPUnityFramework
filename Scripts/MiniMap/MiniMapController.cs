using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIController.Instance.Push("MimiMapLayer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
