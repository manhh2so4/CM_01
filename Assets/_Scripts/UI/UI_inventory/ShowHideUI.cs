using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShowHideUI : MonoBehaviour
{
    [SerializeField] GameObject uiContainer = null;
    // Start is called before the first frame update
    void Start()
    {
        uiContainer.SetActive(false);
    }
    public void Toggle(InputAction.CallbackContext context){
        if(context.started)
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
        }
    }
    public void ToggleUI(){

        uiContainer.SetActive(!uiContainer.activeSelf); 

    }

}
