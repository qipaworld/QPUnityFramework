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
	
	public void GoToStoreScore(string iosAppId)
	{
		string url = "";
#if UNITY_IOS && !UNITY_EDITOR
        url = "http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id="+iosAppId+"&pageNumber=0&sortOrdering=2&type=Purple+Software&mt=8";
#elif UNITY_ANDROID && !UNITY_EDITOR
        url = "https://play.google.com/store/apps/details?id="+Application.identifier;
#endif
        Application.OpenURL(url);
	}
}
