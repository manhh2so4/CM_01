using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
    public class Boss_Attack : Boss_Action
    {
        public bool attackDone;
        public StateEnemy stateEnemy;
        public override void OnStart()
        {
            attackDone = false;
            bossState.state = stateEnemy;
            movement?.SetVelocityZero();
            bossState.OnAttackDone += DoneAttack;
        }
        void DoneAttack(){
            attackDone = true;
            bossState.state = StateEnemy.Idle;
        }
        public override TaskStatus OnUpdate()
        {
            return attackDone ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            bossState.OnAttackDone -= DoneAttack;
            attackDone = false;
        }
    }
