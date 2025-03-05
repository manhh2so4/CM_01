using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Movement : CoreComponent
{
    public System.Action OnFlip;
    [SerializeField] Controller2D mController2D;
    [SerializeField] public int facingDirection { get; private set; }
    [SerializeField] public bool CanSetVelocity;
    [SerializeField] public Vector2 CurrentVelocity;
    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();
        mController2D = GetComponentInParent<Controller2D>();
        facingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = mController2D.GetVelocity();
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
        //Debug.Log("1");
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
        //Debug.Log("2");
    }
    public void SetVelocity(float X,float Y)
    {
        workspace.Set(X,Y);
        SetFinalVelocity();
        //Debug.Log("3");
    }

    public void SetVelocityX(float vX)
    {
        workspace.Set(vX, mController2D.GetVelocity().y);
        SetFinalVelocity();
        //Debug.Log("4");
    }

    public void SetVelocityY(float vY)
    {
        workspace.Set(mController2D.GetVelocity().x, vY);
        SetFinalVelocity();
        //Debug.Log("5");
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            mController2D.SetVelocity(workspace);
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
    public void SetGravity(float gravity){
        mController2D.SetGravity(gravity);
    }
    public void Flip()
    {
        OnFlip?.Invoke();
        facingDirection *= -1;
        mController2D.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
