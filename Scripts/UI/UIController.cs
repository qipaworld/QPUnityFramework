using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController {

	public static UIController instance = null;

    DataBase uiDatas;
	DataBase uiLoadingLayerDatas;
	int loadingLayerNum = 0;
    
    GameObject hintLayerLast;
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
	/// <summary>
    /// 添加一个带确定按钮的提示layer
    /// </summary>
    /// <value>name 名字.</value>
    /// <value>userActionCallBack 用户操作回掉.</value>
    /// <value>key 文本的key.</value>
    /// <value>value 可变化文本内所替换的内容.</value>
    /// <value>callback 操作UI时的回掉方法.</value>
	public GameObject PushSelectHint(string name,Action<bool> userActionCallBack,string key = null,string[] value = null,UIChangeDelegate callback = null){
		GameObject ui = PushRepeatableLayer (name,"selectHintLayer",callback);
		if (ui!=null){
            if(key != null){            
                var text = ui.GetComponentInChildren<Text>();
				text.text = LocalizationManager.Instance.GetLocalizedValue(key,value);
                var bg = ui.transform.Find("Bg");
                if(bg){
                    var rectTransform = bg.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2( rectTransform.sizeDelta.x, text.preferredHeight + 100);
                }
            }
            ui.GetComponent<SelectHint>().Init(name,userActionCallBack);
		}
		return ui;
	}
    /// <summary>
    /// 添加一个带确定按钮的提示layer
    /// </summary>
    /// <value>name 名字.</value>
    /// <value>items 传道具的type 和数量.</value>
    /// <value>key 文本的key.</value>
    /// <value>value 可变化文本内所替换的内容.</value>
    /// <value>callback 操作UI时的回掉方法.</value>
    public GameObject PushItemHint(string name,string[,] items,string key = null,string[] value = null,UIChangeDelegate callback = null){
        GameObject ui = PushRepeatableLayer (name,"hintItemLayer",callback);
        if (ui!=null){
            float textHeight = 0;
            if(key != null){            
                var text = ui.GetComponentInChildren<Text>();
                var textOriginalHeight = text.preferredHeight;
                text.text = LocalizationManager.Instance.GetLocalizedValue(key,value);
                textHeight = text.preferredHeight - textOriginalHeight;
            }
            var bg = ui.transform.Find("Bg");

            var itemBase = bg.transform.Find("ItemBase").transform;
            GameObject uiLoad = LoadObjManager.Instance.GetLoadObj<GameObject>("UIPrefabs/Icon");
            
            var uiLoadRectTransform = uiLoad.GetComponent<RectTransform>();
            var itemBaseRectTransform = itemBase.GetComponent<RectTransform>();
            int itemNum = items.GetLength(0);
            GridLayoutGroup itemBaseGridLayoutGroup = itemBase.GetComponent<GridLayoutGroup>();

            float itemBaseNowHieght = (float)(Math.Ceiling(itemNum / Math.Floor(itemBaseRectTransform.sizeDelta.x / (uiLoadRectTransform.sizeDelta.x+itemBaseGridLayoutGroup.spacing.x)))*(uiLoadRectTransform.sizeDelta.y+itemBaseGridLayoutGroup.spacing.y)+10);
            float itemBaseHieght = itemBaseNowHieght - itemBaseRectTransform.sizeDelta.y;
            itemBaseRectTransform.sizeDelta = new Vector2(itemBaseRectTransform.sizeDelta.x,itemBaseNowHieght);
            itemBaseGridLayoutGroup.cellSize = uiLoadRectTransform.sizeDelta;
            
            for(int i = 0;i<items.GetLength(0);++i){
                GameObject button = GameObject.Instantiate<GameObject>(uiLoad, itemBase);
                button.GetComponent<Icon>().Reset(items[i,0],items[i,1]);
            }
            if(bg){
                var rectTransform = bg.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2( rectTransform.sizeDelta.x, rectTransform.sizeDelta.y+textHeight+itemBaseHieght);
            }
        }
        return ui;
    }
	public GameObject PushListHint(string name,string key = null,string[] value = null,bool log = false,UIChangeDelegate callback = null){
		GameObject ui = this.PushHint(name,key,value,log,callback);
		if (ui!=null){
			HintListCell hintListCell = ui.AddComponent<HintListCell>();
			hintListCell.Init(name, new Vector2(Screen.width/2,0),new Vector2(Screen.width/2,Screen.height/2),hintLayerLast);
			hintLayerLast = ui;
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