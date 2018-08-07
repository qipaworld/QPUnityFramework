
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
        static string formatStr = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
        static public void GoToScreen(string name,bool force = false)
        {

            if (!force&&DataManager.Instance.getData("GameStatus").GetStringValue("GameScreenName") == name)
            {
                return;
            }
            DataManager.Instance.getData("GameStatus").SetStringValue("GameScreenName", name);

            GameObjManager.Instance.RecycleObjAll();
            // GameObject.Find("Canvas").SetActive(false);
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
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public static double GetTimeStampDouble()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalSeconds;
        }
        static public string NumFormat(double num,string  str = "f1")
        {
            int index = 0;
            while (true)
            {
                if (num < 1000)
                {
                    if(index>= formatStr.Length)
                    {
                        int tindex = index % formatStr.Length + 1;
                        return num.ToString(str) + formatStr[tindex] + formatStr[tindex];
                    }
                    return num.ToString(str) + formatStr[index];
                }
                num /= 1000;
                index++;
                if(index > formatStr.Length * 2-2)
                {
                    return "MAX";
                }
            }
        }
        static public bool IsNewDay(string key,bool isSave = true){
            string strKey = "IsNewDay" + key;
            string timeKey = DateTime.Now.ToShortDateString().ToString();
            if (EncryptionManager.GetString(strKey, "")!=timeKey){
                if(isSave){
                    SaveNewDay(key);
                }
                return true;
            }
            return false;
        }
        static public void SaveNewDay(string key){
            string strKey = "IsNewDay" + key;
            string timeKey = DateTime.Now.ToShortDateString().ToString();
            EncryptionManager.SetString(strKey, timeKey);
            EncryptionManager.Save();
                
        }
        static public string GetDeviceStr()
        {
        #if UNITY_IOS
                    return "ios";
        #elif UNITY_ANDROID
                   return "android";
        #else
                    return "";
        #endif
        }
    }
}