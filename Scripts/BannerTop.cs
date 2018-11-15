using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerTop : MonoBehaviour {

    Vector3 originalPosition;
    //音量
    void Start()
    {
        originalPosition = transform.position;
        DataManager.Instance.getData("GameStatus").Bind(Change);
    }
    private void OnDestroy()
    {
        DataManager.Instance.getData("GameStatus").Unbind(Change);
    }
    public virtual void Change(DataBase data)
    {
        if (data.initBind || data.ChangeDic.ContainsKey("AdBannerSize")) {
            transform.position = new Vector3(originalPosition.x, originalPosition.y - data.GetVectorValue("AdBannerSize").y, originalPosition.z);
        }
    }
}
