using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShowHideUI : MonoBehaviour
{
    [SerializeField] GameObject InventContainer = null;
    [SerializeField] GameObject QuestContainer = null;
    // Start is called before the first frame update
    void Start()
    {
        InventContainer.SetActive(false);
        QuestContainer.SetActive(false);
    }
    public void ToggleInven(InputAction.CallbackContext context){
        if(context.started)
        {
            InventContainer.SetActive(!InventContainer.activeSelf);
        }
    }
    public void ToggleQuest(InputAction.CallbackContext context){
        if(context.started)
        {
            QuestContainer.SetActive(!QuestContainer.activeSelf);
        }
    }

}
