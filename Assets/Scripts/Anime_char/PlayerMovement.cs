using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speedRun = 5f;
    [SerializeField] float speedPlayerAir = .5f;
    [SerializeField] float speedPlayerGroud = 5f;
    [SerializeField] float speedJump = 5f;
    float MoveInput = 0f;
    float MoveJump = 0f;
    bool isRun = false;
    Rigidbody2D myRigibody;
    BoxCollider2D myfeedColider;
    public int indexStage  = 0;
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myfeedColider = GetComponent<BoxCollider2D>();       
    }
    void Update()
    {
        CheckInput();
        CheckJump();
    }
    private void FixedUpdate() {    
         
        Move();    
        Flip();          
    }
    void Move(){
        myRigibody.velocity = new Vector2 (MoveInput*speedRun,myRigibody.velocity.y);
        isRun = Mathf.Abs(myRigibody.velocity.x) > Mathf.Epsilon;
        if(!isRun){
            indexStage = 0;
        }
        if(myfeedColider.IsTouchingLayers(LayerMask.GetMask("Ground")) && MoveJump > 0){
            myRigibody.velocity  = new Vector2(myRigibody.velocity.x,speedJump*MoveJump);     
        }
        if(!myfeedColider.IsTouchingLayers(LayerMask.GetMask("Ground"))){    
            speedRun = speedPlayerAir;
            isRun = false;
        }else{
             speedRun = speedPlayerGroud;
        }
    }
    void CheckInput(){
        MoveInput = Input.GetAxisRaw("Horizontal");
    }
    void CheckJump(){
        MoveJump = Input.GetAxisRaw("Vertical");
    }
    private void Flip(){
        if(isRun){
        indexStage = 2;
        transform.localScale = new Vector3(Mathf.Sign(myRigibody.velocity.x),1f,1f);
        }
    }  
}
