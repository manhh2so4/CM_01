using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditor.Callbacks;
using System.Linq;

[CustomEditor(typeof(SkillData_SO))]
public class WeaponDataSOEditor : Editor {
    private static List<Type> dataCompTypes = new List<Type>();
    private SkillData_SO dataSO;
    private bool showAddComponentButtons;
    private void OnEnable()
    {
        dataSO = target as SkillData_SO;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");
        if (showAddComponentButtons){
            foreach (var dataComp in dataCompTypes)
            {
                if(GUILayout.Button(dataComp.Name)){
                    var comp = Activator.CreateInstance(dataComp) as ComponentData;
                        
                        if(comp == null)
                            return;

                        dataSO.AddData(comp);
                }
            }
        }

        if(GUILayout.Button("Force Update Componet Names")){
            foreach (var item in dataSO.ComponentData)
            {
                item.SetComponentName();
            }
        }
        
        
    }
    [DidReloadScripts]
    private static void OnRecompile(){
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
        );
        dataCompTypes = filteredTypes.ToList();
    }
}
