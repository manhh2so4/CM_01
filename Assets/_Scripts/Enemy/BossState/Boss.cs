
using System.Collections.Generic;
using System.Threading;
using HStrong.ProjectileSystem;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Boss : EnemyEntity{
    // #region State Variable
    // #endregion
    //----------------------
    #region Component

    public Draw_boss draw_Boss {get;private set;}
    public EnemyBoss_SO bossData;

    #endregion
    //----------View_data---------------------
    #region View Data_log
    public Cooldown cooldowns = new Cooldown();
    #endregion
    //----------View_data---------------------
    #region DataCombat
    [SerializeField] public Projectile PrefabProjectile;
    [SerializeField] LayerMask layerCombat;
    [SerializeField] Effect_Instance prefabHit;
    [SerializeField] Collider2D colWeapon;
    public HitBox hitBoxWeapon;
    public HitBox hitBoxBody;
    #endregion

    #region State Variable
    //---------------- Set State----------------------------

    public B_IdleState idleState { get; private set;}
    public B_MoveState moveState { get; private set;}
    public B_ChargeState chargeState { get; private set;}
    public B_LookState lookState { get; private set;}
    public B_Attack attack_1 { get; private set;}
    public B_Attack attack_2 { get; private set;}
    public B_Jump jump { get; private set;}
    public B_Dash dash { get; private set;}
    public B_Rage rage { get; private set;}
    public B_DeadState deadState { get; private set;}
    public B_StuneState stuneState { get; private set;}
    public BossAbilityState[] abilitys;
    #endregion
    protected override void Awake(){
        base.Awake();
        LoadComponent();
        FistSetUp();

        core.height = mCollider.size.y + .7f;
        core.size = new Vector2(3,2);

        moveState = new B_MoveState(this, stateMachine);
        idleState = new B_IdleState(this, stateMachine);
        chargeState = new B_ChargeState(this,stateMachine);
        lookState = new B_LookState(this,stateMachine);
        attack_1 = new B_Attack(this,stateMachine,StateEnemy.Skill_1);
        attack_2 = new B_Attack(this,stateMachine,StateEnemy.Skill_2);

        jump = new B_Jump(this,stateMachine);
        dash = new B_Dash(this,stateMachine);
        rage = new B_Rage(this,stateMachine);
        deadState = new B_DeadState(this,stateMachine);
        stuneState = new B_StuneState(this,stateMachine);
        
        abilitys = new BossAbilityState[]{ jump, dash, rage};

    }
    void Start(){

        stateMachine.Initialize(moveState);
       
    }
    void Update()
    {
        canChangeState = stateMachine.canChange;
        core.LogicUpdate();
        cooldowns.Update();
        draw_Boss.StageBoss();
        CheckReceiver();
        stateMachine.CurrentState.LogicUpdate();
    }
    private void OnEnable()
    {
        hitBoxWeapon.OnHit += TakeDamage;
        hitBoxBody.OnHit += TakeDamage;
        CharStats.onHealthZero += CheckDead;
    }
    private void OnDisable()
    {
        hitBoxWeapon.OnHit -= TakeDamage;
        hitBoxBody.OnHit -= TakeDamage;
        CharStats.onHealthZero -= CheckDead;
    }

    #region FuncLoad
    private void CheckReceiver(){
        
        if(poiseReceiver.IsPoise()){
            stateMachine.ChangeState(stuneState);
        }
    }
    private void CheckDead(){
        stateMachine.ChangeState(deadState);
    }
    List<int> IdAbility = new List<int>();
    public State GetAbility(){
        IdAbility.Clear();
        for (int i = 0; i < abilitys.Length; i++){
            if(cooldowns.IsDone(abilitys[i])) IdAbility.Add(i);
        }

        if(IdAbility.Count == 0) return null;

        int index2 = Random.Range(0, IdAbility.Count);
        return abilitys[ IdAbility[index2] ];

    }
    void TakeDamage(Collider2D other){
        if(other.TryGetComponent(out IDamageable damageable)){
            CharStats.DoDamage( damageable.Target(prefabHit) );
        }
        if(other.TryGetComponent(out IKnockBackable knockBackable)){
            knockBackable.KnockBack(new Vector2(1,1), 5, movement.facingDirection);
        }
    }
    protected override void FistSetUp(){
        base.FistSetUp();
        draw_Boss = GetComponent<Draw_boss>();
        bossData = draw_Boss.DataBoss_SO;
        Epos = transform.position;
        playerCheck = null; 

        hitBoxWeapon = new HitBox( colWeapon,layerCombat,3, this.tag );
        hitBoxBody = new HitBox( mCollider,layerCombat,3, this.tag );
    }
    private void OnTriggerEnter2D(Collider2D other){
        if( playerCheck ) return;
        if (other.tag == "Enemy") return;
        playerCheck = other.transform;
        
        cooldowns.Start( dash, 10 );
        cooldowns.Start( jump, 10 );
        cooldowns.Start( rage, 20 );
    }
    #endregion
}
