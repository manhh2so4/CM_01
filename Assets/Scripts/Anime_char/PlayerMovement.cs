using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speedRun = 5f;
    //[SerializeField] float speedPlayerAir = .5f;
    //[SerializeField] float speedPlayerGroud = 5f;
    [SerializeField] float speedJump = 5f;
    [SerializeField] float groundCheckRadius = 5f; 

    public static int velocityView = 0; 

    float MoveInput = 0f;  

    [SerializeField] bool isRun;
    [SerializeField] bool canJump;
    [SerializeField] bool jumping;
    [SerializeField] bool isGrounded ;
    [SerializeField] bool atking = false;

    [SerializeField] int amountOfJumpsLeft;
    public int amountOfJumps = 1;
    public static int indexStage  = 0;
    public static int stagejump  = 0;
    public int viewStage;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    Rigidbody2D myRigibody;
    
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;        
    }
    void Update()
    {          
        CheckInput();
        velocityView = (int)myRigibody.velocity.y; 
        
    }
    private void FixedUpdate() {            
        CheckGround();
        ChecIfCanJump();
        Move();
        CheckMoveDir();
        ChangeStage();
        viewStage = indexStage;           
    }
    void Move(){
        myRigibody.velocity = new Vector2 (MoveInput*speedRun,myRigibody.velocity.y);       
    }
    void CheckInput(){
        
        MoveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")){
            Jump(); 
        }
        if(Input.GetButtonDown("Fire1")){
           // Attack(); 
        }
    }
    void Attack(){
        indexStage = 4;
        atking = true;
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
            myRigibody.velocity  = new Vector2(myRigibody.velocity.x ,speedJump);

            amountOfJumpsLeft--;
        }
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
