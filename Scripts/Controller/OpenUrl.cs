using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour {
    public string url = "http://qipa.world";
    public string ios = null;
    public string android = null;
    public bool webView = false;
    public void open()
    {
    	if(webView&& QipaWorld.Utils.IsPhone())
        {
	        WebViewManager.Instance.OpenWeb(url);
        }else{
	        Application.OpenURL(url);
        }
    }
}
