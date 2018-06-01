using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
    public void switchObj(){
        obj1.SetActive(!obj1.activeSelf);
        obj2.SetActive(!obj2.activeSelf);
    }
}
