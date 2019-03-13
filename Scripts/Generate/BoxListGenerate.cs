using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface BoxListGenerateInterface
{
    void Init(Vector3 v);
    //void EndAttack();
}
public class BoxListGenerate : MonoBehaviour
{
    public BoxCollider box;
    public string objPath;
    public string objKey;
    public Transform target;
    public int num=10;
    public int maxNum;
    int nowNum = 0;
    Vector3 objSize = Vector3.zero;
    Vector3 pointNum = Vector3.zero;
    DataBase objData;
    public List<GameObject> objList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.getData(objKey) == null)
        {
            objData = DataManager.Instance.addData(objKey);
        }
        else
        {
            objData = DataManager.Instance.getData(objKey);

        }
        objData.SetObjectValue("BoxListGenerate", this);
        StartCoroutine(Generate());
    }
    public void Reset()
    {
        objList.Clear();
        GameObjManager.Instance.RecycleObjAllByKey(objPath);
        StartCoroutine(Generate());
    }
    public IEnumerator Generate()
    {
        yield return null;
        yield return null;

        for (int i = 0; i < num; i++)
        {
            GameObject obj = GameObjManager.Instance.GetGameObj(objPath, target);
            objList.Add(obj);
            if (objSize == Vector3.zero)
            {
                objSize = QipaWorld.Utils.GetObjectMeshSize(obj);
            }
            BoxListGenerateInterface gInterface = obj.GetComponent<BoxListGenerateInterface>();
            if (gInterface!=null)
            {
                Vector3 position = GetPositionByIndex();
                //obj.transform.position = position;
                gInterface.Init(position);
            }
           
            if (i % maxNum == 0)
            {
                yield return null;
            }
        }
    }
    public void ResetPoint()
    {
        pointNum = Vector3.zero;
        foreach(GameObject obj in objList)
        { 
            obj.transform.position = GetPositionByIndex();
            obj.transform.rotation = new Quaternion();
        }
    }
    public void UpdatePointNum(int index)
    {
        pointNum[index] = pointNum[index] + 1;
        float point = box.bounds.min[index] + objSize[index] / 2 + objSize[index] * pointNum[index];
        if(point > box.bounds.max[index] - objSize[index] / 2)
        {
            pointNum[index] = 0;
        }
    }
    public Vector3 GetPositionByIndex()
    {

        Vector3 position = Vector3.zero;
        for(int j = 0;j<=3;j++)
        {
            position[0] = box.bounds.min[0] + objSize[0] / 2 + objSize[0] * pointNum[0];
            position[1] = box.bounds.min[1] + objSize[1] / 2 + objSize[1] * pointNum[1];
            position[2] = box.bounds.min[2] + objSize[2] / 2 + objSize[2] * pointNum[2];
            bool isReturn = true;
            if (j == 0)
            {
                pointNum[0] = pointNum[0] + 1;
            }
            for(int i = 0; i < 3; i++)
            {
                if (position[i] > box.bounds.max[i] - objSize[i] / 2)
                {
                    pointNum[i] = 0;
                    if (j == 2)
                    {
                        UpdatePointNum(0);
                    }
                    else
                    {
                        UpdatePointNum(j % 3 + 1);
                    }
                    isReturn = false;
                    break;
                }
            }
            if (isReturn)
            {
                return position;
            }            
        }
        return position;
    }
}
