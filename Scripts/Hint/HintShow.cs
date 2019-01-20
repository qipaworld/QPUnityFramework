using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintShow : MonoBehaviour {

    public string hintKey = "";
    public void Show()
    {
        HintManager.Instance.AddHint(hintKey);
    }
}
