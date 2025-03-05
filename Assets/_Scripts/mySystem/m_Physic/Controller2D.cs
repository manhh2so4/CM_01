using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public class Controller2D : RaycastController {


    [Header("\n--------Setting_Colision--------\n")]
    public LayerMask groundMask;
    public CollisionInfor collisionInfor;
	[SerializeField] float HitDis;

	[Header("\n--------Setting_Physic--------\n")]

	[SerializeField] protected float gravity ;
	[SerializeField] protected Vector2 velocity;
	public virtual void SetVelocity(Vector2 xy){}
	public virtual void SetGravity(float _gravity){
		this.gravity = _gravity;
	}
	public Vector2 GetVelocity(){
		return velocity;
	}

   	protected void Move(Vector2 velSet){
		UpdateRaycastOrigins ();
		collisionInfor.Reset ();
		if (velSet.x != 0) {
			collisionInfor.faceDir = (int)Mathf.Sign(velSet.x);
		}

		HorizontalCollisions (ref velSet);

		if (velSet.y != 0) {
			
			VerticalCollisions (ref velSet);
		}
		transform.Translate(velSet,Space.World);
	}

	void HorizontalCollisions(ref Vector2 vel){
		float directionX = Mathf.Sign (vel.x);
		float rayLength = Mathf.Abs (vel.x) + skinWidth;
		for (int i = 0; i < horizontalRayCount; i ++){
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft: raycastOrigins.bottomRight;
			rayOrigin.y += 1 * ( horizontalRaySpacing * i );
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, groundMask);
			if (hit) {
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
		public int faceDir;
        public void Reset(){
            above = below = left = right = false;
        }  
	}

}