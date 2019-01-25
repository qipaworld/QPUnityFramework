using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[ExecuteInEditMode]
public class SplitAtlas 
{
    [MenuItem("QP/SplitAtlas")]
    public static void SpriteSlices()
    {
        var selectedGameObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Editable | SelectionMode.TopLevel);
        foreach (Texture2D obj in selectedGameObjects)
        {
            SpriteSlice(obj);
        }
        Debug.Log("SplitAtlas....OK");
    }
    public static void SpriteSlice(Texture2D image)
    {
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称  
        string path = AssetDatabase.GetAssetPath(image);//图片路径名称  
        
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口  
        bool oldIsRead = texImp.isReadable;
        
        if(oldIsRead == false)
        {
            texImp.isReadable = true;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        if (!Directory.Exists(rootPath + "/QPSplitAtlasOut" + image.name))
        {
            AssetDatabase.CreateFolder(rootPath, "QPSplitAtlasOut" + image.name);//创建文件夹  
        }

        foreach (SpriteMetaData metaData in texImp.spritesheet)//遍历小图集  
        {
            string filePath = rootPath + "/QPSplitAtlasOut" + image.name + "/" + metaData.name + ".png";
            if (File.Exists(filePath))
            {
                SpriteDataCopy(metaData, filePath, texImp);

                Debug.Log("文件已存在"+filePath);
                continue;
            }
            Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

            //abc_0:(x:2.00, y:400.00, width:103.00, height:112.00)  
            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y轴像素  
            {
                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                    myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
            }

            //转换纹理到EncodeToPNG兼容格式  
            if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
                newTexture.SetPixels(myimage.GetPixels(0), 0);
                myimage = newTexture;
            }
            var pngData = myimage.EncodeToPNG();
            File.WriteAllBytes(filePath, pngData);
            // 刷新资源窗口界面  
            AssetDatabase.Refresh();

            SpriteDataCopy(metaData, filePath, texImp);
        }
        if (oldIsRead == false)
        {
            texImp.isReadable = false;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
    static void SpriteDataCopy(SpriteMetaData metaData,string path, TextureImporter rootTexture)
    {
        TextureImporter texImp3 = AssetImporter.GetAtPath(path) as TextureImporter;
        TextureImporterSettings texSettings = new TextureImporterSettings();
        texImp3.ReadTextureSettings(texSettings);
        texSettings.spriteAlignment = metaData.alignment;
        texSettings.spriteBorder = metaData.border;
        texSettings.spritePivot = metaData.pivot;
        texSettings.spritePixelsPerUnit = rootTexture.spritePixelsPerUnit;
        texImp3.SetTextureSettings(texSettings);
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}
    
