using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (Maps))]
public class MapGenEditior : Editor
{
    chunks chunk;
    private void OnEnable()
    {
        chunk = FindObjectOfType<chunks>();
    }
    public override void OnInspectorGUI()
    {
        Maps map = (Maps)target;

        if(DrawDefaultInspector())
        {
            if(map.autoUpdate)
            {
                //map.GenerateMap();
                chunk.createChunks();
            }
        }
        if(GUILayout.Button("Generate"))
        {
            //map.GenerateMap();
            chunk.createChunks();
        }
        DrawColorEditor(map.colorSettings, chunk.createChunks);
        DrawSettings(map.noiseSettings);

    }
    void DrawSettings(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
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
