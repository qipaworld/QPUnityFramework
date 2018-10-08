
using UnityEngine;  
#if UNITY_IOS && !UNITY_EDITOR  
using System.Runtime.InteropServices;  
#endif  
public delegate void ShareCallBackDelegate(bool success ,string platform);  
  
public class NativeSocialShareManager : MonoBehaviour  {  
    #if UNITY_IOS && !UNITY_EDITOR  
    [DllImport ("__Internal")]  
	private static extern void IOS_NativeShare(string text, string encodedMedia);  
    #endif  
  
	public ShareCallBackDelegate shareCallBack = null;  

    private static NativeSocialShareManager instance = null;  
    public static NativeSocialShareManager Instance {  
        get {
            // if (instance == null) {
            //     instance = new NativeSocialShareManager();
            // }
            return instance;  
        }  
		set{ 
			instance = value;
		}
    }  
  
	public void NativeShare(string text, Texture2D texture,ShareCallBackDelegate shareCallBack = null,string screenShotPath = null) {  
		this.shareCallBack = shareCallBack;
#if UNITY_IOS && !UNITY_EDITOR
            Debug.Log("NativeShare: Texture");  
            byte[] val = texture.EncodeToPNG();  
            string bytesString = System.Convert.ToBase64String (val);  
            IOS_NativeShare(text, bytesString);  
#elif UNITY_ANDROID && !UNITY_EDITOR
        ShareAndroid(text, "", "", screenShotPath, "image/png", true, "");
#else
        NativeShareSuccess("");
#endif

        

    }
#if UNITY_ANDROID
    public static void ShareAndroid(string body, string subject, string url, string screenShotPath, string mimeType, bool chooser, string chooserText)
    {
        
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("setType", "image/png");
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //currentActivity.Call("startActivity", intentObject);
        if (chooser)
        {
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, chooserText);
            currentActivity.Call("startActivity", jChooser);
        }
        else
        {
            currentActivity.Call("startActivity", intentObject);
        }

    }

#endif
    private void NativeShareSuccess(string result){  
         Debug.Log("success: " + result);  
		if (shareCallBack!=null){  
			shareCallBack(true,result);  
        }  
    }  
    private void NativeShareCancel(string result){  
         Debug.Log("cancel: " + result);  
		if (shareCallBack != null){  
			shareCallBack(false,result);  
        }  
    }  
}  