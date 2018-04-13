using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SelectStatus { YES,NO,NOT_SELECT}
public class SelectHint : MonoBehaviour {

	string uiName;
	Action<SelectStatus> callback;
    SelectStatus selectStatus;
    bool isSend = false;
    public Text hintText;
    public Text button1Text;
    public Text button2Text;
    public void Init(string uiName,Action<SelectStatus> callback = null){
		this.uiName = uiName;
		this.callback = callback;
	}
	public void Yes() {
        selectStatus = SelectStatus.YES;
        Send();	
	}
	
	public void No() {
        selectStatus = SelectStatus.NO;
        Send();	
	}
	void Send(bool notPop = false){
		
		isSend = true;
		if(!notPop){
			UIController.Instance.Pop(uiName);
		}
        if (callback != null)
        {
            callback(selectStatus);
        }
    }
	void OnDestroy()
    {
        if(!isSend){
            selectStatus = SelectStatus.NOT_SELECT;
            Send(true);        	
        }
    }
}
