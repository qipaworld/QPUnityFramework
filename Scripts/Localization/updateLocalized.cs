using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateLocalized : MonoBehaviour {
    public void save()
    {
        LocalizationManager.Instance.saveDic();
    }
}
