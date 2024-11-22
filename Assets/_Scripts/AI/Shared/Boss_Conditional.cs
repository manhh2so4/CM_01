using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class Boss_Conditional : Conditional {
    protected BoxCollider2D GroundCheck;
    protected CapsuleCollider2D wallCheck;
    public override void OnAwake()
    {
        
        if(GroundCheck == null) GroundCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>(); 
    }
}