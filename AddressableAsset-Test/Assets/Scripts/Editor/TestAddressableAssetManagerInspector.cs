using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestAddressableAssetManager), true)]
[CanEditMultipleObjects]
public class TestAddressableAssetManagerInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawGUI();
    }

    private void DrawGUI()
    {
        if (GUILayout.Button("Load - 1"))
        {

        }

        if (GUILayout.Button("Load - 2"))
        {

        }
    }
}
