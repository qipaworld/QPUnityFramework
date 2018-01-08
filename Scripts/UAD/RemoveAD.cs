using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAD : MonoBehaviour {

    // Use this for initialization
	void Start () {
        
		DataManager.Instance.getData("RemoveAD").Bind(change);

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
	}
    
    
}
