﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHide : MonoBehaviour {

	// Use this for initialization
	void Start () {
           gameObject.SetActive(!QipaWorld.Utils.IsPhone());
    }
	
}
