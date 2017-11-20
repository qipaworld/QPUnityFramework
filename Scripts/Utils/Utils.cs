
using System;
using UnityEngine;

public class Utils {
	static string strKeys = "~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:ZXCVBNM<>?`1234567890-=qwertyuiop[]asdfghjkl;'zxcvbnm,./";
    static public string GetRandomString(int length){
    	string randomStr = "";
    	for(int i = 0;i<length;++i){
           int index = Mathf.FloorToInt(strKeys.Length * UnityEngine.Random.value);
            randomStr = randomStr + strKeys[index];
    	}
    	return randomStr;
    }
}
