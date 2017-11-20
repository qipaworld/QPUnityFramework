using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUserLanguage : MonoBehaviour {

    public string language;
    public void SetLanguage()
    {
        LocalizationManager.Instance.SetUserLanguage(language);
        UIController.Instance.PushHint("setLanguage","设置成功");
    }
}
