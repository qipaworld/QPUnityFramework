using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using YamlDotNet.Serialization;

public class InitSelectLanguageLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Object obj = LoadObjManager.Instance.GetLoadObj<Object>("Localization/AllLanguage");
        
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, string> list = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));

            GameObject uiLoad = LoadObjManager.Instance.GetLoadObj<GameObject>("UIPrefabs/selectLanguageButton");
            
            foreach (KeyValuePair<string, string> kv in list)
            {
                GameObject button = GameObject.Instantiate<GameObject>(uiLoad, transform);
                button.GetComponent<SetUserLanguage>().language = kv.Key;
                button.GetComponentInChildren<Text>().text = kv.Value;

            }
        }
    }

}
