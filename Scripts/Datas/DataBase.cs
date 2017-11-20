using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
public enum ChangeType{Add,Update,Remove};

public delegate void ChangeDataDelegate(DataBase data);

public class DataBase {
	public ChangeDataDelegate sendChange;
    //存储的数据类型
	protected Dictionary<string, int> intDic = new Dictionary<string, int>();
    protected Dictionary<string, string> stringDic = new Dictionary<string, string>();
    protected Dictionary<string, DataBase> dataDic = new Dictionary<string, DataBase>();
    protected Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();
	//数据在Manager里的key
	public string dataName = "";
    //这一帧里变化的所有的key 和变化属性
	Dictionary<string, ChangeType> changeDic = new Dictionary<string, ChangeType>();
	public Dictionary<string, ChangeType> ChangeDic{
		get{return changeDic;}
	}
    //是否已经准备好发送了。
    bool readySend = false;
    // bind 数量
    int bindNum = 0;
    public int BindNum{
        get{return bindNum;}
    }
    
    //添加一个监听
    public void Bind(ChangeDataDelegate change){
		if (sendChange == null) {
			sendChange = change;
		} else {
			sendChange += change;
		}
        ++bindNum;
		change (this);
	}
    //删除一个监听
	public void Unbind(ChangeDataDelegate change){
		sendChange -= change;
        --bindNum;
	}

	public void SetIntValue(string key,int value){
		AddValueToDic<Dictionary<string, int>,int> (intDic,key,value);
    }

    public void SetStringValue(string key,string value){
		AddValueToDic<Dictionary<string, string>,string> (stringDic,key,value);
    }
	public void SetGameObjectValue(string key,GameObject value){
		AddValueToDic<Dictionary<string, GameObject>,GameObject> (objDic,key,value);
	}

    public void SetDataValue(string key, DataBase value = null)
    {
        if (value == null)
        {
            value = new DataBase();
        }
		value.dataName = key;

		AddValueToDic<Dictionary<string, DataBase>,DataBase> (dataDic,key,value);
    }
    //让某个字典里拥有某个值
	public void AddValueToDic<T,V>(T dic,string key,V value)where T: Dictionary<string,V>{
		if (dic.ContainsKey(key))
		{
			AddToChangeDic (key,ChangeType.Update);
			dic[key] = value;
		}
		else
		{
			dic.Add(key, value);
			AddToChangeDic (key,ChangeType.Add);
		}
		ReadySend();
	}
	public int GetIntDicCount(){
		return GetCountToDic<Dictionary<string,int>,int> (intDic);
    }

    public int GetStringDicCount(){
		return GetCountToDic<Dictionary<string,string>,string> (stringDic);
        
    }
    public int GetGameObjectDicCount(){
		return GetCountToDic<Dictionary<string,GameObject>,GameObject> (objDic);
    }

    public int GetDataDicCount()
    {
		return GetCountToDic<Dictionary<string,DataBase>,DataBase> (dataDic);
    }
	//获得某个字典的长度
	public int GetCountToDic<T,V>(T dic)where T: Dictionary<string,V>{
		return dic.Count;
	}

    public void RemoveIntValue(string key){
		RemoveValueFromDic<Dictionary<string,int>,int> (intDic, key);
    }

    public void RemoveStringValue(string key){
		RemoveValueFromDic<Dictionary<string,string>,string> (stringDic, key);
        
    }
    public void RemoveGameObjectValue(string key){
		RemoveValueFromDic<Dictionary<string,GameObject>,GameObject> (objDic, key);
    }

    public void RemoveDataValue(string key)
    {
		RemoveValueFromDic<Dictionary<string,DataBase>,DataBase> (dataDic, key);
    }
    //通过key从一个字典里删除值  这个是马上发送的，发送时数据没有被删，发送后删除
	public void RemoveValueFromDic<T,V>(T dic,string key)where T: Dictionary<string,V>{
		if (dic.ContainsKey (key)) {
			AddToChangeDic (key,ChangeType.Remove);
			Send();
			dic.Remove (key);
		}
	}

    public int GetIntValue(string key){
		return GetValueFromDic<Dictionary<string,int>,int>(intDic,key);
	}

    public DataBase GetDataValue(string key)
    {
		return GetValueFromDic<Dictionary<string,DataBase>,DataBase>(dataDic,key);
    }

    public string GetStringValue(string key){
		return GetValueFromDic<Dictionary<string,string>,string>(stringDic,key);
        
    }
    public GameObject GetGameObjectValue(string key){
		return GetValueFromDic<Dictionary<string,GameObject>,GameObject>(objDic,key);
	}
    //通过key从一个字典里获得值
	public V GetValueFromDic<T,V>(T dic,string key)where T: Dictionary<string,V>{
		if (dic.ContainsKey (key)) {
			return dic [key];
		}
		return default(V);
	}
    //把自己添加到manager 的发送列表里
	protected void ReadySend(){
        if (readySend) {
            return;
        }
        DataManager.Instance.ReadySendData(this);
        readySend = true;
    }
    //通知监听者数据发生了变化
    public void Send()
    {
        if (sendChange != null)
        {
            sendChange(this);
        }
        readySend = false;
		changeDic.Clear ();
    }
    //把变化的属性和key添加到变化列表里
	public void AddToChangeDic(string key,ChangeType type){
		if (!changeDic.ContainsKey (key)) {
			changeDic.Add (key, type);	
		}
	}
}
