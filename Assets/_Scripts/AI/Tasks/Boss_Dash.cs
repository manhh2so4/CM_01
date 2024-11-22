using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
    public class Boss_Dash : Boss_Action
    {
        public float horizontalForce = 5.0f;
        public float buildupTime = 0.5f;
        private bool hasDashed;
        public Ease ease;
        private Tween buildupTween;
        public override void OnStart()
        {
            hasDashed = false;
            bossState.state = StateEnemy.Hold;   
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartDash, false);
        }
        void StartDash(){
            bossState.state = StateEnemy.Dash; 
            int direction = playerCheck.position.x < transform.position.x ? -1 : 1;
            movement.CheckIfShouldFlip(direction);
            movement.SetVelocityZero();
            transform.DOMove( new Vector2 (transform.position.x + horizontalForce*direction,transform.position.y), .4f)
            .SetEase(ease)  
            .OnComplete(()=>{
				hasDashed = true;
            });
        }
        public override TaskStatus OnUpdate()
        {
            return hasDashed ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween.Kill();
            hasDashed = false;
        }
}
