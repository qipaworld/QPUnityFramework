using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController {

	public static UIController instance = null;
	public Transform target;

    DataBase uiDatas;
	DataBase uiLoadDatas;
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
			instance.uiLoadDatas = DataManager.Instance.addData("UILoadDatas");
			instance.uiLoadingLayerDatas = DataManager.Instance.addData("uiLoadingLayerDatas");
			instance.target = GameObject.Find ("Canvas").transform;

        }
    }
	public GameObject Push(string name,UIChangeDelegate callback = null){
        return PushRepeatableLayer(name,name,callback);
	}
	private GameObject PushRepeatableLayer(string name,string fileName,UIChangeDelegate callback = null){
		
		if (uiDatas.GetGameObjectValue(name)!=null){
			Debug.LogWarning("QIPAWORLD:重复添加UI--"+name);
        	return null;
        }

		GameObject uiLoad = uiLoadDatas.GetGameObjectValue(fileName);
		if (uiLoad==null){
			uiLoad = Resources.Load("UIPrefabs/"+fileName) as GameObject;
			uiLoadDatas.SetGameObjectValue(name,uiLoad);
		}
		GameObject ui = GameObject.Instantiate(uiLoad,target) as GameObject;
		UIData uiData = ui.AddComponent<UIData>();
		uiData.uiName = name;
		uiData.changeCallback = callback;
		uiDatas.SetGameObjectValue(name,ui);
		return ui;
	}
	public GameObject PushHint(string name,string key = null,string[] value = null,bool log = false,UIChangeDelegate callback = null){
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
		GameObject ui = uiDatas.GetGameObjectValue(name);
        if(ui!=null){
            uiDatas.RemoveGameObjectValue (name);
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
		return uiDatas.GetGameObjectValue(name);
	}
	public int getLoadingLayerNum(){
		return loadingLayerNum;
	}
	public int getLayerNum(){
		return uiDatas.GetGameObjectDicCount();
	}
    //越小越靠前
    public void SetSequence(int index)   {
        
    }

    public void ToTop(string name)   {
        GameObject ui = uiDatas.GetGameObjectValue(name);
        if (ui!=null){
			ui.transform.position = new Vector3 (ui.transform.position.x, ui.transform.position.y, -1);
        }else{
            Debug.LogWarning("QIPAWORLD:没有这个UI--"+name);
        }
    }
}