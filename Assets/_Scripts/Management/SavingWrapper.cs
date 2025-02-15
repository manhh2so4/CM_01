using HStrong.Saving;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavingWrapper : MonoBehaviour {
    const string defaultSaveFile = "save";
    public void OnLoad(InputAction.CallbackContext context){
        if(context.started)
        {
            Debug.Log("on Load");
            Load();
        }
    }

    public void OnSave(InputAction.CallbackContext context){
        if(context.started)
        {
            Debug.Log("on Save");
            Save();
        }
    }
    
    private void Load(){
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }

    private void Save(){
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
}