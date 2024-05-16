using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Diagnostics;

public class EnemeState : Draw_Enemy
{   
    [Header("EnemeState --------------")]
    [SerializeField] GameObject bloodSp; 
    [SerializeField] bool groundDetected,wallDetected,canFlip; 
    [SerializeField] private State currentState;
    //--------- Enemy Moment -----------  	        
	[SerializeField] float timeStateWait;
    public float frameTimer = 0,timeCount = 0,timeAction = 0;
    public int FrameCurrent = 0; 
    float vY = 1,dirY;
    
    int facingDirection = 1;
    Vector3 posCurrent;
    Vector2 movement;
    //--------- PlayerDetectedState -----------
    [SerializeField] Transform player;
    bool isPlayerInMinAgroRange;
    bool isPlayerInMaxAgroRange;   
    //--------- Enemy Combat -----------
    [SerializeField] bool playerDetected, attackDetected,canAttack,attacking;
    public Ease ease,easeEnd;
    int dir;
    private void Awake() {
        groundDetected = true;
        LoadComponent();
        Load_Enemy();
        SwitchState(State.Moving);
    }
    private void Reset() {
        LoadComponent();
        Load_Enemy();
    }
    private void Update()
	{
		UpdateState();
        
	}
    private void FixedUpdate() {
        Load_Enemy();
        EnemyDetected();
        playerDetected = CheckPlayerInMaxAgroRange();
        attackDetected = CheckPlayerInCloseRangeAction();
    }
    //------------ Ohter Func ----------------
    private void OnTriggerEnter2D(Collider2D other) { 
        if(other.tag == "Dame") {              
        int a =  other.transform.parent.GetComponent<PlayerCombat>().Dame;

        if(other.transform.parent.localPosition.x > transform.localPosition.x) dir = -1;
        else dir = 1;
        TakeDame(a);
        }
        if(other.tag == "Player"){
            player = other.transform;

            playerDetected = true;
            SwitchState(State.Follow_Player);
        }
                             
    }
    
    bool CheckPlayerInMaxAgroRange(){
        return PlayerCheck.IsTouchingLayers(LayerMask.GetMask("Player"));
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return AttackCheck.IsTouchingLayers(LayerMask.GetMask("Player"));
    }

