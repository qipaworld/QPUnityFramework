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
    public Text iconName;
    public Action<Icon> callback;
    public GameObject lockImage = null;
    public string key;
	public void Reset(string key, Action<Icon> callback = null,string num = null){
        // this.iconType = iconType;
        // this.texturePath = iconDataKey;
        // this.num = num;
        this.key = key;
		iconImage.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>(IconManager.Instance.GetIconFilePath(key));
        this.callback = callback;
        HideLock();
        //float scale = 1;
        //Vector3 size = iconImage.sprite.bounds.size;
        //if (size.x > size.y)
        //{
        //    scale = 1.0f / size.x;
        //}
        //else
        //{
        //    scale = 1.0f / size.y;
        //}
        //Image
        iconName.GetComponent<LocalizedText>().UpdateText(IconManager.Instance.GetIconName(key));

    }
    public string GetName() {
        return iconName.text;
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
    public void ShowLock()
    {
        lockImage.SetActive(true);
    }
    public void HideLock() {
        lockImage.SetActive(false);
    }
}
