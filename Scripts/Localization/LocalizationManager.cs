using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;
public class LocalizationManager: DataBase
{
    public static LocalizationManager instance = null;
    
    private string yamlPath = "";
    private string userLanguage = "";
    private Dictionary<string, Dictionary<string, string>> bufferDic = new Dictionary<string, Dictionary<string, string>>();

    static public LocalizationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LocalizationManager();
                string localized = GetLanguage();
                instance.yamlPath = Application.dataPath + "/Resources/Localization/" + localized + ".yaml";
                instance.LoadLocalizedText("Localization/" + localized);
            }
            return instance;
        }
    }
    public void SetUserLanguage(string type){
        userLanguage = type;
        EncryptionManager.SetString("userLanguage",userLanguage);
        EncryptionManager.Save();
        LoadLocalizedText("Localization/" + userLanguage);
    }
    private static string GetLanguage()
    {
        instance.userLanguage = EncryptionManager.GetString("userLanguage");
        if (instance.userLanguage != ""){
            return instance.userLanguage;
        }

        switch (Application.systemLanguage) {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                return "Chinese";
            case SystemLanguage.ChineseTraditional:
                return "ChineseTraditional";
            case SystemLanguage.Russian:
            case SystemLanguage.Belarusian:
                return "Russian";//俄语
            case SystemLanguage.Arabic:
                return "Arabic";//阿拉伯语
            case SystemLanguage.French:
                return "French";//法语
            case SystemLanguage.German:
                return "German";//德语
            case SystemLanguage.Indonesian:
                return "Indonesia";//印尼语
            case SystemLanguage.Japanese:
                return "Japanese";//日语
            case SystemLanguage.Korean:
                return "Korean";//韩语
            case SystemLanguage.Ukrainian:
                return "Ukrainian";//乌克兰语
            case SystemLanguage.Afrikaans:
            case SystemLanguage.Basque:
            case SystemLanguage.Bulgarian:
            case SystemLanguage.Catalan:
            case SystemLanguage.Czech:
            case SystemLanguage.Danish:
            case SystemLanguage.Dutch:
            case SystemLanguage.English:
            case SystemLanguage.Estonian:
            case SystemLanguage.Faroese:
            case SystemLanguage.Finnish:
            case SystemLanguage.Greek:
            case SystemLanguage.Hebrew:
            case SystemLanguage.Icelandic:
            case SystemLanguage.Italian:
            case SystemLanguage.Latvian:
            case SystemLanguage.Lithuanian:
            case SystemLanguage.Norwegian:
            case SystemLanguage.Polish:
            case SystemLanguage.Portuguese:
            case SystemLanguage.Romanian:
            case SystemLanguage.SerboCroatian:
            case SystemLanguage.Slovak:
            case SystemLanguage.Slovenian:
            case SystemLanguage.Spanish:
            case SystemLanguage.Swedish:
            case SystemLanguage.Thai:
            case SystemLanguage.Turkish:
            case SystemLanguage.Vietnamese:
            case SystemLanguage.Unknown:
                return "English";
        }
        return "English";
    }
    public void LoadLocalizedText(string fileName)
    {
        if(bufferDic.ContainsKey(fileName)){
            stringDic = bufferDic[fileName];
            ReadySend();
        }
        else
        {
            Object obj = Resources.Load(fileName); 
            if (!obj)
            {
                obj = Resources.Load("Localization/English"); 
                Debug.LogWarning("QIPAWORLD:没有多语言文件"+fileName);
                UIController.Instance.PushHint("LocalizationError","没有多语言文件"+fileName,null,true);
            }
            string dataAsYaml = obj.ToString();
            var deserializer = new Deserializer();
            stringDic = deserializer.Deserialize<Dictionary<string,string>>(new StringReader(dataAsYaml));

            Object baseObj = Resources.Load(fileName+"Base"); 
            if (!baseObj)
            {
                baseObj = Resources.Load("Localization/EnglishBase"); 
                Debug.LogWarning("QIPAWORLD:没有多语言文件"+fileName+"Base");
                UIController.Instance.PushHint("LocalizationErrorBase","没有多语言文件"+fileName+"Base",null,true);
            }
            string dataAsYamlBase = baseObj.ToString();
            var deserializerBase = new Deserializer();
            var baseDic = deserializerBase.Deserialize<Dictionary<string,string>>(new StringReader(dataAsYamlBase));
            
            foreach (KeyValuePair<string, string> kv in baseDic)
            {
				if (!stringDic.ContainsKey(kv.Key))
                {
                    stringDic.Add(kv.Key, kv.Value);
                }
            }

            ReadySend();
            bufferDic.Add(fileName,stringDic);
        }
        
    }

   
    public string GetLocalizedValue(string key,string[] value = null)
    {
        if (stringDic.ContainsKey(key))
        {
            if (value == null) {
                return stringDic[key];
            }
            return string.Format (stringDic[key],value);
        }
        else
        {   
        #if UNITY_EDITOR
            //stringDic.Add(key,key);
        #endif
        }
#if UNITY_EDITOR
            return "<文本丢失了！！！！！！！>";
        #else
            return key;
        #endif
        
    }
    public void saveDic()
    {
        var serializer = new SerializerBuilder().Build();
        System.IO.StreamWriter file = new System.IO.StreamWriter(yamlPath);
        serializer.Serialize(file, stringDic);
        file.Close();
    }
    
}