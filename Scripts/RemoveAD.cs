﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAD : MonoBehaviour {

    // Use this for initialization
	void Start () {
        
		DataManager.Instance.getData("RemoveAD").Bind(change);

	}
	void OnDestroy()
    {
		DataManager.Instance.getData("RemoveAD").Unbind(change);
        
    }
	public void change(DataBase data)
	{
		if (data.GetNumberValue("popAdStatus") == 1)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
        if (!QipaWorld.Utils.AdButtonIsShow())
        {
            gameObject.SetActive(false);
        }
        if (IAPManager.Instance.isTapTap)
        {
            gameObject.SetActive(false);
        }
    }


}
