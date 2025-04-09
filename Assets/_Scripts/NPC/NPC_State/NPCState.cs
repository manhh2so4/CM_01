using UnityEngine;

public class NPCState : State {
    protected NPC npc;
    protected PaintChar paintChar;
    protected DataNPC_SO npcData;
    protected FiniteStateMachine stateMachine;
    protected Cooldown cooldowns;
    protected float startTime;

    //------------------- DataLogic -------------------
    protected Vector3 enemyPos;
    protected bool isGrounded,isledge,isWall;
    protected float XDirPos;
    protected float YDirPos;
    protected bool isFollowPath;

    public NPCState(NPC _npc, FiniteStateMachine _stateMachine ){
        this.npc = _npc;
        this.stateMachine = _stateMachine;
        this.npcData = _npc.dataNPC;
        this.cooldowns = _npc.cooldowns;

        core = npc.core;
        movement = core.GetCoreComponent<Movement>();
        paintChar = core.GetCoreComponent<PaintChar>();
        enemyPos = npc.transform.position;
    }
    public override void Enter(){
        base.Enter();
        DoCheck();
        startTime = Time.time;
        npc.CurentState = this.GetType().Name;
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        DoCheck();
    }
    public virtual void DoCheck(){
        isFollowPath = npc.findPathHandle.isFollowPath;
        isGrounded = movement.isGround();
        isledge = npc.isledge();
        isWall = movement.isWall();

        XDirPos = npc.transform.position.x  - enemyPos.x;
        YDirPos = npc.transform.position.y  - enemyPos.y;     
    }

}