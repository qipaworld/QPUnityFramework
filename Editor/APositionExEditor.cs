using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(APositionEx))]
public class APositionExEditor : Editor
{
    APositionEx aPositionEx;
    private void OnEnable()
    {
        aPositionEx = target as APositionEx;
    }
    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        if (Application.isPlaying)
        {
            Handles.DrawDottedLine(aPositionEx.originPosition - aPositionEx.distance, aPositionEx.originPosition + aPositionEx.distance, 10);
        }
        else
        {
            Handles.DrawDottedLine(aPositionEx.transform.position - aPositionEx.distance, aPositionEx.transform.position + aPositionEx.distance, 10);
        }
    }
}

