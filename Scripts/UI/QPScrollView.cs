using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QPScrollView : MonoBehaviour {
    public RectTransform content;
    public RectTransform viewport;
    public Vector3 cellMaxScale;
    float cellWidth = 0;
    GridLayoutGroup gridLayout = null;
    List<GameObject> cellList = new List<GameObject>();
    //float spacingNum = 0;
    // Use this for initialization
    void Awake() {
        if(gridLayout == null)
        {
            gridLayout = content.GetComponent<GridLayoutGroup>();
        }
        cellWidth = gridLayout.cellSize.x;
        
        //spacingNum = Mathf.Floor((viewport.sizeDelta.x- gridLayout.padding.left - gridLayout.padding.right) / (cellWidth+ gridLayout.spacing.x));
    }

    public GameObject Push(string name)
    {
        GameObject obj = GameObjManager.Instance.GetGameObj(name, content);
        float width =   (gridLayout.spacing.x + cellWidth) * content.childCount + gridLayout.padding.left + gridLayout.padding.right;
        content.sizeDelta = new Vector2(width, content.sizeDelta.y);
        obj.AddComponent<QPScrollViewCell>().Init(viewport, cellMaxScale,cellWidth+ gridLayout.spacing.x);
        cellList.Add(obj);
        return obj;
    }
    private void OnDestroy()
    {
        foreach(GameObject obj in cellList)
        {
            QPScrollViewCell svc = obj.GetComponent<QPScrollViewCell>();
            svc.Over();
            Destroy(svc);
        }
        for (int i = content.childCount -1 ; i >=0 ; i--)
        {
            GameObjManager.Instance.RecycleObj(content.GetChild(i).gameObject);
        }
    }
}
