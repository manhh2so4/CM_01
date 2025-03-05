using System.Drawing;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class mPhysics : Controller2D {
	[Header("mPhysic")]
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	float SetX;
	float velocityXSmoothing;
    void Update()
    {
		CalculateVelocity();

		Move(velocity * Time.deltaTime);
		if ((collisionInfor.above || collisionInfor.below)){
			velocity.y = 0;
		}
    }

    public override void SetVelocity(Vector2 xy){
        this.SetX = xy.x;
		this.velocity.y = xy.y;
    }

	void CalculateVelocity(){

		float smoothTime = (collisionInfor.below)? accelerationTimeGrounded : accelerationTimeAirborne;
		velocity.x = Mathf.SmoothDamp( velocity.x, SetX, ref velocityXSmoothing,smoothTime ,Mathf.Infinity,Time.deltaTime);
		velocity.y += gravity * Time.deltaTime;

	}
}