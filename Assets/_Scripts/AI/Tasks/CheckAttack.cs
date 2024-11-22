using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckAttack : Boss_Action{
    
    public float dist;

    public override TaskStatus OnUpdate()
    {
        float XDirPos = (transform.position  - playerCheck.position).sqrMagnitude ;
        if( XDirPos < dist*dist ){                    
           return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}