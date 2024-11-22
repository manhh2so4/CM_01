using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class Boss_Move : Boss_Action{
    public float speedMove;
    public override void OnStart()
    {
        base.OnStart();
        bossState.speedMove = speedMove;
        bossState.state = StateEnemy.Moving;
    }
    public override TaskStatus OnUpdate()
    {
        movement?.SetVelocityX(speedMove * movement.facingDirection);
        return TaskStatus.Success;
    }
}
