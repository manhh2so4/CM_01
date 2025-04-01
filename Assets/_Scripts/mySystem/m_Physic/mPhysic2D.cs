using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class mPhysic2D : RaycastPhysic2D {


    [Header("\n--------Setting_Colision--------\n")]
    public LayerMask groundMask;
    public CollisionInfor collisionInfor;

	[Header("\n--------Setting_Physic--------\n")]
	public bool isDrag = false;
	public bool isColision = true;
	public float dragForce = 2f;
	[SerializeField] protected float gravity = -9.8f;
	[SerializeField] protected Vector2 velocity;
	public Vector2 Velocity { get {return velocity; } set { SetVelocity(value); }}
	public float Gravity{ get {return gravity;} set{ gravity = value;} }
	[SerializeField] Vector2 velocitySet;
	protected virtual void SetVelocity( Vector2 _xy){
		this.velocity = _xy;
	}
	protected virtual void Update(){
		if (isDrag) ApplyDrag();
		
		velocity.y += Gravity * Time.deltaTime;

		Move(velocity * Time.deltaTime);

		if( collisionInfor.right || collisionInfor.left ){
			velocity.x = 0;
		}
		if (collisionInfor.above || collisionInfor.below){
			velocity.y = 0;
		}

	}

   	protected void Move( Vector2 velSet){

		UpdateRaycastOrigins ();
		collisionInfor.Reset ();

		if (isColision) {
			HorizontalCollisions (ref velSet);

			if (velSet.y != 0) {
				
				VerticalCollisions (ref velSet);
			}
		}
		velocitySet = velSet;
		
		transform.Translate(velSet,Space.World);
	}
	void ApplyDrag()
	{
		if (velocity.x != 0)
		{
			float dragX = Mathf.Sign(velocity.x) * dragForce;
			velocity.x = Mathf.MoveTowards(velocity.x, 0, Mathf.Abs(dragX) * Time.deltaTime);
		}

		// if (velocity.y < 0)
		// {
		// 	float dragY = dragForce * 0.5f;
		// 	velocity.y = Mathf.MoveTowards(velocity.y, gravity, dragY * Time.deltaTime);
		// }
	}

	void HorizontalCollisions(ref Vector2 vel){
		float directionX = Mathf.Sign (vel.x);
		float rayLength = Mathf.Abs (vel.x) + skinWidth;
		for (int i = 0; i < horizontalRayCount; i ++){
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft: raycastOrigins.bottomRight;
			rayOrigin.y += 1 * ( horizontalRaySpacing * i );
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, groundMask);
			if (hit){

				if(hit.transform.CompareTag("Platform")){
					return;				
				}
				vel.x = (hit.distance - skinWidth/2) * directionX;
				rayLength = hit.distance;
				collisionInfor.left = directionX == -1;
				collisionInfor.right = directionX == 1;
				return;

			}
		}
	}
	
	void VerticalCollisions(ref Vector2 vel){

		float directionY = Mathf.Sign (vel.y);
		float rayLength = Mathf.Abs(vel.y) + skinWidth/2;

		for(int i = 0; i < verticalRayCount; i ++){
			Vector2 rayOrigin = (directionY == -1)? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * ( verticalRaySpacing * i + vel.x );

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength,  groundMask );
			
			if(hit){
				

				if(hit.transform.CompareTag("Platform")){

					if( directionY == 1 || hit.distance == 0 ) {
						continue;
					}					
				}
				
				vel.y = (hit.distance - skinWidth/2) * directionY;

				rayLength = hit.distance;

				collisionInfor.below = directionY == -1;
				collisionInfor.above = directionY == 1 ;
							
				
			}
		}
	}

    [Serializable]
    public struct CollisionInfor{
		public bool above, below, left, right;
        public void Reset(){
            above = below = left = right = false;
        }  
	}

}