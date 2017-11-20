using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEmail : MonoBehaviour {
    public string email = "shengming.qi.cn@gmail.com";

    // Use this for initialization
    public void Open()
    {
//#if UNITY_IPHONE || UNITY_EDITOR
        Uri uri = new Uri(string.Format("mailto:{0}?subject={1}", email, Application.productName));
        Application.OpenURL(uri.AbsoluteUri);
//#endif
    }
}
