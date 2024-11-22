using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class Boss_Idle : Boss_Action{

    public override void OnStart()
    {
        base.OnStart();
        bossState.state = StateEnemy.Idle;
        movement.SetVelocityZero();
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }
    
}