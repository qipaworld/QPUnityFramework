using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotController : MonoBehaviour {
    public static ScreenshotController instance = null;
    //获得Manager 实例
    static public ScreenshotController Instance
    {
        get {
            return instance;
        }
    }
    
// Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
    }
      
	/// <summary>
    /// 使用Application类下的CaptureScreenshot()方法实现截图
    /// 优点：简单，可以快速地截取某一帧的画面、全屏截图
    /// 缺点：不能针对摄像机截图，无法进行局部截图
    /// </summary>
    /// <param name="mFileName">M file name.</param>
    public void CaptureByUnity (string mFileName)
    {
        ScreenCapture.CaptureScreenshot (mFileName, 0);
    }

    /// <summary>
    /// 根据一个Rect类型来截取指定范围的屏幕, 左下角为(0,0)
    /// 读取屏幕像素存储为纹理图片
    /// </summary>
    /// <param name="mRect">M rect.截屏的大小</param>
    /// <param name="mFileName">M file name.保存路径</param>
    /// <param name="callback">获得文理的回掉方法</param>
    public IEnumerator CaptureByRect (Rect mRect, Action<Texture2D> callback = null , string mFileName = null)
    {
        //等待渲染线程结束
        yield return new WaitForEndOfFrame ();
        //初始化Texture2D, 大小可以根据需求更改
        Texture2D mTexture = new Texture2D ((int)mRect.width, (int)mRect.height,
                                 TextureFormat.RGB24, false);
        //读取屏幕像素信息并存储为纹理数据
        mTexture.ReadPixels (mRect, 0, 0);
        //应用
        mTexture.Apply ();

        if(mFileName != null){

	        //将图片信息编码为字节信息
	        byte[] bytes = mTexture.EncodeToPNG ();  
	        //保存
	        System.IO.File.WriteAllBytes (mFileName, bytes);
        }

        if(callback != null){
        	callback(mTexture);
        }
    }

    /// <summary>
    /// 指定相机截图
    /// </summary>
    /// <returns>The by camera.</returns>
    /// <param name="mCamera">M camera.要被截屏的相机</param>
    /// <param name="mRect">M rect. 截屏的区域</param>
    /// <param name="mFileName">M file name.</param>
    /// <param name="callback">获得文理的回掉方法</param>
    public IEnumerator  CaptureByCamera (Camera mCamera, Rect mRect, Action<Texture2D> callback = null , string mFileName = null)
    {
        //等待渲染线程结束
        yield return new WaitForEndOfFrame ();
        //初始化RenderTexture   深度只能是【0、16、24】截不全图请修改
        RenderTexture mRender = new RenderTexture ((int)mRect.width, (int)mRect.height,16);
        //设置相机的渲染目标
        mCamera.targetTexture = mRender;
        //开始渲染
        mCamera.Render ();
        //激活渲染贴图读取信息
        RenderTexture.active = mRender;
        Texture2D mTexture = new Texture2D ((int)mRect.width, (int)mRect.height, TextureFormat.RGB24, false);
        //读取屏幕像素信息并存储为纹理数据
        mTexture.ReadPixels (mRect, 0, 0);
        //应用
        mTexture.Apply ();
        //释放相机，销毁渲染贴图
        mCamera.targetTexture = null;   
        RenderTexture.active = null; 
        GameObject.Destroy (mRender);  
        
        if(mFileName != null){
	        //将图片信息编码为字节信息
	        byte[] bytes = mTexture.EncodeToPNG ();  
	        //保存
	        System.IO.File.WriteAllBytes (mFileName, bytes);
        }
        
        if(callback != null){
        	callback(mTexture);
        }
    }
}