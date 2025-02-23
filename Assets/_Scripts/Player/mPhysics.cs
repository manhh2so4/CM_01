using UnityEngine;

public class mPhysics : MonoBehaviour {
    private float gravity = -9.81f;
    public Vector2 velocity;
    private float gravityScale = 2.5f;
    Core core;
    CollisionSenses collisionSenses;
    public Vector2 logview;

    void Awake()
    {
        core = GetComponentInChildren<Core>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
    }
    void FixedUpdate()
    {
        //UpdateGravity();
        // if (collisionSenses.isGround && velocity.y < 0)
        // {
        //     //SetVelocityY(0);
        // }
        //transform.Translate( this.velocity * Time.fixedDeltaTime);
    }
    public void UpdateGravity(){
        this.velocity.Set(velocity.x, velocity.y + gravity * gravityScale*Time.fixedDeltaTime);
    }

    public void SetVelocityY(float vY){
        this.velocity.Set(velocity.x, vY);    
    }
    public void SetVelocityX(float vX){
        this.velocity.Set(vX, velocity.y);    
    }
    public void SetVelocity(Vector2 xy){

        this.velocity.Set(Mathf.Abs(xy.x),xy.y);    
    }
}