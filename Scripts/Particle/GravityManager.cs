using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager
{
    public static GravityManager instance = null;
    DataBase dataBase;
    static public GravityManager Instance
    {
        get
        {
            return instance;
        }
    }

    static public void Init()
    {
        if (instance == null)
        {
            instance = new GravityManager();
            instance.dataBase = DataManager.Instance.addData("GravityControllerData");
            instance.Bind(instance.ChangeStatus);
            instance.dataBase.SetNumberValue("gravityScale", EncryptionManager.GetDouble("gravityScale", 0.5));
        }
    }
    public float GetGravityScale()
    {
        return (float)dataBase.GetNumberValue("gravityScale");
    }
    public void SetGravityScale(float scale)
    {
        dataBase.SetNumberValue("gravityScale",scale);
    }
    void ChangeStatus(DataBase data)
    {
        if (!data.initBind)
        {
            EncryptionManager.SetDouble("gravityScale", data.GetNumberValue("gravityScale"));
            EncryptionManager.Save();
        }
    }
    public void Bind(ChangeDataDelegate change)
    {
        instance.dataBase.Bind(change);
    }
    public void Unbind(ChangeDataDelegate change)
    {
        instance.dataBase.Unbind(change);
    }
}
