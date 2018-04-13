using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour {
    public string url = "http://qipa.world";
    public string ios = null;
    public string android = null;
    public void open()
    {
        Application.OpenURL(url);
    }
}
