using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent

{
    #region Check Transforms

    mPhysic2D mController2D;
    protected override void Awake() {
        base.Awake();
        mController2D = GetComponentInParent<mPhysic2D>();
    }

    #endregion
    public bool isWall{
        get =>  mController2D.collisionInfor.left ||  mController2D.collisionInfor.right;

    }
    public bool isGround{
        get =>  mController2D.collisionInfor.below;

    }
}
