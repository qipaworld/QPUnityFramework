using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using YamlDotNet.Serialization;
using System.IO;

public class GameBaseDataManager {

    public static GameBaseDataManager instance = null;
    public DataBase gameBaseData; //版本号之类的

    static public GameBaseDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    static public void Init()
    {
        instance = new GameBaseDataManager();
        instance.gameBaseData = DataManager.Instance.addData("GameBaseData");
        //instance.gameBaseData.SetNumberValue("GameError", 0);
        UnityEngine.Object obj = Resources.Load("VersionData");
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, string> dic = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));
            //Debug.Log(dic[QipaWorld.Utils.GetDeviceStr() + "GameVersion"]);
            instance.gameBaseData.SetStringValue("gameVersion", dic[QipaWorld.Utils.GetDeviceStr() + "GameVersion"]);
            instance.gameBaseData.SetStringValue("iosId", dic["iosId"]);
            instance.gameBaseData.SetStringValue("steamId", dic["steamId"]);
            instance.gameBaseData.SetStringValue("firebaseUrl", dic["firebaseUrl"]);
        }
    }
    public string GetIosId()
    {
        return gameBaseData.GetStringValue("iosId");
    }
    public string GetSteamId()
    {
        return gameBaseData.GetStringValue("steamId");
    }
    public string GetGameVersion()
    {
        return gameBaseData.GetStringValue("gameVersion");
    }
    public string GetFireBaseUrl()
    {
        return gameBaseData.GetStringValue("firebaseUrl");
    }
    public DataBase GetBaseData()
    {
        return gameBaseData;
    }
}
