using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour {
    public string guideNum = "";
    public string guideNumTo = "";
    public string hintKey = "";
    // Use this for initialization
    void GuideNext()
    {
        if(guideNum != "")
        {
            if (GuideManager.Instance.GetGuideNum() == guideNum)
            {
                GuideManager.Instance.SetGuideNum(guideNumTo);
            }
        }
    }
    public void EndHint()
    {
        if (guideNum != "")
        {
            if (GuideManager.Instance.GetGuideNum() == guideNum)
            {
                GuideManager.Instance.SetGuideNum(guideNumTo);
            }
        }
        if (hintKey != "")
        {
            if (HintManager.Instance.IsHint(hintKey))
            {
                HintManager.Instance.RemoveHint(hintKey);
            }
        }
    }
}
