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
#endif
public delegate void HandleShowResultDelegate(ShowResult result);

public class UADManager
{
    HandleShowResultDelegate handleShowResultDelegate;
	public static UADManager instance = null;
    static public void Init()
    {
		instance = new UADManager();
		DataManager.Instance.addData("RemoveAD");
		DataManager.Instance.getData("RemoveAD").SetIntValue("popAdStatus", EncryptionManager.GetInt("RemoveAD", 0));
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
   
#if UNITY_IOS || UNITY_ANDROID

    //--- Unity Ads Setup and Initialization

    void Start ()
	{
        UnityEngine.Object obj = Resources.Load("UAD/UADDATA");
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, string> dic = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));
#if UNITY_IOS
            string gameId = dic["ios"];
#elif UNITY_ANDROID
            string gameId = dic["android"];
#endif
            Advertisement.Initialize(gameId);
            Debug.Log("QIPAWORLD:广告ID"+gameId);

        }else{
            Debug.LogError("QIPAWORLD:没有广告ID");
            UIController.Instance.PushHint("没有广告ID","没有广告ID",null,true);
        }
    }
    
    //--- Static Helper Methods

    public  bool isShowing { get { return Advertisement.isShowing; }}
	public  bool isSupported { get { return Advertisement.isSupported; }}
	public  bool isInitialized { get { return Advertisement.isInitialized; }}

	public  bool ShowAd (string zoneID = null, HandleShowResultDelegate handleFinished = null)
	{
		if (string.IsNullOrEmpty(zoneID)) zoneID = null;
        if (Advertisement.IsReady(zoneID))
        {
            handleShowResultDelegate = handleFinished;
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;
            Advertisement.Show(zoneID, options);
            return true;
        }
        return false;
	}
    public bool PopAd( HandleShowResultDelegate handleFinished = null)
    {
		UADManager uad = UADManager.Instance;
        if (uad.ShowAd("video", handleFinished)) { return true; }
        if (uad.ShowAd("display", handleFinished)) { return true; }
        if (uad.ShowAd("rewardedVideo", handleFinished)) { return true; }
        return false;
    }
    public void ShowRewardedAd( HandleShowResultDelegate handleFinished = null)
    {
		UADManager uad = UADManager.Instance;
        if (uad.ShowAd("rewardedVideo", handleFinished)) { return; }
        if (uad.ShowAd("video", handleFinished)) { return; }
        if (uad.ShowAd("display", handleFinished)) { return; }
        UIController.Instance.PushHint("ShowRewardedAd","广告还没有准备好");
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
	public void RemoveAds(){
		DataManager.Instance.getData ("RemoveAD").SetIntValue ("popAdStatus", 1);
		EncryptionManager.SetInt ("RemoveAD", 1);
		EncryptionManager.Save ();
	}

#endif
}