using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System;


[CustomEditor(typeof(BaseVariable<>), true)]
public class BaseVariableEditor : Editor
{
     private const string NullPropertyText = "SerializedProperty is null. Your custom type is probably missing the [Serializable] attribute";
    private BaseVariable Target { get { return (BaseVariable)target; } }
    private SerializedProperty _valueProperty;
    private SerializedProperty _defaultValueProperty;
    private SerializedProperty _useDefaultProperty;
    private AnimBool _useDefaultValueAnimation;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawValue();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableDebug"));
        serializedObject.ApplyModifiedProperties();

    }
    void OnEnable()
    {
        _valueProperty = serializedObject.FindProperty("_value");
        _defaultValueProperty = serializedObject.FindProperty("_defaultValue");
        _useDefaultProperty = serializedObject.FindProperty("_useDefaultValue");

        _useDefaultValueAnimation = new AnimBool(_useDefaultProperty.boolValue);
        _useDefaultValueAnimation.valueChanged.AddListener(Repaint);
    }

    protected virtual void DrawValue()
    {
        DrawPropertyDrawerLayout(_valueProperty, Target.Type);

        EditorGUILayout.PropertyField(_useDefaultProperty);
        _useDefaultValueAnimation.target = _useDefaultProperty.boolValue;
        using (var anim = new EditorGUILayout.FadeGroupScope(_useDefaultValueAnimation.faded))
        {
            if ( anim.visible )
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    DrawPropertyDrawerLayout(_defaultValueProperty, Target.Type);
                }
            }
        }
    }
    public static void DrawPropertyDrawerLayout(SerializedProperty property, Type type, bool drawLabel = true)
    {
        if(property == null)
        {
            Debug.LogError(NullPropertyText);
            return;
        }

        using (var scope = new EditorGUI.ChangeCheckScope())
        {

            if (drawLabel)
            {
                EditorGUILayout.PropertyField(property);
            }
            else
            {
                EditorGUILayout.PropertyField(property, GUIContent.none);
            }
            
            

            if (scope.changed)
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }
        
    }

    
}

[CustomEditor(typeof(BaseVariable<,>), true)]
public class BaseVariableWithEventEditor : BaseVariableEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_event"));

        serializedObject.ApplyModifiedProperties();
    }
}