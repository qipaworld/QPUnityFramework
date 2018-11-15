using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADButtonCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!QipaWorld.Utils.AdButtonIsShow())
        {
            gameObject.SetActive(false);
        }

    }


}
