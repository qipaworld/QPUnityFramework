/// <summary>
/// UnityAdsHelper.cs - Written for Unity Ads Asset Store v1.1.4
///  by Nikkolai Davenport <nikkolai@unity3d.com> 
/// </summary>

using System;
using UnityEngine;
using System.Collections;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using System.IO;

#if UNITY_IOS || UNITY_ANDROID
using UnityEngine.Advertisements;

public delegate void HandleShowResultDelegate(ShowResult result);

public class UADManager
{
   HandleShowResultDelegate handleShowResultDelegate;
	public static UADManager instance = null;
   static public void Init()
   {
		instance = new UADManager();
       instance.Start();
   }
	static public UADManager Instance
   {
       get {
           if (instance == null)
           {
               Init();
           }
           return instance;
       }
   }
   

   //--- Unity Ads Setup and Initialization

   void Start ()
	{
       UnityEngine.Object obj = Resources.Load("UAD/UADDATA");
       if (obj)
       {
           string dataAsYaml = obj.ToString();
           Deserializer deserializer = new Deserializer();
           Dictionary<string, string> dic = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));
           string key = "";
#if UNITY_IOS
           key = "ios";
#elif UNITY_ANDROID
           key = "android";
#endif
           if (dic != null && dic.ContainsKey (key)) {
               string gameId = dic[key];
               Advertisement.Initialize(gameId);
               Debug.LogWarning("QIPAWORLD:广告ID"+gameId);
           }
           else{
               Debug.LogWarning("QIPAWORLD:没有广告ID 配置文件位置Resources/UAD/UADDATA");
               UIController.Instance.PushHint("没有广告ID","没有广告ID 配置文件位置Resources/UAD/UADDATA",null,true);
           }
       }else{
           Debug.LogError("QIPAWORLD:没有广告配置文件 Resources/UAD/UADDATA");
           UIController.Instance.PushHint("没有广告ID","没有广告配置文件 Resources/UAD/UADDATA",null,true);
       }
   }
    
   //--- Static Helper Methods

   public  bool isShowing { get { return Advertisement.isShowing; }}
	public  bool isSupported { get { return Advertisement.isSupported; }}
	public  bool isInitialized { get { return Advertisement.isInitialized; }}
   void ShowAdEx(string zoneID = null, HandleShowResultDelegate handleFinished = null){
       handleShowResultDelegate = handleFinished;
       ShowOptions options = new ShowOptions();
       options.resultCallback = HandleShowResult;
       Advertisement.Show(zoneID, options);
   }
	public  bool ShowAd (string zoneID = null, HandleShowResultDelegate handleFinished = null,bool excuse = false)
	{
		if (string.IsNullOrEmpty(zoneID)) zoneID = null;
       if (Advertisement.IsReady(zoneID))
       {   if(excuse)
           {
               UIChangeDelegate uiChange = delegate(UIData data) 
               {   
                   if(data.uiChangeType == UIChangeType.Pop){
                       ShowAdEx(zoneID,handleFinished); 
                   }
               };

               UIController.Instance.PushHint("ShowAdExcuse","致歉",null,false,uiChange,5.5f);
           }
           else
           {
               ShowAdEx(zoneID,handleFinished);
           }
           return true;
       }
       return false;
	}
   public bool PopAd( HandleShowResultDelegate handleFinished = null,bool excuse = false)
   {
		UADManager uad = UADManager.Instance;
       if (uad.ShowAd("video", handleFinished,excuse)) { return true; }
       if (uad.ShowAd("display", handleFinished,excuse)) { return true; }
       if (uad.ShowAd("rewardedVideo", handleFinished,excuse)) { return true; }
       return false;
   }
   public bool ShowRewardedAd( HandleShowResultDelegate handleFinished = null)
   {
		UADManager uad = UADManager.Instance;
       if (uad.ShowAd("rewardedVideo", handleFinished)) { return true; }
       if (uad.ShowAd("video", handleFinished)) { return true; }
       if (uad.ShowAd("display", handleFinished)) { return true; }
        return false;
       //UIController.Instance.PushHint("ShowRewardedAd","广告还没有准备好");
   }
   private  void HandleShowResult (ShowResult result)
	{
       if (handleShowResultDelegate != null)
       {
           handleShowResultDelegate(result);
           return;
       }
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			break;
		case ShowResult.Skipped:
			Debug.LogWarning("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
#else
public class UADManager
{
    public static UADManager instance = null;
    static public void Init()
    {
        instance = new UADManager();
    }
    static public UADManager Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
    }
    
}
#endif