using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AcknowledgementLayerCell : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    public void Init(string name,string url)
    {
        text.text = name;
        transform.GetComponent<OpenUrl>().url = url;
    }
}
