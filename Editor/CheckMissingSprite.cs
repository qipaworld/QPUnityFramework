using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 检查GameObject上的Image组件是否引用了丢失的精灵资源。
/// </summary>
public class CheckMissingSprite
{

    [MenuItem("QP/CheckMissingSprite")]
    static void CheckMissing()
    {
        var selectedGameObjects = Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel);
        
        if (selectedGameObjects.Length <= 0)
        {
            Debug.LogError("请先选择GameObject，再执行此导出指令。");
            return;
        }
            
        foreach (GameObject rootGameObject in selectedGameObjects)
        {
            bool includeInactive = true;
            SpriteRenderer[] allImages = rootGameObject.GetComponentsInChildren<SpriteRenderer>(includeInactive);
            //int missingCount = 0;
            for (int i = 0; i < allImages.Length; i++)
            {
                var image = allImages[i];
                Sprite sprite = image.sprite;
                if (ReferenceEquals(sprite, null))
                {
                    // 编辑器下永远不会返回C#引用null。这条log永远不会出现。
                    string path = GetPath(rootGameObject.transform, image.transform);
                    Debug.Log("找到一个sprite属性为null的Image组件。点击这条消息以定位。" + path, image);
                }
                else
                {
                    // UnityEngine.Object 自定义的相等性检查。
                    // 在编辑器环境下，Missing状态的C#对象引用非null，但是UnityEngine.Object 自定义的相等性检查与null比较返回true
                    if (sprite == null)
                    {
                        // 尝试访问这个sprite的name属性时，将会返回两种不同的异常。
                        try
                        {
                            string name = sprite.name;
                        }
                        catch (MissingReferenceException)
                        {
                            //引用的资源丢失，已经不在工程中。Inspector面板的Missing状态。
                            string path = GetPath(rootGameObject.transform, image.transform);
                            Debug.Log("找到一个sprite属性为Missing的Image组件。点击这条消息以定位。" + path, image);
                        }
                        catch (UnassignedReferenceException)
                        {
                            //完全没有赋值。Inspector面板展示为None状态。
                        }

                    }
                    else
                    {
                        
                    }
                }
            }
        }
        
        Debug.Log("CheckMissingSprite...OK");
    }

    /// <summary>
    /// 获取<paramref name="node"/>节点相对<paramref name="root"/>的路径
    /// </summary>
    /// <param name="root">根节点</param>
    /// <param name="node">目标节点</param>
    /// <returns>返回路径</returns>
    public static string GetPath(Transform root, Transform node)
    {
        List<string> names = new List<string>();
        while (node != null && node != root)
        {
            names.Add(node.name);
            node = node.parent;
        }

        if (node == null)
        {
            throw new Exception("目标节点不是根节点的子物体！");
        }

        names.Add(root.name);

        names.Reverse();
        return string.Join("/", names.ToArray());
    }
}