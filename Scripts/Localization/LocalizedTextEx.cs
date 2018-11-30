using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextEx : LocalizedText {
    
    public string[] value;
    protected override void ChangeText(DataBase data){
		text.text = LocalizationManager.Instance.GetLocalizedValue(key,value);
    }
    public void UpdateText(string key, string[] value)
    {
        this.key = key;
        this.value = value;
        if (text != null)
        {
            text.text = LocalizationManager.Instance.GetLocalizedValue(this.key,value);
        }
    }
}
