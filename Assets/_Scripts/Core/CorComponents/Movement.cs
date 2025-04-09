using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Movement : CoreComponent
{
    public System.Action OnFlip;
    [SerializeField] mPhysic2D _Physic2D;
    [SerializeField] public int facingDirection { get; private set; }
    [SerializeField] public bool CanSetVelocity;
    [SerializeField] public Vector2 Velocity;
    private Vector2 workspace;
    public int mWidth = 1,mHeight = 1;

    protected override void Awake()
    {
        base.Awake();
        _Physic2D = GetComponentInParent<mPhysic2D>();
        facingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        Velocity = _Physic2D.Velocity;
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
        workspace.Set(vX, _Physic2D.Velocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float vY)
    {
        workspace.Set(_Physic2D.Velocity.x, vY);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {

        if (CanSetVelocity)
        {
            _Physic2D.Velocity = workspace;
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
        _Physic2D.Gravity = gravity;
    }
    public mPhysic2D GetPhysic2D(){
        return _Physic2D;
    }
    public void Flip()
    {
        OnFlip?.Invoke();
        facingDirection *= -1;
        _Physic2D.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public bool isGround(){
        return _Physic2D.collisionInfor.below;
    }
    public bool isWall(){
        return (_Physic2D.collisionInfor.left && facingDirection == -1)
                || (_Physic2D.collisionInfor.right && facingDirection == 1);
    }
    public bool isVXzero(){
        return Mathf.Abs(Velocity.x) < 0.01f;
    }
    public bool isVYzero(){
        return Mathf.Abs(Velocity.y) < 0.01f;
    }
    public void IsColision(bool isColision){
        _Physic2D.isColision = isColision;
    }
    public void SetDrag(float dragForce){
        _Physic2D.dragForce = dragForce;
    }
    #endregion
}
