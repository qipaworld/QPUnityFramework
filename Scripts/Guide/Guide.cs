using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {

    // Use this for initialization
    public GameObject hintObj = null;
    public string hintNum = "";
    public string hintNumNext = "";
    void Start () {
        if (hintObj == null)
        {
            hintObj = gameObject;
        }
        GuideManager.Instance.BindGuide(Change);
    }
	
    private void OnDestroy()
    {
        GuideManager.Instance.UnbindGuide(Change);
    }
    void Change(DataBase data)
    {
        if (GuideManager.Instance.GetGuideNum()== hintNum)
        {
            hintObj.SetActive(true);
        }
        else
        {
            hintObj.SetActive(false);
        }
    }
    public void GuideNext()
    {
        if (GuideManager.Instance.GetGuideNum() == hintNum)
        {
            GuideManager.Instance.SetGuideNum(hintNumNext);
        }
    }
}
