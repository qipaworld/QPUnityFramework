using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager {

	public static UIManager instance = null;
	public Transform target;

//    static public UIManager Instance
//    {
//        get {
//            if (instance == null)
//            {
//                Init();
//            }
//            return instance;
//        }
//        set {instance = value; }
//    }
//    static public void Init(){
//		if (instance == null) {
//			instance = new UIManager ();
//			instance.target = GameObject.Find ("Canvas").transform;
//			DataManager.Instance.getData ("UIDatas").Bind (instance.Change);
//		}
//    }
//	void Change(DataBase data){
//		foreach (KeyValuePair<string, ChangeType> kv in data.ChangeDic)
//		{
//		    if(kv.Value == ChangeType.Add){
////				GameObject ui = data.GetGameObjectValue(kv.Key);
////				ui.transform.SetParent(target);
////				ui.transform.parent = target;
////				ui.transform.position = new Vector3 (Screen.width / 2, Screen.height / 2, -1);
//			}else if(kv.Value == ChangeType.Remove){
//				GameObject ui = data.GetGameObjectValue(kv.Key);
//				GameObject.Destroy(ui);
//
//			}
//		}
//		
//	}	
}
