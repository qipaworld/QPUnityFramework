using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaseController : MonoBehaviour {
    string shareKey = "";
    public void Start()
    {
        if(shareKey == "")
        {
            shareKey = Application.productName;
        }
    }
    public void Share()
    {
        //FireBaseManager.Instance.SendInvite(shareKey);
    }
}
