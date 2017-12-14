using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void UIChangeDelegate(UIData data);
public enum UIChangeType{Push,Pop};
public class UIData : MonoBehaviour {

	public UIChangeType uiChangeType;
	public string uiName;
	public UIChangeDelegate changeCallback = null;
	void sendChange(){
		if (changeCallback!=null){
			changeCallback(this);
		}
	}
	public void pushUI(){
		uiChangeType = UIChangeType.Push;
		sendChange();
	}
	public void popUI(){
		uiChangeType = UIChangeType.Pop;
		sendChange();
	}

	void Start()
    {
        pushUI();
    }
    
    void OnDestroy()
    {
        popUI();
    }
}
