using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float wallSlideSpeedMax = 3;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpLeap;
	public Vector2 wallJumpDrop;

	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	float minJumpVelocity = 2;
	float maxJumpVelocity = 10;
	float moveSpeed = 6;
	float gravity = -20;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	void Start () {
		controller = GetComponent<Controller2D> ();
	}

	void Update(){

		bool wallSliding = false;
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if(velocity.y < -wallSlideSpeedMax){
				velocity.y = -wallSlideSpeedMax;
			}
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space)) {
			if(wallSliding){
				if(wallDirX == input.x){
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}else if(input.x == 0){
					velocity.x = -wallDirX * wallJumpDrop.x;
					velocity.y = wallJumpDrop.y;
				}else{
					velocity.x = -wallDirX * wallJumpLeap.x;
					velocity.y = wallJumpLeap.y;
				}
			}
			if(controller.collisions.below){
				velocity.y = maxJumpVelocity;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			if(velocity.y > minJumpVelocity){
				velocity.y = minJumpVelocity;
			}
		}


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
