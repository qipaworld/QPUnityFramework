using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NUnit;


public class ShareManager {
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
	public static ShareManager instance = null;
	static public ShareManager Instance
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
		instance = new ShareManager ();
	}
	//文字分享已被禁用
	public void Share (string text, Texture2D texture,ShareCallBackDelegate shaeCallback = null, string screenShotPath = null)
	{
		NativeSocialShareManager.Instance.NativeShare (text, texture, shaeCallback ?? FinishedSharing, screenShotPath);
	}
	void FinishedSharing (bool success,string str)
	{
		Debug.Log(str);
		Debug.Log(success);
	}
}
