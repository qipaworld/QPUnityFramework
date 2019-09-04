using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenController : MonoBehaviour
{
    //private void Start()
    //{
    //    GameBaseDataManager.Instance.UpdateFullSceneData();
    //}
    public void Change()
    {
        GameBaseDataManager.Instance.ChangeFullScene();
    }
}
