
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils {
	static string strKeys = "~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:ZXCVBNM<>?`1234567890-=qwertyuiop[]asdfghjkl;'zxcvbnm,./";
    static public string GetRandomString(int length){
    	string randomStr = "";
    	for(int i = 0;i<length;++i){
           int index = Mathf.FloorToInt(strKeys.Length * UnityEngine.Random.value);
            randomStr = randomStr + strKeys[index];
    	}
    	return randomStr;
    }
    static public void GoToScreen(string name ){
        
        DataManager.Instance.getData("GameStatus").SetStringValue("GameScreenName",name) ;

        GameObjManager.Instance.RecycleObjAll();
    	SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
    static public void Serialize(string fileName,System.Object o) {
        
        string directoryPath = Path.GetDirectoryName(fileName);

        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, o);
        fs.Close();
    }
    static public T Deserialize<T>(string fileName)
    {
        if (!System.IO.File.Exists(fileName))
        {
            return default(T);
        }
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
        BinaryFormatter bf = new BinaryFormatter();
        T t = (T)(bf.Deserialize(fs));
        fs.Close();
        return t;
    }
}
