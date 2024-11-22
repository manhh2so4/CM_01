using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckRangeMove : Boss_Action{
    
    public float rangeMove;
    public override TaskStatus OnUpdate()
    {
        float XDirPos = transform.position.x  - posBoss.x;
        if( Mathf.Abs(XDirPos) > rangeMove ){                    
                    movement.CheckIfShouldFlip( XDirPos > 0 ? -1 : 1 );
        }
        return  TaskStatus.Success;
    }
}