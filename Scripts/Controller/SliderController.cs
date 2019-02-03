using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Action<float> callback = null;
    public string[] dataKeys;
    public string dataValueKey;
    DataBase sliderData;
    public bool isPopSave = true;
    public string saveKey = "";
    double value = 0;
    public Slider slider;
    private void Start()
    {
        sliderData = DataManager.Instance.getData(dataKeys);
        GetComponent<UIData>().changeCallback = UIStatus;
        slider.value = (float)sliderData.GetNumberValue(dataValueKey);

        //sliderData.Bind(Change);
        //if(saveKey == "" && isPopSave)
        //{
        //    saveKey = dataValueKey;
        //}
    }
    //void Change(DataBase data)
    //{

    //}
    public void SetSaveKey(string key)
    {
        saveKey = key;
    }
    public void SetValue(string key,float v)
    {
        sliderData.SetNumberValue(key, v);
    }
    public DataBase GetData()
    {
       return sliderData;
    }
    public void UIStatus(UIData d)
    {
        if(d.uiChangeType == UIChangeType.Pop)
        {
            if (isPopSave&& !string.IsNullOrEmpty(saveKey))
            {
                
                EncryptionManager.SetDouble(saveKey, value);
                EncryptionManager.Save();
            }
        }
    }
    public void Change(float f)
    {
        
        sliderData.SetNumberValue(dataValueKey,f);
        value = f;
        if (callback != null)
        {
            callback(f);
        }
    }
    public void SetAction(Action<float> a)
    {
        callback = a;
    }
}
