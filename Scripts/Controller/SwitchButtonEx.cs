using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonEx : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
    public void switchObj(){
        obj1.SetActive(true);
        obj2.SetActive(false);
    }
}
