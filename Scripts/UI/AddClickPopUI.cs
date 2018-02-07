using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClickPopUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		 GameObject uiLoad = LoadObjManager.Instance.GetLoadObj<GameObject>("UIPrefabs/ClickPopUI");
	     GameObject ClickPopUI = GameObject.Instantiate<GameObject>(uiLoad, transform);
	     ClickPopUI.transform.SetSiblingIndex(0);
	}
	
	
}
