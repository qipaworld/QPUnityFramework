using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjManager : MonoBehaviour {

	public static GameObjManager instance = null;
	protected Dictionary<string, List<GameObject>> gameObjDatas = new Dictionary<string, List<GameObject>>();

	static public GameObjManager Instance
	{
	   get {
	       return instance;
	   }
	   // set {instance = value; }
	}
	public void Init(){
		instance = this;
		GameObject.DontDestroyOnLoad(transform.gameObject);
	}
	void Awake()
    {
        if (instance != null) {
            return;
        }
        Init();
    }
	public GameObject GetGameObj(string key,Transform target = null,bool notActive = false){
        List<GameObject> objs = null;
        if (gameObjDatas.ContainsKey(key))
        {
            objs = gameObjDatas[key];
        }
        else
        {
            objs = new List<GameObject>();
            gameObjDatas.Add(key, objs);
        }

        foreach (var o in objs) {
            if (!o.activeSelf)
            {
                if (!notActive) {
                    o.SetActive(true);
                }
                return o;
            }
        }
        GameObject objLoad = LoadObjManager.Instance.GetLoadObj<GameObject>(key);
     
        GameObject obj = GameObject.Instantiate<GameObject>(objLoad, target);
        objs.Add(obj);
        return obj;
    }
    public void RemoveObj(string key)
    {
        if (gameObjDatas.ContainsKey(key))
        {
            foreach (var o in gameObjDatas[key])
            {
                GameObject.Destroy(o);
            }
            gameObjDatas.Clear();
        }
    }

	public void RemoveObjAll()
    {
    	foreach(var data in gameObjDatas ){
    		foreach (var o in data.Value)
            {
                GameObject.Destroy(o);
            }
            gameObjDatas.Clear();
    	}
    }
    public void RecycleObj(GameObject o){
    	if (o.activeSelf)
        {
	        o.transform.parent = transform;
	        o.SetActive(false);
	    }
    }
    public void RecycleObjAllByKey(string key){
    	if (gameObjDatas.ContainsKey(key))
        {
            foreach (var o in gameObjDatas[key])
            {
            	RecycleObj(o);
            }
        }
    }
    public void RecycleObjAll(){
    	foreach(var data in gameObjDatas ){
    		foreach (var o in data.Value)
            {
                RecycleObj(o);
            }
    	}
    }

}
