/// <summary>
/// UnityAdsHelper.cs - Written for Unity Ads Asset Store v1.1.4
///  by Nikkolai Davenport <nikkolai@unity3d.com> 
/// </summary>

using System;
using UnityEngine;
using System.Collections;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using System.IO;

public class IconManager
{
	public static IconManager instance = null;
	DataBase itemData;
    static public void Init()
    {
		instance = new IconManager();
		instance.itemData = DataManager.Instance.addData("IconData");
        UnityEngine.Object obj = Resources.Load("Icon/ICONDATA");
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, string> dic = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));
            foreach (var kv in dic ){
            	instance.itemData.SetStringValue(kv.Key,kv.Value);
            }
        }
    }
	static public IconManager Instance
    {
        get {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
    }
    public DataBase GetIconData(string key){
    	var data = itemData.GetDataValue(key);
    	if(data==null){
            data = new DataBase();
    		itemData.SetDataValue(key,data);
    		string[] dataStr = itemData.GetStringValue(key).Split(',');
    		data.SetStringValue("name",dataStr[0]);
    		data.SetStringValue("texture",dataStr[1]); 
    		data.SetStringValue("boarder", dataStr[2]); 

        }
    	return data;
    }
    public string GetIconFilePath(string key){
    	return GetIconData(key).GetStringValue("texture");
    }
    public string GetIconName(string key){
    	return GetIconData(key).GetStringValue("name");
    }
    public string GetIconBoarder(string key)
    {
        return GetIconData(key).GetStringValue("boarder");
    }
}