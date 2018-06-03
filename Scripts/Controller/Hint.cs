using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    public string hintKey = "GameHint";
    public GameObject hintObj = null;
    public int hintNum = 0;
	// Use this for initialization
	void Start () {
		if(hintObj == null)
        {
            hintObj = gameObject;
        }
        DataManager.Instance.getData("GameStatus").Bind(Change);
	}
    private void OnDestroy()
    {
        DataManager.Instance.getData("GameStatus").Unbind(Change);
    }
    void Change(DataBase data)
    {
        if(data.GetNumberValue(hintKey) == hintNum)
        {
            hintObj.SetActive(true);
        }
        else
        {
            hintObj.SetActive(false);
        }
    }
}
