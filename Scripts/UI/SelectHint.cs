using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHint : MonoBehaviour {

	string uiName;
	Action<bool> callback;
	bool isSend = false;
    public Text hintText;
    public Text button1Text;
    public Text button2Text;
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
		
		isSend = true;
		if(!notPop){
			UIController.Instance.Pop(uiName);
		}
        if (callback != null)
        {
            callback(isYes);
        }
    }
	void OnDestroy()
    {
        if(!isSend){
			Send(false,true);        	
        }
    }
}
