﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScreen : MonoBehaviour {

    public string screenName;

    // Update is called once per frame
    public void Go () {
        SceneManager.LoadScene(screenName, LoadSceneMode.Single);
    }
}
