﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.RepresentationModel;
using UnityEngine.UI;
// 配置文件  yaml  样例  test: "test,icon/test" #第一位是名字，第二位是资源路径
public class Icon : MonoBehaviour {

	// string iconType;
	// string texturePath;
	// int num;
	public Image iconImage;
    public Text iconName;
    public Action<Icon> callback;
    public string key;
	public void Reset(string key, Action<Icon> callback = null,string num = null){
        // this.iconType = iconType;
        // this.texturePath = iconDataKey;
        // this.num = num;
        this.key = key;
        this.callback = callback;
		iconImage.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>(IconManager.Instance.GetIconFilePath(key));
        iconName.GetComponent<LocalizedText>().UpdateText(IconManager.Instance.GetIconName(key));

    }
    public void OnClickIcon()
    {
        if (callback != null) {
            callback(this);
        }
    }
    public void setIconColor(Color c) {
        iconImage.color = c;
    }
}
