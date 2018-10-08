using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVersionBind : MonoBehaviour {

    // Use this for initialization
    void Start () {
        FireBaseManager.Instance.gameBaseData.Bind(ChangeStatus);
	}
    private void OnDestroy()
    {
        FireBaseManager.Instance.gameBaseData.Unbind(ChangeStatus);
    }
    // Update is called once per frame
    void ChangeStatus (DataBase data) {
        if (data.GetNumberValue("isUpdateGame") == 1)
        {
            PopUpdateHit();
        }
    }
    void PopUpdateHit()
    {
          UIController.Instance.PushSelectHint("updateGame", UpdateGame, "有新版本发布", null, "下载", "取消");
          FireBaseManager.Instance.gameBaseData.SetNumberValue("isUpdateGame", 0);

    }
    void UpdateGame(SelectStatus b)
    {
        if (b == SelectStatus.YES)
        {
            ScoreTheGameManager.Instance.GoToStoreScore(FireBaseManager.Instance.gameBaseData.GetStringValue("iosId"));
        }
        //else if (b == SelectStatus.NO)
        //{

        //}
    }
}
