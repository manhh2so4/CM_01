using UnityEditor;
using UnityEngine;

public class Boss : Entity{
    #region State Variable
    public FiniteStateMachine StateMachine{get;private set;}
    #endregion
    //----------------------
    #region Component
    public Draw_boss draw_Boss {get;private set;}
    public EnemyBoss_SO bossData;
    Vector3 Bpos;
    //----------View_data
    public bool canAttack;
    public bool canChangeState;
    public StateEnemy state;
    public float minIdleTime;
    public float maxIdleTime;
    #endregion
    
    #region Setup_Enemy
    public LayerMask LayerCombat;
    public Transform playerCheck;
    protected CapsuleCollider2D PlayerCheck;
    protected BoxCollider2D ledgeCheck;
    protected CapsuleCollider2D wallCheck;
    public FiniteStateMachine stateMachine;  
    //---------------- Set State----------------------------

    public B_IdleState idleState { get; private set;}
    public B_MoveState moveState { get; private set;}

    #endregion


    protected override void Awake(){
        base.Awake();
        LoadComponent();
        LoadCore();
        moveState = new B_MoveState(this, stateMachine);
        idleState = new B_IdleState(this, stateMachine);

    }
    private void Start() {
        stateMachine.Initialize(moveState);
        Bpos = transform.position;
        playerCheck = null;  
    }
    public virtual void Update()
    {
        canChangeState = stateMachine.canChange;
        stateMachine.CurrentState.LogicUpdate();
        core.LogicUpdate();
    }
    private void Reset() {
        LoadComponent();
        LoadCore();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #region FuncLoad
    private void LoadCore(){
        draw_Boss = GetComponent<Draw_boss>();
        bossData = draw_Boss.DataBoss_SO;
        stateMachine = new FiniteStateMachine();
    }
    private void LoadComponent(){
        
        if(ledgeCheck == null) ledgeCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>(); 
        if(PlayerCheck == null) PlayerCheck = transform.Find("PlayerDetected").GetComponent<CapsuleCollider2D>();
    }
    #endregion
    #region ColCheck
    public bool isledge()
    {
        return ledgeCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isWall()
    {
        return wallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isGround()
    {
        return mCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if ( (LayerCombat.value & (1 << other.gameObject.layer)) != 0)
        {
            if (other.tag == "Enemy") return;
            playerCheck = other.transform;
        }
    }
    #endregion
    public virtual void OnDrawGizmos(){
         Gizmos.DrawLine(Bpos,transform.position);
         if(playerCheck) {
            Gizmos.DrawLine(playerCheck.position,transform.position);
            Handles.color = Color.red;
            Handles.Label((playerCheck.position+transform.position)/2, Vector2.Distance(playerCheck.position,transform.position).ToString());
         }
    }
}