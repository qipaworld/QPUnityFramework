using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class DataManager
{
    Dictionary<string, DataBase> dataDic = new Dictionary<string, DataBase>();
    List<DataBase> readySendList = new List<DataBase>();

	public static DataManager instance = null;
    static bool readySend = false;
    //获得Manager 实例
    static public DataManager Instance
    {
        get {
            if (instance == null)
            {
                Init ();
            }
            return instance;
        }
        set {instance = value; }
    }
    static public void Init () {
        if (instance == null)
        {
            instance = new DataManager();
        }
    }
    //添加一个数据
	public DataBase addData(string key,DataBase data = null){
        if (data == null)
        {
            data = new DataBase();
        }
		data.dataName = key;
		dataDic.Add (key, data);
		return data;
	}
    //获得一个数据对象
	public DataBase getData(string key){

        if (dataDic.ContainsKey(key)){
            return dataDic[key];
        }
        return null;
    }
    //获得某个数据里的子数据，keys 是 key的链表
    public DataBase getData(string[] keys)
    {
        DataBase data = null;

        foreach (string key in keys)
        {
            if (data == null){
                data = getData(keys[0]);
            }
            else {
                data = data.GetDataValue(key);
            }
        }
        return data;
    }
    //把变化的数据添加到准备推送的列表里
    public void ReadySendData( DataBase data)
    {
        readySendList.Add(data);
        if (readySend)
        {
            return;
        }
        readySend = true;
        Timer.Instance.DelayInvoke(0,()=>{Send();});// 延迟一帧发送
    }
    //通知监听数据的对象数据发生变化
    protected void Send()
    {
        foreach (DataBase data in readySendList) {
            data.Send();
        }
        readySend = false;
        readySendList.Clear();
    }
}
