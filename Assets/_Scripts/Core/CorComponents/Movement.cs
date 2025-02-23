using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : CoreComponent
{
    public System.Action OnFlip;
    [SerializeField] Rigidbody2D mRB;
    [SerializeField] mPhysics mphysics;
    [field: SerializeField] public int facingDirection { get; private set; }
    [field: SerializeField] public bool CanSetVelocity;
    [field: SerializeField] public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();
        mphysics = GetComponentInParent<mPhysics>();
        facingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = mphysics.velocity;
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

    public void SetVelocityX(float vX)
    {
        workspace.Set(vX, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float vY)
    {
        workspace.Set(CurrentVelocity.x, vY);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {

            //mphysics.SetVelocity(workspace);
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
    public void AddForce(Vector2 dir){
        //mRB.AddForce(dir,ForceMode2D.Impulse);
    }
    public void Flip()
    {
        OnFlip?.Invoke();
        facingDirection *= -1;
        mphysics.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
