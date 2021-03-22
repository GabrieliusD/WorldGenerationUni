using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (Maps))]
public class MapGenEditior : Editor
{
    public override void OnInspectorGUI()
    {
        Maps map = (Maps)target;

        if(DrawDefaultInspector())
        {
            if(map.autoUpdate)
            {
                map.GenerateMap();
            }
        }
        if(GUILayout.Button("Generate"))
        {
            map.GenerateMap();
        }
        DrawColorEditor(map.colorSettings, map.GenerateMap);
    }
    void DrawColorEditor(Object settings, System.Action onSettingsUpdated)
    {
        using(var check = new EditorGUI.ChangeCheckScope())
        {
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if(onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }
    }
}
