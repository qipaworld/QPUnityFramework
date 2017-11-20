using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTime : MonoBehaviour {

    public int intputType;
    public InputField mainInputField;
    public TimeManager timeManager;
    void Start ()
    {
        timeManager = TimeManager.Instance;
        mainInputField = transform.GetComponent<InputField>();
        mainInputField.onEndEdit.AddListener(delegate { LockInput(mainInputField); });
		if ((intputType < 6 && timeManager.IsReady)||(intputType < 12 && timeManager.IsReadyFuture)) {
			transform.GetComponent<InputField>().text = timeManager.getTimeByType(intputType).ToString();
		}

    }
	
    void LockInput(InputField input)
    {
        if (input.text.Length < 1)
        {
            input.text = "1";
		}
        if(int.Parse(input.text)<0){
            input.text = "1";       
        }
        if ((intputType == 0 ||intputType == 1 || intputType == 2 ||intputType == 6 || intputType == 7 || intputType == 8)&&(int.Parse(input.text)<1)) {
            input.text = "1";
        }
        if (intputType == 0 || intputType == 6)
        {
            int maxDay = timeManager.GetMonthMaxDay(intputType + 2, int.Parse(input.text),0);
            if (timeManager.getTimeByType(intputType + 2) > maxDay)
            {
                timeManager.setTimeByType(intputType + 2, maxDay);
            }
        }
        else if (( intputType == 1 ||  intputType == 7))
        {
            if (int.Parse(input.text) > 12)
            {
                input.text = "12";
            }
            int maxDay = timeManager.GetMonthMaxDay(intputType+1,0, int.Parse(input.text));
            if (timeManager.getTimeByType(intputType + 1) > maxDay)
            {
                timeManager.setTimeByType(intputType+1, maxDay);
            }
        }
        else if ((intputType == 3 || intputType == 9) && (int.Parse(input.text) > 23))
        {
            input.text = "23";
        }
        else if ((intputType == 4 || intputType == 5 || intputType == 10 || intputType == 11) && (int.Parse(input.text) > 59))
        {
            input.text = "59";
        }
        else if (intputType == 2 || intputType == 8)
        {
            int maxDay = timeManager.GetMonthMaxDay(intputType);
            if (int.Parse(input.text)> maxDay)
            {
                input.text = maxDay.ToString();
            }
        }
        timeManager.setTimeByType(intputType, int.Parse(input.text));
        
    }
}
