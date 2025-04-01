using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShowHideUI : MonoBehaviour
{
    [SerializeField] GameObject ObjectUI = null;
    void Start()
    {
        ObjectUI.SetActive(true);
        ObjectUI.SetActive(false);
    }
    public void ToggleInput(InputAction.CallbackContext context){
        if(context.started)
        {
            Toggle();
        }
    }
    public void Toggle(){
        UI_selected.SetSelected(null);
        ObjectUI.SetActive(!ObjectUI.activeSelf);
       
    }

}
