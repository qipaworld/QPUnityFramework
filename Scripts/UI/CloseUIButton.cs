using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIButton : MonoBehaviour {

    public void CloseUI()
    {
        UIController.Instance.Pop(transform.parent.GetComponent<UIData>().uiName);
    }
}
