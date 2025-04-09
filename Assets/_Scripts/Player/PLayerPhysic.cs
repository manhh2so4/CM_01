using System.Drawing;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PLayerPhysic : mPhysic2D {
	[Header("mPhysic")]
	[SerializeField] float accelerationTimeAirborne = .2f;
	[SerializeField] float accelerationTimeGrounded = .1f;

	float SetX;
	float velocityXSmoothing;
    protected override void Update()
    {
		CalculateVelocity();
		base.Update();
    }

    protected override void SetVelocity(Vector2 xy){
        this.SetX = xy.x;
		this.velocity.y = xy.y;
    }

	protected void CalculateVelocity(){
		float smoothTime = (collisionInfor.below)? accelerationTimeGrounded : accelerationTimeAirborne;
		velocity.x = Mathf.SmoothDamp( velocity.x, SetX, ref velocityXSmoothing,smoothTime );
	}
}