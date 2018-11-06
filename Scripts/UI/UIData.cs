using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void UIChangeDelegate(UIData data);
public enum UIChangeType{Push,Pop};
public class UIData : MonoBehaviour {

	public UIChangeType uiChangeType;
	public string uiName;
    bool onClickPop = true;
    public DataBase uiData = new DataBase();
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

    public void SetStringData(string key,string value)
    {
        uiData.SetStringValue(key, value);
    }
    public string GetStringData(string key)
    {
        return uiData.GetStringValue(key);
    }
    public void SetOnClickPop(bool onClickPop)
    {
        this.onClickPop = onClickPop;
    }
    public bool GetOnClickPop()
    {
        return onClickPop;
    }
}
