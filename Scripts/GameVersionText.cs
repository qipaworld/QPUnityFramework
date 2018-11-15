using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVersionText : MonoBehaviour {
    public Text text = null;
    // Use this for initialization
    void Start()
    {
        if (text == null)
        {
            text = transform.GetComponent<Text>();
        }
        GameBaseDataManager.Instance.GetBaseData().Bind(ChangeStatus);
    }
    private void OnDestroy()
    {
        GameBaseDataManager.Instance.GetBaseData().Unbind(ChangeStatus);
    }
    // Update is called once per frame
    void ChangeStatus(DataBase data)
    {
        text.text = "Version : " + GameBaseDataManager.Instance.GetGameVersion();
    }
    
}
