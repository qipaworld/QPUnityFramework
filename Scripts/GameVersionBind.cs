using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameVersionBind : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //GameBaseDataManager.Instance.GetBaseData().Bind(ChangeStatus);
        GameBaseStatus.Instance.Bind(ChangeStatus);
        StartCoroutine(DownloadGameVersion());
    }
    private void OnDestroy()
    {
        GameBaseStatus.Instance.Unbind(ChangeStatus);
        //GameBaseDataManager.Instance.GetBaseData().Unbind(ChangeStatus);
    }
    
    public IEnumerator DownloadGameVersion()
    {
        string url = "http://nodeserver.qipa.world:3150?eventType=getVersion&gameName=" + GameBaseDataManager.Instance.GetGameName() + "&runPlatform=" + QipaWorld.Utils.GetDeviceStr();
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        //异常处理，很多博文用了error!=null这是错误的，请看下文其他属性部分
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
           
        else if(webRequest.responseCode==200)
        {
            if (int.Parse(GameBaseDataManager.Instance.GetGameVersion()) < int.Parse(webRequest.downloadHandler.text))
            {
                GameBaseStatus.Instance.SetReadyUpdate(true);
            }
        }
        
    }

    // Update is called once per frame
    void ChangeStatus (DataBase data) {
        if (GameBaseStatus.Instance.IsReadyUpdate())
        {
            PopUpdateHit();
        }
    }
    void PopUpdateHit()
    {
          UIController.Instance.PushSelectHint("updateGame", UpdateGame, "有新版本发布", null, "下载", "取消");
          //GameBaseDataManager.Instance.gameBaseData.SetNumberValue("isUpdateGame", 0);
          GameBaseStatus.Instance.SetReadyUpdate(false);
    }
    void UpdateGame(SelectStatus b)
    {
        if (b == SelectStatus.YES)
        {
            ScoreTheGameManager.Instance.GoToStoreScore();

        }
        //else if (b == SelectStatus.NO)
        //{

        //}
        //GameBaseStatus.Instance.SetReadyUpdate(false);
    }


}
