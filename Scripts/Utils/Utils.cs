
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace QipaWorld
{
    public class Utils
    {
        static string strKeys = "~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:ZXCVBNM<>?`1234567890-=qwertyuiop[]asdfghjkl;'zxcvbnm,./";
        static public string GetRandomString(int length)
        {
            string randomStr = "";
            for (int i = 0; i < length; ++i)
            {
                int index = Mathf.FloorToInt(strKeys.Length * UnityEngine.Random.value);
                randomStr = randomStr + strKeys[index];
            }
            return randomStr;
        }
        static public void GoToScreen(string name)
        {

            if (DataManager.Instance.getData("GameStatus").GetStringValue("GameScreenName") == name)
            {
                return;
            }
            DataManager.Instance.getData("GameStatus").SetStringValue("GameScreenName", name);

            GameObjManager.Instance.RecycleObjAll();
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
        }
        static public void Serialize(string fileName, System.Object o)
        {

            string directoryPath = Path.GetDirectoryName(fileName);

            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, o);
            fs.Close();
        }
        static public T Deserialize<T>(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return default(T);
            }
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            T t = (T)(bf.Deserialize(fs));
            fs.Close();
            return t;
        }
        static public bool GetBeginTouch(out Touch t)
        {
            t = Input.GetTouch(0);
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    t = Input.GetTouch(i);
                    return true;
                }
            }
            return false;
        }
        static public bool IsPhone()
        {
#if UNITY_IOS || UNITY_ANDROID
            return true;
#endif
            return false;
        }
        static public bool GetTouchByFingerId(int fingerId, out Touch t)
        {
            t = Input.GetTouch(0);
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).fingerId == fingerId)
                {
                    t = Input.GetTouch(i);
                    return true;
                }
            }
            return false;
        }
    }
}