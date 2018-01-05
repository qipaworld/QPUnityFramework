using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjManager {

	public static LoadObjManager instance = null;
    protected Dictionary<string, Object> gameObjLoadDatas = new Dictionary<string, Object>();

	static public LoadObjManager Instance
	{
	   get {
            if (instance == null) {
                Init();
            }
	       return instance;
	   }
	   // set {instance = value; }
	}
	public static void Init(){
		instance = new LoadObjManager();
	}
	
	public T GetLoadObj<T>(string key) where T : Object
    {

        T objLoad = default(T);
        if (gameObjLoadDatas.ContainsKey(key))
        {
            objLoad = gameObjLoadDatas[key] as T;
            if(!objLoad){
                objLoad = Resources.Load<T>(key);
                gameObjLoadDatas[key] = objLoad;
            }
            return objLoad;
        }
        
        objLoad = Resources.Load<T>(key);
        gameObjLoadDatas.Add(key, objLoad);
        
        return objLoad;
    }
    // public void RemoveObj(string key)
    // {
    //     if (gameObjDatas.ContainsKey(key))
    //     {
    //         foreach (var o in gameObjDatas[key])
    //         {
    //             GameObject.Destroy(o);
    //         }
    //         gameObjDatas.Clear();
    //     }
    // }

}
