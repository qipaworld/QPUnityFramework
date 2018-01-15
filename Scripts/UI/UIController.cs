using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController {

	public static UIController instance = null;

    DataBase uiDatas;
	DataBase uiLoadingLayerDatas;
	int loadingLayerNum = 0;
    static public UIController Instance
    {
        get {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
        set {instance = value; }
    }
    static public void Init(){
        if (instance == null)
        {
            instance = new UIController();
            instance.uiDatas = DataManager.Instance.addData("UIDatas");
			instance.uiLoadingLayerDatas = DataManager.Instance.addData("uiLoadingLayerDatas");
        }
    }
	public GameObject Push(string name,UIChangeDelegate callback = null){
        return PushRepeatableLayer(name,name,callback);
	}
	private GameObject PushRepeatableLayer(string name,string fileName,UIChangeDelegate callback = null){
		
		if (uiDatas.GetObjectValue(name)!=null){
			Debug.LogWarning("QIPAWORLD:重复添加UI--"+name);
        	return null;
        }

		GameObject uiLoad = LoadObjManager.Instance.GetLoadObj<GameObject>("UIPrefabs/"+fileName);
		
		GameObject ui = GameObject.Instantiate(uiLoad, GameObject.Find("Canvas").transform) as GameObject;
		UIData uiData = ui.AddComponent<UIData>();
		uiData.uiName = name;
		uiData.changeCallback = callback;
		uiDatas.SetObjectValue(name,ui);
		return ui;
	}
	/// <summary>
    /// 添加一个提示layer
    /// </summary>
    /// <value>name 名字.</value>
    /// <value>key 文本的key.</value>
    /// <value>value 可变化文本内所替换的内容.</value>
    /// <value>log 是否是调试信息.</value>
    /// <value>callback 操作UI时的回掉方法.</value>
    /// <value>outTime 自动关闭UI的时间，不传为手动关闭.</value>
	public GameObject PushHint(string name,string key = null,string[] value = null,bool log = false,UIChangeDelegate callback = null,float outTime = 0f){
		GameObject ui = PushRepeatableLayer (name,"hintLayer",callback);
		if (ui!=null){
			if(log){
				ui.GetComponentInChildren<Text>().text = key;
			}
            else if(key != null){            
                var text = ui.GetComponentInChildren<Text>();
				text.text = LocalizationManager.Instance.GetLocalizedValue(key,value);
                var bg = ui.transform.Find("Bg");
                if(bg){
                    var rectTransform = bg.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2( rectTransform.sizeDelta.x, text.preferredHeight + 100);
                }
            }
            if(outTime != 0){
            	Timer.Instance.DelayInvoke(outTime,()=>{Pop(name);});
            }
		}
		return ui;
	}
	public GameObject PushLoading(string name,string key = null,string[] value = null,UIChangeDelegate callback = null){
		GameObject ui = PushRepeatableLayer (name,"Loading",callback);
		if (ui!=null){
			uiLoadingLayerDatas.SetStringValue (name,name);
			loadingLayerNum++;
			if(key != null){            
				ui.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedValue(key,value);
			}
		}
		return ui;
	}
    public void Pop(string name)	{
		GameObject ui = uiDatas.GetObjectValue(name) as GameObject;
        if(ui!=null){
            uiDatas.RemoveObjectValue (name);
			if(uiLoadingLayerDatas.GetStringValue(name)!=null){
				loadingLayerNum--;
				uiLoadingLayerDatas.RemoveStringValue(name);
			}
			GameObject.Destroy(ui);
        }else{
            Debug.LogWarning("QIPAWORLD:没有找到UI--"+name);
        }

    }
	public GameObject getLayer(string name){
		return uiDatas.GetObjectValue(name) as GameObject;
	}
	public int getLoadingLayerNum(){
		return loadingLayerNum;
	}
	public int getLayerNum(){
		return uiDatas.GetObjectDicCount();
	}
    //越小越靠前
    public void SetSequence(int index)   {
        
    }

    public void ToTop(string name)   {
        GameObject ui = uiDatas.GetObjectValue(name) as GameObject;
        if (ui!=null){
			ui.transform.position = new Vector3 (ui.transform.position.x, ui.transform.position.y, -1);
        }else{
            Debug.LogWarning("QIPAWORLD:没有这个UI--"+name);
        }
    }
}