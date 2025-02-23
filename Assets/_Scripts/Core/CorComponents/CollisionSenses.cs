using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent

{
    #region Check Transforms
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float groundCheckDistance;
    [SerializeField] Transform WallCheck;
    [SerializeField] float wallCheckDistance;
    private Movement movement;
    protected override void Awake() {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
    }

    #endregion
    public bool isWall{
        get =>  Physics2D.Raycast(WallCheck.position, Vector2.right * movement.facingDirection, wallCheckDistance, whatIsGround);

    }
    public bool isGround{
        get =>  Physics2D.OverlapCircle(GroundCheck.position, groundCheckDistance, whatIsGround);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Đặt màu cho đường ray
        Gizmos.DrawLine(WallCheck.position, WallCheck.position + (Vector3.right*wallCheckDistance));

        Gizmos.color = Color.blue; // Đặt màu cho đường ray
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckDistance);
    }
}
