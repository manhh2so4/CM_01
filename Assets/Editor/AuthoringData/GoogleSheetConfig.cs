using System;
using System.IO;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Internal;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "GoogleSheetConfig", menuName = "Data/GoogleSheetConfig", order = 0)]
public class GoogleSheetConfig : ScriptableObject {
    public string GoogleSheetId;
    public string CredentialPath = "Assets/Data/KEYgoogle.json";

    public MonoScript targetScriptFile ;


    [Button]
    public async void ConvertFromGoogle()
    {
        if(string.IsNullOrEmpty(GoogleSheetId) || string.IsNullOrEmpty(CredentialPath))
        {
            Debug.LogError("GoogleSheetId, CredentialPath is required");
            return;
        }

        GoogleSheetConverter googleConverter = new GoogleSheetConverter( GoogleSheetId , File.ReadAllText(CredentialPath));
        Debug.Log("Loaded GoogleSheet "+ targetScriptFile.name + "..........");
        BaseSheetContainer instanceSheet = null;
        bool isBake = false;
        
        object instance = Activator.CreateInstance( targetScriptFile.GetClass() );
        instanceSheet = instance as BaseSheetContainer;
        isBake = await instanceSheet.Bake(googleConverter);
        
        if(isBake){
            instanceSheet.BakeData();
            Debug.Log("<color=green>Load Data Success</color>");
        }else{
            Debug.Log("<color=red>Load Data Failed</color>");
        }
        AssetDatabase.Refresh();
    }
}