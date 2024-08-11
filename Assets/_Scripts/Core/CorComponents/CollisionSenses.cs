using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent

{
    #region Check Transforms
    [SerializeField] private BoxCollider2D mGroundCheck;
    [SerializeField] private BoxCollider2D mWallCheck;
    [SerializeField] private BoxCollider2D mWallBackCheck;

    protected override void Awake() {
        base.Awake();
        mGroundCheck = transform.Find("Ground_check").GetComponent<BoxCollider2D>();
        mWallCheck = transform.Find("Wall_check").GetComponent<BoxCollider2D>();
        mWallBackCheck = transform.Find("Wall_check_Back").GetComponent<BoxCollider2D>();
    }

    #endregion
    public bool isWall{
        get =>  mWallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isGround{
        get =>  mGroundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isWallBack{
        get =>  mWallBackCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
