using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NUnit;
#if UNITY_IOS && !UNITY_EDITOR  
using System.Runtime.InteropServices;  
#endif  

public class UserScoresManager {
#if UNITY_IOS && !UNITY_EDITOR  
    [DllImport ("__Internal")]  
	private static extern void IOS_PopScoreBox();  
#endif 
	public static UserScoresManager instance = null;
	static public UserScoresManager Instance
	{
		get
		{
			if (instance == null) {
				Init ();
			}
			return instance;
		}
		set{ 
			instance = value;
		}
	}
	static public void Init()
	{
		instance = new UserScoresManager ();
	}
	
	public void PopScoreBox ()
	{
#if UNITY_IOS && !UNITY_EDITOR
       IOS_PopScoreBox(); 
#endif
	}
	
	public void GoToStoreScore()
	{
// 		#if UNITY_IOS && !UNITY_EDITOR
//             Debug.Log("NativeShare: Texture");  
//             byte[] val = texture.EncodeToPNG();  
//             string bytesString = System.Convert.ToBase64String (val);  
//             _GJC_NativeShare(text, bytesString);  
// #elif UNITY_ANDROID && !UNITY_EDITOR
//         ShareAndroid(text, "", "", screenShotPath, "image/png", true, "");
// #else
//         NativeShareSuccess("");
// #endif
	}
}
