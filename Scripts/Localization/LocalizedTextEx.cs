using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextEx : LocalizedText {
    
    public string[] value;
    protected override void ChangeText(DataBase data){
		text.text = LocalizationManager.Instance.GetLocalizedValue(key,value);
    }
}
