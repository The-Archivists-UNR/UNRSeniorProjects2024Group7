using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelCreator))]

public class RoomEditor : Editor
{
   public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelCreator levelCreator = (LevelCreator)target;
        if (GUILayout.Button("GenerateNewLevel"))
        {
            levelCreator.CreateLevel(); 
        }
    }
       
}
