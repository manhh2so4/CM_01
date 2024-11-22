using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
    public class Boss_Jump : Boss_Action
    {
        public float jumpForce = 10.0f;
        public float buildupTime = .5f;
        bool hasLanded;
        bool isjump;
        private Tween buildupTween;
        public override void OnStart()
        {
            hasLanded = false;
            isjump = false;
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
        }

        private void StartJump()
        {
            bossState.state = StateEnemy.Jump;  
            float horizontalForce = playerCheck.position.x - transform.position.x;
            horizontalForce = Mathf.Clamp(horizontalForce, -6,6);
            int direction = horizontalForce > 0 ? 1 : -1;

            movement.CheckIfShouldFlip(direction);
            movement.AddForce(new Vector2(horizontalForce*0.85f, jumpForce));
            
        }

        public override TaskStatus OnUpdate()
        {
            core.LogicUpdate();
            if(movement.CurrentVelocity.y < -0.01){
                bossState.state = StateEnemy.Fall;
                isjump = true;
                int direction = playerCheck.position.x < transform.position.x ? -1 : 1;
                movement.CheckIfShouldFlip(direction);
            }
            if(isGround() && isjump ){
                hasLanded =true;
                movement.SetVelocityZero();
            }
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            hasLanded = false;
        }
    }