    void EnemyDetected(){
        



        if(type != 4 && type != 5) groundDetected = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));

        
        wallDetected = wallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if ((!groundDetected || wallDetected) && canFlip )
		{               
			Flip();
			return;
		}
    }
    
    
    void CheckDirTarget(Vector3 target){

        if(target.x > transform.localPosition.x) {
            facingDirection = 1;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            facingDirection = -1;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Flip(){        
        facingDirection *= -1;
        gameObject.transform.Rotate(0f,180f,0f);
    }
    void OffFlip(){
        canFlip = false;        
    }
    void OnFlip(){
        canFlip = true;
    }
    void TakeDame(int dame){
        if(Hp > 0){
            Hp -= dame;
            if(attacking) return;
            SwitchState(State.Knockback);
        }else{
            SwitchState(State.Dead);
        }
    }    
    
    //------------ State ----------------
    private void UpdateState() {
        switch (currentState)
		{
        case State.Respawn:
			Respawn();
			break;
		case State.Dead:
			Dead();
			break;
		case State.Idle:
			Idle();
			break;
		case State.Moving:
			Moving();
			break;
        case State.Dash:
			Dash();
			break;
        case State.Follow_Player:
			Follow_Player();
			break;
        case State.Attack:
			Attack();
			break;
		case State.Knockback:
			Knockback();
			break;
		}
    }
	private void SwitchState(State state)
	{
        frameTimer = 99f;
        timeCount = 0f;
		switch (currentState)
		{
        case State.Respawn:
			ExitRespawn();
			break;
		case State.Idle:
			ExitIdle();
			break;
		case State.Moving:
			ExitMoving();
			break;
        case State.Dash:
			ExitDash();
			break;
        case State.Follow_Player:
			ExitFollow_Player();
			break;
        case State.Attack:
			ExitAttack();
			break;
		case State.Knockback:
			ExitKnockback();
			break;
		}
		switch (state)
		{
        case State.Respawn:
			EnterRespawn();
			break;
		case State.Idle:
			EnterIdle();
			break;
		case State.Moving:
			EnterMoving();
			break;
        case State.Dash:
			EnterDash();
			break;
        case State.Follow_Player:
			EnterFollow_Player();
			break;
        case State.Attack:
			EnterAttack();
			break;
		case State.Knockback:
			EnterKnockback();
			break;
		case State.Dead:
			EnterDead();
			break;
		}
		currentState = state;
	}
    //------------ Stage Idle ----------------
    private void EnterIdle(){
        
        timeStateWait = Random.Range(1f,5f);
        Paint(0);

	}
    private void Idle(){

        if(CountDown(timeStateWait) && !playerDetected ){
            OnFlip();
           SwitchState(State.Moving);
           if(GetRandomBoolean()) Flip(); 
        }
        switch(type){
            case 0:
				Paint(0);
				break;
            case 1:
                break;
            case 4:                
                if(TimeRate(0.4f/speedMove)) return;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;
        }
	}
    private void ExitIdle(){
		OffFlip();
	}
    //------------ Stage Moving ----------------
    private void EnterMoving(){
		OnFlip();
        timeStateWait = Random.Range(3f,10f);
	}
	private void Moving(){
        if(CountDown(timeStateWait) && !playerDetected){
            SwitchState(State.Idle);
            return;
        }       
        switch(type){			
			case 0:
				Paint(0);
				break;
            case 1:
                movement.Set( speedMove/3*facingDirection, mRB.velocity.y);
		        mRB.velocity = movement;

                if(TimeRate(0.35f/speedMove)) return;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;                
                break;
            case 2:
                break;

            case 4:
                if(Mathf.Abs(transform.position.x  - enemyPos.x) > RangeMove ){
                    if(!playerDetected) {
                        CheckDirTarget(enemyPos);
                    }
                }
                

                if(TimeRate(0.35f/speedMove)) return;
                if(Mathf.Abs(transform.position.y  - enemyPos.y) > 1.5 ){
                    dirY = enemyPos.y - transform.position.y;
                }else{
                    dirY = Random.Range(-1f,1f);
                }
                vY *= -1;
                movement.Set( speedMove/3*facingDirection, dirY - vY );
		        mRB.velocity = movement;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;

            default:
                Paint(0);
				break;           
        }

        
	}
	private void ExitMoving(){
        mRB.velocity = Vector3.zero;
		OffFlip();
	}
    //------------ Stage Dash ----------------
    private void EnterDash(){
		
	}

	private void Dash(){
		
	}

	private void ExitDash(){
		
	}
    //------------ Stage Attack ----------------
    private void EnterAttack(){
        attacking = true;
        posCurrent = transform.position;       
		transform.DOMove(player.position,0.4f/speedAtk).SetEase(ease)
            .OnComplete(()=>{
                transform.DOMove(posCurrent,0.4f/speedAtk).SetEase(easeEnd)
                .OnComplete(()=>{;
                attacking = false;
                canAttack = false;
                SwitchState(State.Follow_Player);
                });
            });
	}
    public virtual void OnDrawGizmos(){
         Gizmos.DrawLine(player.position,transform.position);
         Gizmos.DrawLine(enemyPos,transform.position);
    }
	private void Attack(){        
       switch(type){			
			case 0:
				Paint(0);
				break;
            case 1:
                if(TimeRate(0.2f/speedMove)) return;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;                
                break;
            case 2:
                break;
            case 4:
                if(TimeRate(0.2f/speedMove)) return;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                break;
            default:
                Paint(0);
				break;           
        }
	}

	private void ExitAttack(){
		
	}
    //------------ Stage Attack ----------------
    private void EnterFollow_Player(){
		speedMove *=1.5f;
	}
	private void Follow_Player(){
        if(canAttack == false) {
        if(TimeAction(1/speedAtk)){
            canAttack = true;
        }
        }
        if(playerDetected == false){
            SwitchState(State.Moving);
        }
        CheckDirTarget(player.transform.position);                        
        if(attackDetected == false){
		    Moving();
        }else{
            mRB.velocity = Vector3.zero;            
            
            if(canAttack == false) {
                Idle();
                return;
            } 
                      
            if(CountDown(0.5f)) {
                SwitchState(State.Attack);                            
            }else{
                
                if(TimeRate(0.3f/speedMove)) return;
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;            
            }
        }    
	}
	private void ExitFollow_Player(){
		speedMove /=1.5f;
	}
    //------------ Stage Knockback ----------------
	private void EnterKnockback(){
        movement.Set(dir,2f);
		mRB.velocity = movement;
        Paint(2);
	}
	private void Knockback(){
        
		if(CountDown(0.3f)){
        if(playerDetected && canAttack) SwitchState(State.Attack);
        else SwitchState(State.Follow_Player);
        } 
	}
	private void ExitKnockback(){
		mRB.velocity = Vector3.zero;
	}
    //------------ Stage Dead ----------------
	private void EnterDead(){
        mCollider.enabled = false;
        mRB.gravityScale = 2;
        movement.Set(dir,4f);
        Paint(2);
		mRB.velocity = movement;		
	}

	private void Dead(){
		if(CountDown(5f)){
            SwitchState(State.Respawn);
        } 
	}
    //------------ Stage Respawn ----------------
    private void EnterRespawn(){

        mRB.velocity = Vector2.zero;
        mCollider.enabled = true;       
		gameObject.SetActive(true);        
        gameObject.transform.localPosition = enemyPos;                
        Hp = HpMax;
        if(type == 4 || type == 5){
            mRB.gravityScale = 0;
        }
        
	}
	private void Respawn(){
		SwitchState(State.Idle);
	}
	private void ExitRespawn(){
		
	}

    //------------ Func Defaut ----------------

    bool TimeRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
        }
        return true;
    }
    bool CountDown(float timeWait){        
        timeCount += Time.deltaTime;
        if(timeCount >= timeWait){
            timeCount = 0;
            return true;
        }
        return false;
    }
    bool TimeAction(float timeWait){        
        timeAction += Time.deltaTime;
        if(timeAction >= timeWait){
            timeAction = 0;
            return true;
        }
        return false;
    }
    
    public bool GetRandomBoolean()
    {
        int randomInt = Random.Range(0, 2);
        return randomInt == 0 ? false : true;
    }
    private enum State
	{
		Idle,
		Moving,
        Dash,
        Follow_Player,
        Attack,
		Knockback,
        Respawn,
		Dead
	}
}
