using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tex_NjPart_SO))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tex_NjPart_SO myScriptableObject = (Tex_NjPart_SO)target;

        DrawDefaultInspector();
        // Add a button that calls MyMethod()
        if (GUILayout.Button("Call MyMethod"))
        {
            myScriptableObject.AddData();
        }
    }
}
