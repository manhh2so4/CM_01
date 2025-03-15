using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditor.Callbacks;
using System.Linq;
namespace HStrong.Quests{

    [CustomEditor(typeof(QuestInfoSO))]
    public class Q_StepDataSOEditor : Editor {
        private static List<Type> dataCompTypes = new List<Type>();
        private QuestInfoSO dataSO;
        private bool showAddComponentButtons;
        private void OnEnable()
        {
            dataSO = target as QuestInfoSO;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Steps Quest");
            if (showAddComponentButtons){
                foreach (var dataComp in dataCompTypes)
                {
                    if(GUILayout.Button(ObjectNames.NicifyVariableName(dataComp.Name))){
                        var comp = Activator.CreateInstance(dataComp) as Q_StepData;
                            
                        if(comp == null)
                            return;

                        dataSO.AddData(comp);
                    }
                }
            }

            if(GUILayout.Button("Force Update Componet Names")){
                foreach (var item in dataSO.GetComponentData())
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
                type => type.IsSubclassOf(typeof(Q_StepData)) && !type.ContainsGenericParameters && type.IsClass
            );
            dataCompTypes = filteredTypes.ToList();
        }
    }

}
