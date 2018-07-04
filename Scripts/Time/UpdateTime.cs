using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

    public int intputType;
    public Text text;
    public TimeManager timeManager;
    double timer = 0;
	string language;
	int intputTypeEx = 0;
    void Start () {
        text = transform.GetComponent<Text>();
		text.text = "";
        timeManager = TimeManager.Instance;
		intputTypeEx = intputType;
		if (intputTypeEx > 5) {
			intputTypeEx -= 6;
		}
        LocalizationManager.Instance.Bind(ChangeText);
    }
    void OnDestroy()
    {
        LocalizationManager.Instance.Unbind(ChangeText);
    }
	void ChangeText(DataBase data){
		language = LocalizationManager.Instance.GetLocalizedValue(timeManager.timeTextKeyArr[intputTypeEx]) + ": ";
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
			string str = language;
			if (intputType < 6 && timeManager.IsReady) {
				str += Math.Floor (timeManager.getPastTimeByType (intputType)).ToString ();
			} else if (intputType < 12 && timeManager.IsReadyFuture) {
				str += Math.Floor (timeManager.getFutureTimeByType (intputType)).ToString ();
			} else {
				str = "";
			}
			text.text = str;
            timer -= 1;
        }
    }
}
