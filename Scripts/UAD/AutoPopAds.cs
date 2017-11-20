using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPopAds : MonoBehaviour {
    public float popTime = 10;
    bool isPop = true;
	// Use this for initialization
	void Start () {
    DataManager.Instance.getData("RemoveAD").Bind(change);

    }
    public void change(DataBase data)
{
    if (data.GetIntValue("popAdStatus") == 1)
    {
            isPop = false;
    }
    else
    {
        isPop = true;
    }
}
// Update is called once per frame
void Update () {
        if (isPop)
        {
            popTime -= Time.deltaTime;
            if (popTime <= 0)
            {
                StartCoroutine(PopAd());
            }
        }
	}
    
    private IEnumerator PopAd()
    {
        isPop = false;
		while (true) {
			if (UIController.Instance.getLayerNum() != 0)
				yield return new WaitForSeconds (1f);
			else if (!UADManager.Instance.PopAd ())
				yield return new WaitForSeconds (1f);
			else
				break;
        }
        yield break;
    }

}
