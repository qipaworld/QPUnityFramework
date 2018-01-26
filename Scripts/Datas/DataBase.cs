using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.IO;

public enum ChangeType{Add,Update,Remove};

public delegate void ChangeDataDelegate(DataBase data);

public class DataBase {
	public ChangeDataDelegate sendChange;
    //存储的数据类型
	protected Dictionary<string, double> numberDic = new Dictionary<string, double>();
	protected Dictionary<string, Vector3> vectorDic = new Dictionary<string, Vector3>();
    protected Dictionary<string, string> stringDic = new Dictionary<string, string>();
    protected Dictionary<string, DataBase> dataDic = new Dictionary<string, DataBase>();
    protected Dictionary<string, Object> objDic = new Dictionary<string, Object>();
    protected Dictionary<string, YamlMappingNode> yamlMapDic = new Dictionary<string, YamlMappingNode>();
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

	public void SetNumberValue(string key,double value){
		AddValueToDic<Dictionary<string, double>,double> (numberDic,key,value);
    }
    public void SetVectorValue(string key, Vector3 value)
    {
        AddValueToDic<Dictionary<string, Vector3>, Vector3>(vectorDic, key, value);
    }
    public void SetStringValue(string key,string value){
		AddValueToDic<Dictionary<string, string>,string> (stringDic,key,value);
    }
    public void SetObjectValue(string key,Object value){
        AddValueToDic<Dictionary<string, Object>,Object> (objDic,key,value);
    }
	public void AddYamlMapValue(string key,string fileName){
        UnityEngine.Object obj = Resources.Load("UAD/UADDATA");
        if (obj)
        {
            var yaml = new YamlStream();
            yaml.Load(new StringReader(obj.ToString()));
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            AddValueToDic<Dictionary<string, YamlMappingNode>,YamlMappingNode> (yamlMapDic,key,mapping);
        }else{
            Debug.LogError("QIPAWORLD:没有YAML文件 Resources/"+fileName);
        }
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

	public int GetNumberDicCount(){
		return numberDic.Count;
    }

    public int GetVectorDicCount()
    {
        return vectorDic.Count;
    }

    public int GetStringDicCount(){
        return stringDic.Count;
    }
    public int GetObjectDicCount(){
		return objDic.Count;
    }

    public int GetDataDicCount()
    {
        return dataDic.Count;
    }

    public int GetYamlMapDicCount()
    {
		return yamlMapDic.Count;
    }

    public void RemoveNumberValue(string key){
		RemoveValueFromDic<Dictionary<string,double>,double> (numberDic, key);
    }

    public void RemoveStringValue(string key){
		RemoveValueFromDic<Dictionary<string,string>,string> (stringDic, key);
    }
    public void RemoveObjectValue(string key){
		RemoveValueFromDic<Dictionary<string,Object>,Object> (objDic, key);
    }
    public void RemoveVectorValue(string key)
    {
        RemoveValueFromDic<Dictionary<string, Vector3>, Vector3>(vectorDic, key);
    }
    public void RemoveDataValue(string key)
    {
        RemoveValueFromDic<Dictionary<string,DataBase>,DataBase> (dataDic, key);
    }
    public void RemoveYamlMapValue(string key)
    {
		RemoveValueFromDic<Dictionary<string,YamlMappingNode>,YamlMappingNode> (yamlMapDic, key);
    }
    //通过key从一个字典里删除值  这个是马上发送的，发送时数据没有被删，发送后删除
	public void RemoveValueFromDic<T,V>(T dic,string key)where T: Dictionary<string,V>{
		if (dic.ContainsKey (key)) {
			AddToChangeDic (key,ChangeType.Remove);
			Send();
			dic.Remove (key);
		}
	}

    public double GetNumberValue(string key){
		return GetValueFromDic<Dictionary<string,double>,double>(numberDic,key);
	}

    public DataBase GetDataValue(string key)
    {
		return GetValueFromDic<Dictionary<string,DataBase>,DataBase>(dataDic,key);
    }

    public string GetStringValue(string key){
		return GetValueFromDic<Dictionary<string,string>,string>(stringDic,key);
        
    }
    public Object GetObjectValue(string key){
		return GetValueFromDic<Dictionary<string,Object>,Object>(objDic,key);
	}
    public Vector3 GetVectorValue(string key)
    {
        return GetValueFromDic<Dictionary<string, Vector3>, Vector3>(vectorDic, key);
    }
    public YamlMappingNode GetYamlMapValue(string key)
    {
        return GetValueFromDic<Dictionary<string, YamlMappingNode>, YamlMappingNode>(yamlMapDic, key);
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
