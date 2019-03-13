using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour {

    public static bool isSave = false;
    public Image image = null;
    public Sprite originSp = null;
    string key = "";
    int maxSize = 2048;
    public void Start()
    {
        if(originSp == null)
        {
            Init();
        }
    }
    void Init()
    {
        originSp = image.sprite;
    }
    public void OnClick()
    {
        if (key == "")
        {
            //UIController.Instance.Push(uiName);
            NativeGallery.Permission p = NativeGallery.CheckPermission();
            // Debug.Log(p);
            if (p == NativeGallery.Permission.Granted){
                PickImage(maxSize);
            }
            else if(p == NativeGallery.Permission.ShouldAsk)
            {
                if (NativeGallery.RequestPermission() == NativeGallery.Permission.Granted)
                {
                    PickImage(maxSize);
                }
            }
            else{
                //UIController.Instance.Pop("PhotoPermission");
                if(NativeGallery.CanOpenSettings()){
                    UIController.Instance.PushSelectHint("PhotoPermission", PhotoPermission, "提示开启相册权限",null,"设置","取消");
                }
                else{
                    UIController.Instance.PushHint("PhotoPermission", "提示开启相册权限");
                }

            }
        }
        else if (key == "origin")
        {
            DataManager.Instance.getData("GameStatus").SetStringValue("PhotoName", "");
            UIController.Instance.Pop("PhotoSelect");
            EncryptionManager.SetString("PhotoName", "");
            EncryptionManager.Save();
        }
        else
        {
            DataManager.Instance.getData("GameStatus").SetStringValue("PhotoName", key);
            UIController.Instance.Pop("PhotoSelect");
            EncryptionManager.SetString("PhotoName", key);
            EncryptionManager.Save();

        }
    }
   

    void PhotoPermission(SelectStatus b)
    {
        if (b == SelectStatus.YES)
        {
            NativeGallery.OpenSettings();
        }
        //else if (b == SelectStatus.NO)
        //{

        //}
    }
    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            //Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                //NativeGallery.cop
                
                if (!isSave)
                {
                    Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }
                    Sprite sp = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), Vector2.zero);
                    GameObject ui = UIController.Instance.Push("PhotoSetting");
                    ui.GetComponentInChildren<PhotoSetting>().Init(sp);
                }
                else
                {
                    QipaWorld.Utils.CheckDirectory(Application.persistentDataPath + "/Photo");
                    string saveImageKey = Application.persistentDataPath + "/Photo/" + QipaWorld.Utils.GetTimeStamp() + Path.GetExtension(path);
                    File.Copy(path, saveImageKey);
                    //byte[] bytes = texture.EncodeToPNG();
                    ////保存
                    //System.IO.File.WriteAllBytes(saveImageKey, bytes);
                    //File.c
                    //FileUtil.CopyFileOrDirectory(path, "destpath/YourFileOrFolder");
                    DataManager.Instance.getData("GameStatus").SetStringValue("PhotoName", saveImageKey);
                    //UIController.Instance.Pop("PhotoSelect");
                    EncryptionManager.SetString("PhotoName", saveImageKey);
                    EncryptionManager.Save();
                    UIController.Instance.Pop("PhotoSelect");

                }



                // Assign texture to a temporary quad and destroy it after 5 seconds

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                //Destroy(texture, 5f);
            }
        }, "Select a PNG image", "image/png", maxSize);

        Debug.Log("Permission result: " + permission);
    }
    public void SetPhoto(string path)
    {
        if (originSp == null)
        {
            Init();
        }
        //image
        this.key = path;
        if(key == ""){
            image.sprite = originSp;
            image.SetNativeSize();
            //image.transform.localScale = new Vector3(1f, 1f, 1);
            image.color = new Color(1, 1, 1, 0.72f);
        }else if (key == "origin")
        {
            image.sprite = LoadObjManager.Instance.GetLoadObj<Sprite>("originPhoto");
            //image.sprite = sprite;
            image.rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta*0.85f;
            //image.SetNativeSize();
            //image.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            image.color = new Color(1, 1, 1, 1);
        }
        else{
            StartCoroutine(LoadPhoto(path));
        }

    }
    private IEnumerator LoadPhoto(string path)
    {

        WWW www = new WWW("file://" + path);//只能放URL
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {

            Texture2D texture = www.texture;
            //创建 Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            image.sprite = sprite;

            image.rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta * 0.85f;

            //image.SetNativeSize();
            //image.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            image.color = new Color(1, 1, 1, 1);
        }
    }


}
