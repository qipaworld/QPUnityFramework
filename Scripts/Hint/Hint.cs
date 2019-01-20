using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hint : Guide {
    public string hintKey = "GameHint";
    // Use this for initialization
     public override void Start () {
        base.Start();
        HintManager.Instance.BindHint(Change);
	}
    public override void OnDestroy()
    {
        base.OnDestroy();
        HintManager.Instance.UnbindHint(Change);
    }
    public override void Change(DataBase data)
    {
        if ((GuideManager.Instance.GetGuideNum() == hintNum|| HintManager.Instance.IsHint(hintKey)) && CheckPlatform())
        {
            hintObj.SetActive(true);
        }
        else
        {
            hintObj.SetActive(false);
        }
    }
}
