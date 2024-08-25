using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logging;
public class Movement : CoreComponent
{
    [SerializeField] public Rigidbody2D mRB;
    [field: SerializeField] public int facingDirection { get; private set; }
    [field: SerializeField] public bool CanSetVelocity { get; set; }
    [field: SerializeField] public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();
        mRB = GetComponentInParent<Rigidbody2D>();
        facingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = mRB.velocity;
    }

    #region Set Functions

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;    
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
    }
    public void SetVelocity(float X,float Y)
    {
        workspace.Set(X,Y);
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            mRB.velocity = workspace;
            CurrentVelocity = workspace;
        }        
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        mRB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
