using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
	public string key = "";
	protected Text text = null;
    protected void Start()
    {
        text = GetComponent<Text>();
        if(key == ""){
	        key = text.text;
	    }
        LocalizationManager.Instance.Bind(ChangeText);
    }
    public void UpdateText(string key) {
        this.key = key;
        if (text != null)
        {
            text.text = LocalizationManager.Instance.GetLocalizedValue(this.key);
        }
    }
    protected virtual void ChangeText(DataBase data){
		text.text = LocalizationManager.Instance.GetLocalizedValue(key);
    }
    void OnDestroy()
    {
        LocalizationManager.Instance.Unbind(ChangeText);
    }
}