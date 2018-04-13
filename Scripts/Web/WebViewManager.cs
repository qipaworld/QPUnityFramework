/*
 * Copyright (C) 2012 GREE, Inc.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System.Collections;
using UnityEngine;

public class WebViewManager
{
    public static WebViewManager instance = null;
    WebViewObject webViewObject;
    bool isUpdateSize = false;
     
    string homeUrl;
    static public WebViewManager Instance
    {
        get {
            return instance;
        }
        set {instance = value; }
    }
    static public void Init(WebViewObject webObj){
        if (instance == null)
        {
            instance = new WebViewManager();
            instance.webViewObject = webObj;
            instance.InitWebView();
        }
    }

    public void InitWebView(){
        
        webViewObject.Init(
            cb: (msg) =>
            {
                Debug.Log(string.Format("CallFromJS[{0}]", msg));
            },
            err: (msg) =>
            {
                Debug.Log(string.Format("CallOnError[{0}]", msg));
            },
            ld: (msg) =>
            {
                Debug.Log(string.Format("CallOnLoaded[{0}]", msg));
#if !UNITY_ANDROID
                // NOTE: depending on the situation, you might prefer
                // the 'iframe' approach.
                // cf. https://github.com/gree/unity-webview/issues/189
#if true
                webViewObject.EvaluateJS(@"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        window.location = 'unity:' + msg;
                      }
                    }
                  }
                ");
#else
                webViewObject.EvaluateJS(@"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        var iframe = document.createElement('IFRAME');
                        iframe.setAttribute('src', 'unity:' + msg);
                        document.documentElement.appendChild(iframe);
                        iframe.parentNode.removeChild(iframe);
                        iframe = null;
                      }
                    }
                  }
                ");
#endif

#endif
                webViewObject.EvaluateJS(@"Unity.call('ua=' + navigator.userAgent)");
            },
            //ua: "custom user agent string",
            enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
        
        
    }
    public void OpenWeb(string url)
    {
       WebView  webviewUI = UIController.Instance.Push("WebViewLayer").GetComponent<WebView>();

        if (!isUpdateSize) {
            int iphonex_down = 0;
            if (SystemInfo.deviceModel == "iPhone10,3" || SystemInfo.deviceModel == "iPhone10,6")
            {
                iphonex_down = 68;
            }
            webViewObject.SetMargins(0, 0, 0, iphonex_down + (int)(webviewUI.GetBarHeight()));
            isUpdateSize = true;
        }
        homeUrl = url;
        webViewObject.SetVisibility(true);
        webViewObject.LoadURL(url.Replace(" ", "%20"));
    }

    public void Close(){
        webViewObject.SetVisibility(false);
    }
    public void GoForward(){
        webViewObject.GoForward();
    }
    public void GoBack(){
        webViewObject.GoBack();
    }
    public void GoHome(){
        webViewObject.LoadURL(homeUrl.Replace(" ", "%20"));
    }
    public void Reload(){
        webViewObject.EvaluateJS("location.reload()");
    }
}
