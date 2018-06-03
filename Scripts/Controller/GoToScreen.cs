using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QipaWorld;

public class GoToScreen : MonoBehaviour {

    public string screenName;
    public string popName = "";

    // Update is called once per frame
    public void Go () {
    	if(popName != ""){
	        UIController.Instance.Pop(popName);
    	}
    	Utils.GoToScreen(screenName);
    }
}
