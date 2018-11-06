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
    public Image lockImage = null;
    public Image boarderImage = null;
    public string key;
    public  bool isAudio = true;
    AudioManagerBase audioManager;
    public void Start()
    {
        audioManager = transform.GetComponent<AudioManagerBase>();
    }
    public void Reset(string key, Action<Icon> callback = null,string num = null){
        // this.iconType = iconType;
        // this.texturePath = iconDataKey;
        // this.num = num;
        this.key = key;
		iconImage.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>(IconManager.Instance.GetIconFilePath(key));
        this.callback = callback;
        HideLock();
        string iconNameStr = IconManager.Instance.GetIconName(key);
        if (iconNameStr == " ")
        {
            HideName();
        }
        else
        {
            ShowName();
        }
        string iconBoarder = IconManager.Instance.GetIconBoarder(key);
        if (iconBoarder == " ")
        {
            HideBoarder();
        }
        else
        {
            ShowBoarder();
            boarderImage.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>(iconBoarder);
        }
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
        iconName.GetComponent<LocalizedText>().UpdateText(iconNameStr);

    }
    public string GetName() {
        return iconName.text;
    }
    public void OnClickIcon()
    {
        if (callback != null) {
            if (isAudio)
            {
                audioManager.play();
            }
            callback(this);
        }
    }
    public void setIconColor(Color c) {
        iconImage.color = c;
    }
    
    public void ShowName()
    {
        iconName.gameObject.SetActive(true);
    }
    public void HideName()
    {
        iconName.gameObject.SetActive(false);
    }
    public void ShowLock()
    {
        lockImage.gameObject. SetActive(true);
    }
    public void HideLock() {
        lockImage.gameObject.SetActive(false);
    }
    public void ShowBoarder()
    {
        boarderImage.gameObject.SetActive(true);
    }
    public void HideBoarder()
    {
        boarderImage.gameObject.SetActive(false);
    }
}
