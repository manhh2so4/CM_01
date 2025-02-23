using UnityEngine;

public class mPhysics : MonoBehaviour {
    public float jumpForce=20;
    private float gravity = -9.81f;
    [SerializeField] Vector2 velocity;
    private float gravityScale = 1;
    Core core;
    CollisionSenses collisionSenses;
    void Awake()
    {
        core = GetComponentInChildren<Core>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
    }
    void Update()
    {
        
        UpdateGravity();
        if (collisionSenses.isGround && velocity.y < 0)
        {
            SetVelocityY(0);
        }

        transform.Translate(velocity * Time.deltaTime);
    }
    public void UpdateGravity(){
        this.velocity.Set(velocity.x, velocity.y + gravity * gravityScale *Time.deltaTime);
    }

    public void SetVelocityY(float vY){
        this.velocity.Set(velocity.x, vY);    
    }
    public void SetVelocityX(float vX){
        this.velocity.Set(vX, velocity.y);    
    }
}