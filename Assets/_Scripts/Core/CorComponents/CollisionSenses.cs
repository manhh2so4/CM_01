using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    [SerializeField] private BoxCollider2D mGroundCheck;
    [SerializeField] private BoxCollider2D mWallCheck;
    [SerializeField] private BoxCollider2D mWallBackCheck;

    #endregion
    public bool CheckTouchingWall(){
        return  mWallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool CheckTouchingGround(){
        return  mGroundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool CheckTouchingGroundBack(){
        return  mWallBackCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
