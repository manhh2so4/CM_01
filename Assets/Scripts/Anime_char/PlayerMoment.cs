using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    [SerializeField] bool isRun,canJump,jumping,isGrounded,canFlip = true;
    [Header("Player_Property")]
    public float speedRun = 5f;
    [SerializeField] float speedJump = 12f;
    public int velocityView = 0; 
    float MoveInput = 0f;
    [SerializeField] int amountOfJumpsLeft; 
    public int amountOfJumps = 1;
    public int viewStage;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Char_anim char_Anim;
    Rigidbody2D myRigibody;
    CapsuleCollider2D myColider;
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myColider = GetComponent<CapsuleCollider2D>();
        char_Anim = GetComponent<Char_anim>();
        amountOfJumpsLeft = amountOfJumps;        
    }
    void Update()
    {          
        CheckInput();
        char_Anim.stagejump = Mathf.RoundToInt(myRigibody.velocity.y); 
        ChecIfCanJump();
        Move();
        ChangeStage();
        viewStage = char_Anim.stage;       
    }
    private void FixedUpdate() {            
               
    }
    void Move(){
        
        if(char_Anim.isAtacking){
            MoveInput = 0;
        }
        myRigibody.velocity = new Vector2 (MoveInput*speedRun, myRigibody.velocity.y);
        if(Mathf.Abs(myRigibody.velocity.x) >= 0.01f){
            isRun = true;
            Flip();       
        }else{
            isRun = false;
        }       
    }
    void CheckInput(){
        if(char_Anim.isAtacking) return;
        MoveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")){            
            Jump(); 
        }
    }
    void ChecIfCanJump(){
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, whatIsGround);
        isGrounded = myColider.IsTouchingLayers(LayerMask.GetMask("Ground"));
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
    private void OnTriggerEnter2D(Collider2D other) {
        
    }
    void Jump(){
        if(canJump){
            jumping = true;
            myRigibody.velocity  = new Vector2(myRigibody.velocity.x ,speedJump);
            amountOfJumpsLeft--;
        }
    }
    public void DisableFlip(){
        canFlip = false;
    }
    public void EnableFlip(){
        canFlip = true;
    }
    void Flip(){
        if(char_Anim.isAtacking) return;
        if(canFlip){
             transform.localScale = new Vector3(Mathf.Sign(MoveInput),1f,1f);
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
