using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShowHideUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnInvent(InputAction.CallbackContext context){
        if(context.started)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

}
