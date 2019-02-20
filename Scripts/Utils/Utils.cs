
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
        static public string GetLocalizationConnectKey(int num)
        {
            string key = "{0}{";
            for (int i = 1; i <= num; i++)
            {
                if (i == num)
                {
                    key = key + i + "}";
                }
                else
                {
                    key = key + i + "}{";
                }
            }
            return key;
        }
        static public void GoToScene(string name,bool force = false)
        {
            if(name == "StartScene")
            {
                name = GameBaseStatus.Instance.GetStartSceneName();
            }
            if (!force&& GameBaseStatus.Instance.GetRunSceneName() == name)
            {
                return;
            }
            GameBaseStatus.Instance.SetRunSceneName(name);
            //DataManager.Instance.getData("GameStatus").SetStringValue("GameSceneName", name);

            GameObjManager.Instance.RecycleObjAll();
            // GameObject.Find("Canvas").SetActive(false);
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        }
        static public void CheckDirectory(string path){
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
        static public void Serialize(string fileName, System.Object o)
        {
            CheckDirectory(Path.GetDirectoryName(fileName));

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
        static public bool IsNewGame()
        {
            string strKey = "newGame";
            if (EncryptionManager.GetString(strKey, "0") !="1" )
            {
                EncryptionManager.SetString(strKey, "1");
                EncryptionManager.Save();
                return true;
            }
            return false;
        }
        static public bool IsSaveKey(string key,string value, bool isSave = true)
        {
            
            if (EncryptionManager.GetString(key, "") != value)
            {
                if (isSave)
                {
                    EncryptionManager.SetString(key, value);
                    EncryptionManager.Save();
                }
                return false;
            }
            return true;
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
        //格式化音乐
        static public void SpectrumDataFormat(float[] samplesFormat, float[] samples, AudioSource audio = null, int channel = 0, FFTWindow window = FFTWindow.Rectangular)
        {
            if(audio != null)
            {
                audio.GetSpectrumData(samples, channel, window);
            }
            else
            {
                AudioListener.GetSpectrumData(samples, channel, window);
            }
            
            int disNum = samples.Length / samplesFormat.Length;
            int audioNum = 0;
            for (int i = 1; i <= samplesFormat.Length; i++)
            {
                int maxNum = i * disNum;
                float tempNum = 0;
                for (; audioNum < maxNum; audioNum++)
                {
                    tempNum += samples[audioNum];
                }
                samplesFormat[i - 1] = tempNum / disNum;
            }
        }
        static public void RemoveCloneStr(GameObject obj)
        {
            obj.name = obj.name.Replace("(Clone)", "");
        }
        static public string GetDeviceStr()
        {
#if UNITY_IOS
                    return "ios";
#elif UNITY_ANDROID
                   return "android";
#elif UNITY_STANDALONE_WIN
            return "win";
#elif UNITY_STANDALONE_OSX
                   return "mac";
#elif UNITY_STANDALONE_LINUX
                   return "linux";
#else
            return "";
#endif
        }
        static public bool AdButtonIsShow()
        {
            return IsPhone();
        }
        static public bool IsPhone()
        {
#if UNITY_IOS || UNITY_ANDROID
            return true;
#endif
            return false;
        }
        static public bool ChangeFullSceen()
        {
            bool full = !Screen.fullScreen;
#if !UNITY_IOS && !UNITY_ANDROID
            Screen.fullScreen = full;
#endif
            return full;
        }
        static public Vector3 GetObjectMeshSize(GameObject obj)
        {
            Vector3 meshSize = obj.GetComponent<MeshFilter>().mesh.bounds.size;
            // 它的放缩  
            Vector3 scale = obj.transform.localScale;

            return new Vector3(meshSize.x * scale.x, meshSize.y * scale.y, meshSize.z * scale.z);


        }
        static public float GetTouchPressure(Vector2 point,float dis = 5)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch t = Input.GetTouch(i);
                if ((t.position - point).magnitude<=dis)
                {
                    return t.pressure;
                }
            }
            return 1;
        }
    }
    
}