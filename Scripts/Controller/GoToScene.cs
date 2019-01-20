using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QipaWorld;

public class GoToScene : MonoBehaviour {

    public string SceneName;
    public string popName = "";
    public bool force = false;
    // Update is called once per frame
    public void Go () {
    	if(popName != ""){
	        UIController.Instance.Pop(popName);
    	}
    	Utils.GoToScene(SceneName,force);
    }
}
