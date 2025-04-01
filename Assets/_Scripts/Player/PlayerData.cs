using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementSpeed = 10f;

    [Header("Jump State")]
    [SerializeField] float maxJumpHeight = 4;
	[SerializeField] float minJumpHeight = 1;
    [SerializeField] float timeToJumpApex = .4f;
    public float MaxJumpVelocity = 15f;
    public float MinJumpVelocity = 8f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;
    public float GetGravity(){

		float gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		MaxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		MinJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
        return gravity;
        
    }
        
}
