using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSetting : MonoBehaviour {
    public Transform target = null;
    public RectTransform rect = null;
    public BoxCollider2D Bounds = null; //移动的边界

    // Use this for initialization
    string saveImageKey = "";
    
    public void Init(Sprite sp){
        Image mImage = transform.GetComponent<Image>();
        Image tImage = target.GetComponent<Image>();

        mImage.sprite = sp;
        tImage.sprite = sp;
        mImage.SetNativeSize();
        tImage.SetNativeSize();

        TouchScaleSize ts = transform.GetComponent<TouchScaleSize>();
        RectTransform photoRect = transform.GetComponent<RectTransform>();
        
        if (rect.sizeDelta.x/ rect.sizeDelta.y> photoRect.sizeDelta.x / photoRect.sizeDelta.y){
            float scale = rect.sizeDelta.x / photoRect.sizeDelta.x;
            ts.minScale = new Vector3(scale, scale, 1);
        }else{
            float scale = rect.sizeDelta.y / photoRect.sizeDelta.y;
            ts.minScale = new Vector3(scale, scale, 1);
        }
        ts.maxScale = ts.minScale * 3;
        transform.localScale = ts.minScale;
    }
	// Update is called once per frame
	void Update () {
        target.localScale = transform.localScale;
        target.position = transform.position;
	}
    public void Confirm(){
        //Debug.Log(rect.position);
        //Debug.Log(rect.offsetMin);
        QipaWorld.Utils.CheckDirectory(Application.persistentDataPath + "/Photo");
        saveImageKey = Application.persistentDataPath + "/Photo/" + QipaWorld.Utils.GetTimeStamp() + ".png";
        //saveImageKey = "/Users/qishengming/Documents/ttt.png";
        //rect.
        ScreenshotController.Instance.CaptureByRect(new Rect(Bounds.bounds.min.x, Bounds.bounds.min.y, Bounds.bounds.size.x, Bounds.bounds.size.y), ShareTexture, saveImageKey,new Vector2(125,180));
        //UIController.Instance.Pop("PhotoSetting");
    }
    void ShareTexture(Texture2D texture)
    {
        DataManager.Instance.getData("GameStatus").SetStringValue("PhotoName", saveImageKey);
        UIController.Instance.Pop("PhotoSelect");
        EncryptionManager.SetString("PhotoName", saveImageKey);
        EncryptionManager.Save();

        Cancel();

        //        if (screenshotCamera != null)
        //        {
        //            screenshotCamera.gameObject.SetActive(false);
        //        }
        //        string sharePath = null;

        //#if UNITY_ANDROID && !UNITY_EDITOR
        //            sharePath = Application.persistentDataPath + "/share.png";
        //#endif
        //ShareManager.Instance.Share(LocalizationManager.Instance.GetLocalizedValue(shareStr), texture, FinishedSharing, sharePath);
        //Destroy(texture);
        ////UIController.Instance.Pop ("ShareLayer");
        //logo.SetActive(false);
        //isShare = false;

    }
    public void Cancel(){
        UIController.Instance.Pop("PhotoSetting");
    }
}
