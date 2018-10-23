using Firebase;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using YamlDotNet.Serialization;
using System.IO;

public class FireBaseManager  {
    public static FireBaseManager instance = null;
    public DataBase gameBaseData; //版本号之类的
    string gameVersionStr = "";
    bool isReady = false;
    // Use this for initialization
    static public FireBaseManager Instance
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
    static public void Init () {
        instance = new FireBaseManager();
        instance.gameBaseData = DataManager.Instance.addData("GameBaseData");
        instance.gameBaseData.SetNumberValue("isUpdateGame", 0);

        UnityEngine.Object obj = Resources.Load("VersionData");
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, string> dic = deserializer.Deserialize<Dictionary<string, string>>(new StringReader(dataAsYaml));
            instance.gameBaseData.SetStringValue("gameVersion", dic[QipaWorld.Utils.GetDeviceStr() + "GameVersion"]);
            instance.gameBaseData.SetStringValue("iosId", dic["iosId"]);
            instance.gameBaseData.SetStringValue("firebaseUrl", dic["firebaseUrl"]);
        }
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)

        try
        {

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
            var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    instance.isReady = true;
                    // instance.InitInvite();
                    //instance.InitDatabase();
                }
                else
                {
                    Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
                FireBaseManager.Instance.Analytics("start_game");
            });
        }
        catch (Exception e) { }
#endif


    }
    //检测farebase是否可用
    bool CheckReady(bool isLog = false)
    {
        if (!isReady)
        {
            if (isLog)
            {
                GameObject hintLayer = UIController.Instance.PushHint("shareHint");
                if (hintLayer)
                {
                    hintLayer.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetLocalizedValue("未知错误谷歌服务框架过低");
                }
            }
            
            return false;
        }
        return true;
    }
    //-----------动态链接
    // 初始化邀请，和动态链接
    // void InitInvite()
    //{
    //    try { 
    //        FirebaseInvites.InviteReceived += OnInviteReceived;
    //        FirebaseInvites.InviteNotReceived += OnInviteNotReceived;
    //        FirebaseInvites.ErrorReceived += OnErrorReceived;
    //    }
    //    catch (Exception e) { }
    //}
    ////收到邀请
    //void OnInviteReceived(object sender,InviteReceivedEventArgs e)
    //{
        
    //    //if (e.InvitationId != "")
    //    //{
    //    //    //Debug.Log("Invite received: Invitation ID: " + e.InvitationId);
    //    //    FirebaseInvites.ConvertInvitationAsync(
    //    //        e.InvitationId).ContinueWith(HandleConversionResult);
    //    //}
    //    //if (e.DeepLink.ToString() != "")
    //    //{
    //    //    Debug.Log("Invite received: Deep Link: " + e.DeepLink);
    //    //}
    //}
    ////没有收到邀请
    //void OnInviteNotReceived(object sender, EventArgs e)
    //{
    //    //Debug.Log("No Invite or Deep Link received on start up");
    //}
    ////收到邀请时发生错误
    //void OnErrorReceived(object sender,InviteErrorReceivedEventArgs e)
    //{
    //    //Debug.LogError("Error occurred received the invite: " + e.ErrorMessage);
    //}

    //public void SendInvite(string title) {
    //    //Debug.Log("Sending an invitation...");
    //    if (!CheckReady(true))
    //    {
    //        return;
    //    }
    //    try
    //    {
    //        var invite = new Invite()
    //        {
    //            TitleText = title,
    //            MessageText = "Please try my app! It's awesome.",
    //        };
    //        FirebaseInvites.SendInviteAsync(invite).ContinueWith(HandleSentInvite);
    //    }
    //    catch (Exception e) { }
    //}

    //void HandleSentInvite(Task< SendInviteResult > sendTask) {
    //    string hintText = "";

    //    if (sendTask.IsCanceled) {
    //        Debug.Log("Invitation canceled.");
    //        hintText = LocalizationManager.Instance.GetLocalizedValue("分享失败了");
    //    } else if (sendTask.IsFaulted) {
    //        Debug.Log("Invitation encountered an error:");
    //        Debug.Log(sendTask.Exception.ToString());
    //        hintText = LocalizationManager.Instance.GetLocalizedValue("分享失败了");
    //    } else if (sendTask.IsCompleted) {
    //        hintText = LocalizationManager.Instance.GetLocalizedValue("分享成功");
    //        //int inviteCount = (new List(sendTask.Result.InvitationIds)).Count;
    //        //Debug.Log("SendInvite: " + inviteCount + " invites sent successfully.");
    //        //foreach (string id in sendTask.Result.InvitationIds) {
    //        //  Debug.Log("SendInvite: Invite code: " + id);
    //        //}
    //    }
    //    GameObject hintLayer = UIController.Instance.PushHint("shareHint");
    //    if (hintLayer)
    //    {
    //        hintLayer.GetComponentInChildren<Text>().text = hintText;
    //    }
    //}

    //------------------数据库
    //void InitDatabase()
    //{
    //     try
    //     {
    //         FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(gameBaseData.GetStringValue("firebaseUrl"));
    //     }
    //     catch (Exception e) { }
    //    BindDatabase(QipaWorld.Utils.GetDeviceStr() + "GameVersion", BindGameVersion);
    //}
    //public void BindDatabase(string key,EventHandler<ValueChangedEventArgs> callback)
    //{
    //    if (!CheckReady())
    //    {
    //        return;
    //    }
    //    try
    //    {
    //        FirebaseDatabase.DefaultInstance.GetReference(key).ValueChanged += callback;
    //    }
    //    catch (Exception e) { }
    //}
    //public void UnbindDatabase(string key, EventHandler<ValueChangedEventArgs> callback)
    //{
    //    if (!CheckReady())
    //    {
    //        return;
    //    }
    //    try
    //    {
    //        FirebaseDatabase.DefaultInstance.GetReference(key).ValueChanged -= callback;
    //    }
    //    catch (Exception e) { }
    //}
    //void BindGameVersion(object sender, ValueChangedEventArgs args)
    //{

    //    if (args.DatabaseError != null)
    //    {
    //        // Debug.LogError(args.DatabaseError.Message);
    //        return;
    //    }
    //    if (gameVersionStr != args.Snapshot.Value.ToString())
    //    {
    //        gameVersionStr = args.Snapshot.Value.ToString();
    //        if (Convert.ToInt32(args.Snapshot.Value) > Convert.ToInt32(gameBaseData.GetStringValue("gameVersion")))
    //        {
    //            gameBaseData.SetNumberValue("isUpdateGame", 1);
    //        }
    //    }

    //    // Do something with the data in args.Snapshot
    //}
    //------------------------数据纪录
    public void Analytics(string key)
    {
        if (!CheckReady())
        {
            return;
        }
        try { Firebase.Analytics.FirebaseAnalytics.LogEvent(key); } catch (Exception e) { }

    }
}
