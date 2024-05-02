using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    public float speedRun = 5f;
    //[SerializeField] float speedPlayerAir = .5f;
    //[SerializeField] float speedPlayerGroud = 5f;
    [SerializeField] float speedJump = 12f;
    [SerializeField] float groundCheckRadius = 0.3f; 

    public int velocityView = 0; 

    float MoveInput = 0f;  

    [SerializeField] bool isRun;
    [SerializeField] bool canJump;
    [SerializeField] bool jumping;
    [SerializeField] bool isGrounded ;
    [SerializeField] bool canFlip = true;
    [SerializeField] int amountOfJumpsLeft; 
    public int amountOfJumps = 1;
    public int viewStage;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Char_anim char_Anim;
    Rigidbody2D myRigibody;
    
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        char_Anim = GetComponent<Char_anim>();
        amountOfJumpsLeft = amountOfJumps;        
    }
    void Update()
    {          
        CheckInput();
        char_Anim.stagejump = Mathf.RoundToInt(myRigibody.velocity.y); 
        CheckGround();
        ChecIfCanJump();
        Move();
        CheckMoveDir();
        ChangeStage();
        viewStage = char_Anim.stage;
        if(char_Anim.stage == 4){
            Debug.Log("stage");
        } 
        
    }
    private void FixedUpdate() {            
               
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
            Attack(); 
        }
    }
    void Attack(){
        //atking = true;
       
        
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

    void DisableFlip(){
        canFlip = false;
    }

    void EnableFlip(){
        canFlip = true;
    }

    void Flip(){
        if(canFlip){
             transform.localScale = new Vector3(Mathf.Sign(MoveInput),1f,1f);
        }
    }
    void CheckMoveDir(){
        if(Mathf.Abs(myRigibody.velocity.x) >= 0.01f){
            isRun = true;
            Flip();       
        }else{
            isRun = false;
        }
    }
    void ChangeStage(){
        if(isGrounded){
            char_Anim.stage = 0;
            if(isRun){
            char_Anim.stage = 1;
            }
        }else{
            if(jumping){
                char_Anim.stage = 2;
            }else{
                char_Anim.stage = 3;
            }
        }

    }
}
