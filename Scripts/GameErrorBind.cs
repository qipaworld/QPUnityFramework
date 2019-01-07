using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameErrorBind : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GameBaseStatus.Instance.Bind(ChangeStatus);
        //GameBaseDataManager.Instance.GetBaseData().Bind(ChangeStatus);
	}
    private void OnDestroy()
    {
        GameBaseStatus.Instance.Unbind(ChangeStatus);

        //GameBaseDataManager.Instance.GetBaseData().Unbind(ChangeStatus);
    }
    // Update is called once per frame
    void ChangeStatus (DataBase data) {
        if(data.GetNumberValue("GameError") == 1)
        {
            UIController.Instance.PushSelectHint("gameError", GameError, "游戏已损坏");
        }
    }

    void GameError(SelectStatus b)
    {
        Application.Quit();
    }
}
