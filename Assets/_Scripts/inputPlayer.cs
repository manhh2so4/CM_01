using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputPlayer : MonoBehaviour
{
    float MoveInput;
    public void OnMoveInput(InputAction.CallbackContext context){
        MoveInput = context.ReadValue<float>();
        Debug.Log("Move : "  + MoveInput);
    }
    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            Debug.Log("isJump");
        }
        if(context.performed){
            Debug.Log("isJump_press");
        }
        if(context.canceled){
            Debug.Log("isJump_canceled");
        }
    }
    public void OnFireInput(InputAction.CallbackContext context){
        Debug.Log("Fire");
    }

}
