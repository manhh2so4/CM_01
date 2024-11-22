using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class Boss_Action : Action
    {
        protected Core core;
        protected Movement movement;
        protected CharacterStats stats;
        protected Vector3 posBoss;
        //-----------------------------------
        protected EnemyBoss_SO DataBoss;
        protected Draw_boss bossState;
        protected Transform playerCheck;
        //-----------------------------------
        protected BoxCollider2D GroundCheck;
        public override void OnAwake()
        {
            bossState = GetComponent<Draw_boss>();
            DataBoss = bossState.DataBoss_SO;
            playerCheck = bossState.playerCheck;

            core = transform.Find("Core").GetComponent<Core>();
            movement = core.GetCoreComponent<Movement>();
            stats = core.GetCoreComponent<CharacterStats>();
            posBoss = transform.position;
            if(GroundCheck == null) GroundCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        }

        public bool isGround()
        {
            return GroundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }
    }