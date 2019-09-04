using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotoSelect : MonoBehaviour {
    public Transform target = null;
	// Use this for initialization
	void Start () {
        GameObjManager.Instance.GetGameObj("UIPrefabs/PhotoButton", target).GetComponent<Photo>().SetPhoto("");
        GameObjManager.Instance.GetGameObj("UIPrefabs/PhotoButton", target).GetComponent<Photo>().SetPhoto("origin");

        QipaWorld.Utils.CheckDirectory(Application.persistentDataPath + "/Photo");

        // 获取Application.dataPath文件夹下所有的图片路径
        string[] dirs = Directory.GetFiles((Application.persistentDataPath + "/Photo"));
        for (int j = 0; j < dirs.Length; j++)
        {
            GameObjManager.Instance.GetGameObj("UIPrefabs/PhotoButton", target).GetComponent<Photo>().SetPhoto(dirs[j]);
        }
        
    }
    private void OnDestroy()
    {
        GameObjManager.Instance.RecycleObjAllByKey("UIPrefabs/PhotoButton");
    }

}
