using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVersionBind : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //GameBaseDataManager.Instance.GetBaseData().Bind(ChangeStatus);
        GameBaseStatus.Instance.Bind(ChangeStatus);
	}
    private void OnDestroy()
    {
        GameBaseStatus.Instance.Unbind(ChangeStatus);
        //GameBaseDataManager.Instance.GetBaseData().Unbind(ChangeStatus);
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
    }
    

}
