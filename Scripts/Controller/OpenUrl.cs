using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour {
    public string url = "http://qipa.world";
    public void open()
    {
        Application.OpenURL(url);
    }
}
