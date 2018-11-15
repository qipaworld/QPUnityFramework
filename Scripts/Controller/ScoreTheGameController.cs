using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTheGameController : MonoBehaviour {
    //public string iosID = "";
    public void PopScoreBox()
    {
        ScoreTheGameManager.Instance.PopScoreBox();
    }
    public void GoToStoreScore()
    {
        ScoreTheGameManager.Instance.GoToStoreScore();
    }
}
