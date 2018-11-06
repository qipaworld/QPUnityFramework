using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVersionBind : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GameBaseDataManager.Instance.GetBaseData().Bind(ChangeStatus);
        
	}
    private void OnDestroy()
    {
        GameBaseDataManager.Instance.GetBaseData().Unbind(ChangeStatus);
    }
    // Update is called once per frame
    void ChangeStatus (DataBase data) {
        if (data.GetNumberValue("isUpdateGame") == 1)
        {
            PopUpdateHit();
        }
        if(data.GetNumberValue("GameError") == 1)
        {
            UIController.Instance.PushSelectHint("gameError", GameError, "游戏已损坏");
        }
    }
    void PopUpdateHit()
    {
          UIController.Instance.PushSelectHint("updateGame", UpdateGame, "有新版本发布", null, "下载", "取消");
          GameBaseDataManager.Instance.gameBaseData.SetNumberValue("isUpdateGame", 0);

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
    
    void GameError(SelectStatus b)
    {
        Application.Quit();
    }
}
