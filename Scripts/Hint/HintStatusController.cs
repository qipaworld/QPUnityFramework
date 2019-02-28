using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintStatusController : MonoBehaviour
{
    public string hintKey = "";
    // Use this for initialization
    public void Change()
    {
        if (hintKey != "")
        {
            if (HintManager.Instance.IsHint(hintKey))
            {
                HintManager.Instance.RemoveHint(hintKey);
            }
            else {
                HintManager.Instance.AddHint(hintKey);
            }
        }
    }
}
