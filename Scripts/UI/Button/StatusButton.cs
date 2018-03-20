using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusButton : MonoBehaviour {
//	public string[] Res = {"",""};
	public string[] dataKeys;
    public string dataValueKey;
    public Sprite[] Sprites;
	public string[] Texts;
    int status = 0;
    public Image image = null;
    public Text text = null;
	void Start() {
        if (!image)
        {
            image = transform.GetComponent<Image>();
        }
        if (!text)
        {
            text = transform.GetComponentInChildren<Text>();
        }
        DataBase data = DataManager.Instance.getData(dataKeys);
        data.Bind(setSatus);
    }
   
	public void setSatus(DataBase data){
        status = System.Convert.ToInt32( data.GetNumberValue(dataValueKey));
        if (image && Sprites.Length > status)
        {
            image.sprite = Sprites[status];
        }
        if (text && Texts.Length > status)
        {
            text.GetComponent<LocalizedText>().UpdateText(Texts[status]);
        }
    }
}
