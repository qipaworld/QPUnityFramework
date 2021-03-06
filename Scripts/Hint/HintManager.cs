﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager {

    public static HintManager instance = null;
    DataBase data = new DataBase();
    static public HintManager Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
        // set {instance = value; }
    }
    public static void Init()
    {
        instance = new HintManager();
        DataManager.Instance.addData("hintData", instance.data);
    }
    
    public void RemoveHint(string k)
    {
        data.SetNumberValue(k,0);
    }
    public void AddHint(string k)
    {
        data.SetNumberValue(k, 1);
    }
    public bool IsHint(string k)
    {
        return data.GetNumberValue(k) >0.5f;
    }
    public void BindHint(ChangeDataDelegate change)
    {
        data.Bind(change);
    }
    public void UnbindHint(ChangeDataDelegate change)
    {
        data.Unbind(change);
    }
}
