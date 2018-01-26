using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHint : MonoBehaviour {

	string uiName;
	Action<bool> callback;
	bool isSend = false;
	public void Init(string uiName,Action<bool> callback = null){
		this.uiName = uiName;
		this.callback = callback;
	}
	public void Yes() {
		Send(true);	
	}
	
	public void No() {
		Send(false);	
	}
	void Send(bool isYes,bool notPop = false){
		if(callback!=null){
			callback(isYes);
		}
		isSend = true;
		if(!notPop){
			UIController.Instance.Pop(uiName);
		}
	}
	void OnDestroy()
    {
        if(!isSend){
			Send(false,true);        	
        }
    }
}
