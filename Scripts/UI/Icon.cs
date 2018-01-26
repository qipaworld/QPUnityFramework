using System;
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
	public void Reset(string key,string num = null){
		// this.iconType = iconType;
		// this.texturePath = iconDataKey;
		// this.num = num;
        // Debug.Log(IconManager.Instance.GetIconFilePath(key));

		iconImage.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>(IconManager.Instance.GetIconFilePath(key));
	}
	// void Start(){

	// }
	
}
