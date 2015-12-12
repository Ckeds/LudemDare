using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	//Player Attributes
	public Camera followCamera;
	private Rigidbody playerRigidbody;
	private float playerSpeedFloor = 0.35f;
	private float playerSpeed;
	private float rotationSpeed = 1.0f;
	public float playerScale = 1.0f;
	/*Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.
	 *As the player eats new good objects, increase scale by "some" modifier. (For now, we will increase our scale by 1/2 the collided object's scale).
	 *Bad things decrease scale; when the scale hits a certain threshold the level should be able to end.
	 */

	//Jump Action Variables
	private bool jumping = false;
	private float jumpForceFloor = 10.0f;
	private float jumpForceMultiplier;

	//Dash Action Variables
	private bool dashing = false;
	private float dashForceFloor = 20.0f;
	private float dashForceMultiplier;
	private float dashTimer = 0.0f;
	private float dashCooldownLength = 3.0f;
	
	//Player Input Variables; Check the Input Manager found in Edit-> Project Settings-> Input for more.
	private float current_horizontal_offset = 0.0f;
	private float current_vertical_offset = 0.0f;
	private bool jump_down = false;
	private bool dash_down = false;

	// Use this for initialization
	void Start ()
	{
		//Snag a reference to the player's Rigidbody
		playerRigidbody = GetComponent<Rigidbody>();

		//Set Default Player Scale
		transform.localScale = new Vector3(playerScale , playerScale , playerScale);

		//set up initial follow camera position
		followCamera = Camera.main;
	}

	// Update is called once per frame; FixedUpdate per physics step
	void FixedUpdate ()
	{
		//Update Input variables
		current_horizontal_offset = Input.GetAxis("Horizontal");
		current_vertical_offset = Input.GetAxis("Vertical");
		jump_down = Input.GetButton("Jump");
		dash_down = Input.GetButton("Dash");

		//Force Multiplier Calculation
		playerSpeed = (playerSpeedFloor * playerScale) / 5;
		if (playerSpeed < playerSpeedFloor) playerSpeed = playerSpeedFloor;

		jumpForceMultiplier = (jumpForceFloor * playerScale) / 5;
		if (jumpForceMultiplier < jumpForceFloor) jumpForceMultiplier = jumpForceFloor;

		dashForceMultiplier = (dashForceFloor * playerScale) / 5;
		if (dashForceMultiplier < dashForceFloor) dashForceMultiplier = dashForceFloor;

		//Player Movement
		if(current_vertical_offset < 0f) //S or Down when not using a joystick
		{
			playerRigidbody.AddForce(-playerRigidbody.transform.forward * playerSpeed, ForceMode.Impulse);
		}
		else if (current_vertical_offset > 0f) // W or Up when not using a joystick
		{
			playerRigidbody.AddForce(playerRigidbody.transform.forward * playerSpeed, ForceMode.Impulse);
		}

		//Player Rotation
		transform.Rotate(new Vector3(0, (current_horizontal_offset * rotationSpeed), 0), Space.World);

		//Camera Rotation
		followCamera.transform.Rotate(new Vector3(0, (current_horizontal_offset * rotationSpeed), 0), Space.World);

		//Player Action Buttons
		if(jump_down && !jumping)
		{
			jumping = true;
			playerRigidbody.AddForce(playerRigidbody.transform.up * jumpForceMultiplier, ForceMode.Impulse);
		}

		if(dash_down && !dashing)
		{
			dashing = true;
			playerRigidbody.AddForce(playerRigidbody.transform.forward * dashForceMultiplier, ForceMode.Impulse);
		}

		//Update Dash state/timer
		if(dashing)
		{
			dashTimer += Time.deltaTime;
			if(dashTimer >= dashCooldownLength)
			{
				dashing = false;
				dashTimer = 0.0f;
			}
		}

		//Camera update code; the 2 and 5 are unfortunately magic numbers based on where we had previously positioned the camera manually to get the view and angle we wanted.
		followCamera.transform.position = Vector3.Lerp(followCamera.transform.position,
														new Vector3(transform.position.x + (transform.forward.x * -5f * playerScale),
																	transform.position.y + (2f * playerScale),
																	transform.position.z + (transform.forward.z * -5f * playerScale)
																	),
														playerSpeed);
	}

	void OnCollisionEnter(Collision collision)
	{
		var collidedObject = collision.gameObject;

		if (jumping && collidedObject.tag == "Terrain")
		{
			jumping = false;
		}
	}
	void OnTriggerEnter(Collider collision)
	{
		var collidedObject = collision.gameObject;

		if (collidedObject.tag == "Bad Resource")
		{
			/* Collided object needs a uniform scale vector, but you can't >= a vector.
			* Currently the resource recycling manager will set uniform scales, so we can just check against a single vector value.
			* Otherwise resources need a small script attached with a scale attribute.
			*/
			if (playerScale >= collidedObject.transform.localScale.x)
			{
				playerRigidbody.isKinematic = true;
				//Resize the player
				transform.localScale -= (collidedObject.transform.localScale / 5);
				//Update internal scale variable for win calculation and such
				playerScale = transform.localScale.x;
				//Destroy to be replaced with a recycle command via the Resource Manager
				Destroy(collidedObject);
				playerRigidbody.isKinematic = false;
			}
		}

		if (collidedObject.tag == "Good Resource")
		{
		    /* Collided object needs a uniform scale vector, but you can't >= a vector.
			*  Currently the resource recycling manager will set uniform scales, so we can just check against a single vector value.
			*  Otherwise resources need a small script attached with a scale attribute.
			*/
			if (playerScale >= collidedObject.transform.localScale.x)
			{
				//Resize the player
				transform.localScale += (collidedObject.transform.localScale / 5);
				//Update internal scale variable for win calculation and such
				playerScale = transform.localScale.x;
				//Destroy to be replaced with a recycle command via the Resource Manager
				Destroy(collidedObject);
			}
		}
	   
	}
}
