using UnityEditor;
using UnityEngine;

public class Boss : EnemyEntity{
    #region State Variable
    public FiniteStateMachine StateMachine{get;private set;}
    #endregion
    //----------------------
    #region Component
    public Draw_boss draw_Boss {get;private set;}
    public EnemyBoss_SO bossData;
    //----------View_data
    #endregion
    
    #region Setup_Enemy
    //---------------- Set State----------------------------

    public B_IdleState idleState { get; private set;}
    public B_MoveState moveState { get; private set;}
    public B_ChargeState chargeState { get; private set;}
    public B_LookState lookState { get; private set;}
    public B_Attack_1 attack_1 { get; private set;}
    public B_Attack_2 attack_2 { get; private set;}
    public B_Jump jump { get; private set;}

    #endregion
    protected override void Awake(){
        base.Awake();
        LoadComponent();
        LoadCore();
        moveState = new B_MoveState(this, stateMachine);
        idleState = new B_IdleState(this, stateMachine);
        chargeState = new B_ChargeState(this,stateMachine);
        lookState = new B_LookState(this,stateMachine);
        attack_1 = new B_Attack_1(this,stateMachine);
        attack_2 = new B_Attack_2(this,stateMachine);
        jump = new B_Jump(this,stateMachine);
    }
    private void Start() {
        stateMachine.Initialize(moveState);
        Epos = transform.position;
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
    protected override void LoadCore(){
        base.LoadCore();
        draw_Boss = GetComponent<Draw_boss>();
        bossData = draw_Boss.DataBoss_SO;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if ( (LayerCombat.value & (1 << other.gameObject.layer)) != 0)
        {
            if (other.tag == "Enemy") return;
            playerCheck = other.transform;
        }
    }
    #endregion
}