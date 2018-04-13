using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
public class Timer : MonoBehaviour
{
    
    public static Timer instance = null;
    //获得Manager 实例
    static public Timer Instance
    {
        get {
            if (instance == null)
            {
                instance = new Timer();
            }
            return instance;
        }
        set {instance = value; }
    }
    static public void Init () {
        
    }

    /// <summary>
    /// 全局延迟执行方法
    /// </summary>
    public void DelayInvoke(float time,Action callback)
    {
        StartCoroutine(Send(time,callback));// 延迟一帧发送
    }
    //通知监听数据的对象数据发生变化
    protected IEnumerator Send(float time, Action callback)
    {
        if(time == 0){
            yield return null;
        }else{
            yield return new WaitForSeconds (time);
        }
        callback();
        yield break;
    }
}
