using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using YamlDotNet.Serialization;

public class AcknowledgementLayer : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform target;
    int num = 0;
    VerticalLayoutGroup layout;
    //RectTransform topR;
    void Start()
    {
        layout = target.GetComponent<VerticalLayoutGroup>();
        GameObject tatle = GameObjManager.Instance.GetGameObj("UIPrefabs/AcknowledgementLayerCellText", target);
        tatle.GetComponent<LocalizedText>().UpdateText("鸣谢");
        updatePosition(tatle);
        //tatle.transform.position = new Vector3(tatle.transform.position.x, -30, tatle.transform.position.z);
        UnityEngine.Object obj = Resources.Load("Acknowledgement/AcknowledgementDATA");
        if (obj)
        {
            string dataAsYaml = obj.ToString();
            Deserializer deserializer = new Deserializer();
            Dictionary<string, List<string>> dic = deserializer.Deserialize<Dictionary<string, List<string>>>(new StringReader(dataAsYaml));
            //topR = tatle.GetComponent<RectTransform>();
            foreach (var kv in dic)
            {
                GameObject tempTatle = GameObjManager.Instance.GetGameObj("UIPrefabs/AcknowledgementLayerCellText", target);
                tempTatle.GetComponent<LocalizedText>().UpdateText(kv.Key);
                updatePosition(tempTatle);
                foreach (string s in kv.Value)
                {
                    string[] dataStr = s.Split(',');
                    GameObject cell = GameObjManager.Instance.GetGameObj("UIPrefabs/AcknowledgementLayerCell", target);
                    cell.GetComponent<AcknowledgementLayerCell>().Init(dataStr[0], dataStr[1]);
                    updatePosition(cell);
                }

            }

        }
        
    }
    void updatePosition(GameObject obj)
    {
        RectTransform tempTatleRect = obj.GetComponent<RectTransform>();
        //if(topR == null)
        //{
        //    tempTatleRect.position = new Vector3(
        //    tempTatleRect.position.x,
        //    tempTatleRect.position.y - tempTatleRect.sizeDelta.y * 0.5f,
        //    tempTatleRect.position.z);
        //}
        //else
        //{
        //    tempTatleRect.position = new Vector3(
        //    tempTatleRect.position.x,
        //    topR.position.y - topR.sizeDelta.y * 1.2f,
        //    tempTatleRect.position.z);
        //}

        //topR = tempTatleRect;
        num++;
        target.sizeDelta = new Vector2(target.sizeDelta.x, (tempTatleRect.sizeDelta.y + layout.spacing) * num+layout.padding.top+layout.padding.bottom);
    }
    private void OnDestroy()
    {
        GameObjManager.Instance.RecycleObjAllByKey("UIPrefabs/AcknowledgementLayerCell");
        GameObjManager.Instance.RecycleObjAllByKey("UIPrefabs/AcknowledgementLayerCellText");
    }
}
