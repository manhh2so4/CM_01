using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public float speedRun = 5f;
    [SerializeField] float speedPlayerAir = .5f;
    [SerializeField] float speedPlayerGroud = 5f;
    [SerializeField] float speedJump = 5f;
    [SerializeField] float groundCheckRadius = 5f; 

    public int velocityView = 0; 

    float MoveInput = 0f;  

    [SerializeField] bool isRun;
    [SerializeField] bool canJump;
    [SerializeField] bool jumping;
    [SerializeField] bool isGrounded ;

    [SerializeField] int amountOfJumpsLeft;
    public int amountOfJumps = 1;
    public int indexStage  = 0;
    public Transform groundCheck;
    public Transform target;
    public LayerMask whatIsGround;
    Rigidbody2D myRigibody;
    BoxCollider2D myfeedColider;
    
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myfeedColider = GetComponent<BoxCollider2D>();
        amountOfJumpsLeft = amountOfJumps; 
        velocityView = (int)myRigibody.velocity.y;       
    }
    void Update()
    {          
              
    }
    private void FixedUpdate() {  
        CheckInput(); 
        velocityView = (int)myRigibody.velocity.y;                
        CheckGround();
        ChecIfCanJump();
        Move();
        CheckMoveDir();
        ChangeStage();            
    }
    void Move(){
        myRigibody.velocity = new Vector2(MoveInput*speedRun,myRigibody.velocity.y);       
    }
    void CheckInput(){
        if (this.target == null) return;
        Vector3 normal = (target.position - this.transform.position);
        if(normal.magnitude > 2){
        this.MoveInput = normal.normalized.x;
            if(normal.normalized.y > 0.5){
                Jump();
            }
        }else{
            this.MoveInput = 0;
        }
    }
    void ChecIfCanJump(){
        if( isGrounded && myRigibody.velocity.y <=0){
            amountOfJumpsLeft = amountOfJumps;
            jumping = false;
        }
        if(amountOfJumpsLeft <= 0){
            canJump = false;
        }else{
            canJump = true;
        }
    }
    void Jump(){
        if(canJump){
            jumping = true;
            myRigibody.velocity  = new Vector2(myRigibody.velocity.x,speedJump);
            amountOfJumpsLeft--;
        }
        // if(!myfeedColider.IsTouchingLayers(LayerMask.GetMask("Ground"))){    
        //     speedRun = speedPlayerAir;
        // }else{
        //      speedRun = speedPlayerGroud;
        // }
    }
    void CheckGround(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, whatIsGround);
    }
    void CheckMoveDir(){
        if(MoveInput != 0){
            isRun = true;
            transform.localScale = new Vector3(Mathf.Sign(MoveInput),1f,1f);
        }else{
            isRun = false;
        }
    }
    void ChangeStage(){
        if(isGrounded){
            indexStage = 0;
            if(isRun){
            indexStage = 1;
            }
        }else{
            if(jumping){
                indexStage = 2;
            }else{
                indexStage = 3;
            }
        }
    }
}
