using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager {

    public static GuideManager instance = null;
    DataBase data = new DataBase();
    static public GuideManager Instance
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
        instance = new GuideManager();
        DataManager.Instance.addData("guideData", instance.data);
        
        string num = EncryptionManager.GetString("guideNum");
        if(num == "")
        {
            num = "1";
        }
        instance.SetGuideNum(num);
    }
    
    public string GetGuideNum()
     {
        return data.GetStringValue("guideNum");
     }
     public void SetGuideNum(string v)
    {
        data.SetStringValue("guideNum", v);
        EncryptionManager.SetString("guideNum", v);
        EncryptionManager.Save();
    }
    public void BindGuide(ChangeDataDelegate change)
    {
        data.Bind(change);
    }
    public void UnbindGuide(ChangeDataDelegate change)
    {
        data.Unbind(change);
    }
}
