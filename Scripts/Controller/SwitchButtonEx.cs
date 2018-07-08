using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonEx : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
    public void switchObj(){
        obj1.SetActive(true);
        obj2.SetActive(false);
        if (obj2.name == "Scroll View" && DataManager.Instance.getData("GameStatus").GetNumberValue("GameHint") == 4)
        {
            DataManager.Instance.getData("GameStatus").SetNumberValue("GameHint", 5);
        }
        if (obj2.name == "Scroll View2" && DataManager.Instance.getData("GameStatus").GetNumberValue("GameHint") == 6)
        {
            DataManager.Instance.getData("GameStatus").SetNumberValue("GameHint", 7);
        }
    }
}
