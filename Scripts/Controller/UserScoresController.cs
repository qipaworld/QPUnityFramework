using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScoresController : MonoBehaviour {
    public string iosID = "";
    public void PopScoreBox()
    {
        UserScoresManager.Instance.PopScoreBox();
    }
    public void GoToStoreScore()
    {
        UserScoresManager.Instance.GoToStoreScore(iosID);
    }
}
