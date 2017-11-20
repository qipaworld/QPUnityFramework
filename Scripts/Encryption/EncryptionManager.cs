using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
public class EncryptionManager
{
    private static string sKEY = "ZTdkIJHKWRBKWTM3MWM0NWFhNTEzAzU1"; // 你的加密KEY
    private static string sIV = "4rZdmAIfa/PpeA89qE4wrL=="; // 你的加密IV
    private static string publicKey = "publicKey";
    private static string publicKeyStr = null;

    public static void Init(string _sKEY = null,string _sIV = null)
    {
        sKEY = _sKEY ?? sKEY;
        sIV = _sIV ?? sIV;
        string key = PlayerPrefs.GetString(EncryptionManager.GetHashEx(publicKey), "");
        if (key!=""){
            publicKeyStr = EncryptionManager.Decrypt(key);
        }else{
            publicKeyStr = Utils.GetRandomString(16) + SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString(EncryptionManager.GetHashEx(publicKey), EncryptionManager.Encrypt(publicKeyStr));
        }
    }

    public static void SetInt(string key, int val)
    {
        PlayerPrefs.SetString(EncryptionManager.GetHash(key), EncryptionManager.Encrypt(val.ToString()));
    }
    public static int GetInt(string key, int defaultValue = 0)
    {
        string value = EncryptionManager.GetString(key, defaultValue.ToString());
        int result = defaultValue;
        int.TryParse(value, out result);
        return result;
    }
    public static void SetDouble(string key, double val)
    {
        PlayerPrefs.SetString(EncryptionManager.GetHash(key), EncryptionManager.Encrypt(val.ToString()));
    }
    public static double GetDouble(string key, double defaultValue = 0f)
    {
        string value = EncryptionManager.GetString(key, defaultValue.ToString());
        double result = defaultValue;
        double.TryParse(value, out result);
        return result;
    }
    public static void SetString(string key, string val)
    {
        PlayerPrefs.SetString(EncryptionManager.GetHash(key), EncryptionManager.Encrypt(val));
    }
    public static string GetString(string key, string defaultValue = "")
    {
        string text = defaultValue;
        string value = PlayerPrefs.GetString(EncryptionManager.GetHash(key), defaultValue.ToString());
        if (!text.Equals(value))
        {
            text = EncryptionManager.Decrypt(value);
        }
        return text;
    }
    public static bool HasKey(string key)
    {
        string hash = EncryptionManager.GetHash(key);
        return PlayerPrefs.HasKey(hash);
    }
    public static void DeleteKey(string key)
    {
        string hash = EncryptionManager.GetHash(key);
        PlayerPrefs.DeleteKey(hash);
    }
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    public static void Save()
    {
        PlayerPrefs.Save();
    }
    private static string Decrypt(string encString)
    {
        RijndaelManaged rijndaelManaged = new RijndaelManaged
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] bytes = Encoding.UTF8.GetBytes(EncryptionManager.sKEY);
        byte[] rgbIV = Convert.FromBase64String(EncryptionManager.sIV);
        ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, rgbIV);
        byte[] array = Convert.FromBase64String(encString);
        byte[] array2 = new byte[array.Length];
        MemoryStream stream = new MemoryStream(array);
        CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
        cryptoStream.Read(array2, 0, array2.Length);
        return Encoding.UTF8.GetString(array2).TrimEnd(new char[1]);
    }
    private static string Encrypt(string rawString)
    {
        RijndaelManaged rijndaelManaged = new RijndaelManaged
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] bytes = Encoding.UTF8.GetBytes(EncryptionManager.sKEY);
        byte[] rgbIV = Convert.FromBase64String(EncryptionManager.sIV);
        ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes, rgbIV);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
        byte[] bytes2 = Encoding.UTF8.GetBytes(rawString);
        cryptoStream.Write(bytes2, 0, bytes2.Length);
        cryptoStream.FlushFinalBlock();
        byte[] inArray = memoryStream.ToArray();
        return Convert.ToBase64String(inArray);
    }
    private static string GetHash(string key)
    {
        return GetHashEx(publicKeyStr+key);
    }
    private static string GetHashEx(string key)
    {
        MD5 mD = new MD5CryptoServiceProvider();
        byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(key));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }
}