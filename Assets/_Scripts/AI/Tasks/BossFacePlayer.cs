using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
    public class BossFacePlayer : Boss_Action
    {
        int baseScaleX;

        public override TaskStatus OnUpdate()
        {
            baseScaleX = transform.position.x > playerCheck.position.x ? -1 : 1;
            movement.CheckIfShouldFlip(baseScaleX);
            return TaskStatus.Success;
        }
    }
