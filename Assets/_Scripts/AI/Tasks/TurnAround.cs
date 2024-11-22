using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class TurnAround : Boss_Action
{
    public override TaskStatus OnUpdate()
    {
        movement.Flip();
        return TaskStatus.Success;
    }
}
