using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Portal))]
class Labelhandle : Editor
{
    private static GUIStyle labelStyle;
    private void OnEnable() {
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.LowerCenter;
        labelStyle.fontSize = 30;
    }
    private void OnSceneGUI()
    {
        Portal portal = (Portal)target;

        Handles.BeginGUI();
        Handles.Label(portal.transform.position + new Vector3(0f,.4f,0f), portal._sceneToLoad.SceneName, labelStyle);
        Handles.EndGUI();
    }
    
}