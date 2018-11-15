using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour {
    public int hintNum = 0;
    public int hintNumTo = 0;
    // Use this for initialization
    public void HintEnd()
    {
        if (DataManager.Instance.getData("GameStatus").GetNumberValue("GameHint") == hintNum)
        {
            DataManager.Instance.getData("GameStatus").SetNumberValue("GameHint", hintNumTo);
        }
    }
}
