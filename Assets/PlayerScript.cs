using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	//Player Attributes
	public float playerSpeedFloor = 0.45f;
	private float playerSpeed;
	public float rotationSpeed = 1.5f;
	public float playerScale = 1.0f;
	public int playerScore = 0;
	/*Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.
	 *As the player eats new good objects, increase scale by "some" modifier. (For now, we will increase our scale by 1/2 the collided object's scale).
	 *Bad things decrease scale; when the scale hits a certain threshold the level should be able to end.
	 */

	//Jump Action Variables
	private bool jumping = false;
	public float jumpForceFloor = 1.0f;
	private float jumpForceMultiplier; 
	private float jumpSpoolTimer = 0.0f;
	public float GetJumpTime() { return jumpSpoolTimer; } //needed for UI
	private float maxJumpSpoolTime = 1.5f;
	public float GetMaxJumpSpoolTime() { return maxJumpSpoolTime; } //needed for UI

	//Dash Action Variables
	private bool dashing = false;
	private float dashSpoolTimer = 0.0f;
	public float GetDashTime() { return dashSpoolTimer; } //needed for UI
	private float maxDashSpoolTime = 1.0f;
	public float GetMaxDashSpoolTime() { return maxDashSpoolTime; } //needed for UI
	public float dashForceFloor = 1.0f;
	private float dashForceMultiplier; 
	private float dashCooldownTimer = 0.0f;
	private float dashCooldownLength = 2.0f;

	//Player Input Variables; Check the Input Manager found in Edit-> Project Settings-> Input for more.
	private float current_horizontal_offset = 0.0f;
	private float current_vertical_offset = 0.0f;
	private bool jump_down = false;
	private bool dash_down = false;

	//Player References
	private Camera followCamera;
	private Rigidbody playerRigidbody;
	private ResourceSpawner gameSpawner;

	// Use this for initialization
	void Start ()
	{
		//Snag a reference to the player's Rigidbody
		playerRigidbody = GetComponent<Rigidbody>();

		//set up initial follow camera position
		followCamera = Camera.main;

		//ResourceSpawner Reference
		gameSpawner = GameObject.FindGameObjectWithTag("ResourceSpawner").GetComponent<ResourceSpawner>();

		//Set Default Player Scale - We are assuming that the player prefab is uniformly scaled on Start()
		playerScale = transform.localScale.x;
	}

	// Update is called once per frame; FixedUpdate per physics step
	void FixedUpdate ()
	{
		playerScore = (int)(playerScale - 1) * 100;

		//Time Variable for use with incrementing timers
		var deltaTime = Time.deltaTime;

		//Update Input variables
		current_horizontal_offset = Input.GetAxis("Horizontal");
		current_vertical_offset = Input.GetAxis("Vertical");
		jump_down = Input.GetButton("Jump");
		dash_down = Input.GetButton("Dash");

		//Player Mass Calculation
		playerRigidbody.mass = playerScale * 1.25f;
		if (playerRigidbody.mass <= 1.0f) playerRigidbody.mass = 1.0f;
		else if (playerRigidbody.mass >= 10.0f) playerRigidbody.mass = 10.0f;

		//Force Multiplier Calculation
		playerSpeed = (playerSpeedFloor * playerScale * playerRigidbody.mass) /(playerRigidbody.mass * 0.5f); //change to a higher number for slower speed
		if (playerSpeed < playerSpeedFloor) playerSpeed = playerSpeedFloor;

		jumpForceMultiplier = (jumpForceFloor * playerScale * playerRigidbody.mass) / (playerRigidbody.mass * 0.85f);
		if (jumpForceMultiplier < jumpForceFloor) jumpForceMultiplier = jumpForceFloor;

		dashForceMultiplier = (dashForceFloor * playerScale * playerRigidbody.mass) / (playerRigidbody.mass * 0.85f);
		if (dashForceMultiplier < dashForceFloor) dashForceMultiplier = dashForceFloor;

		//Player Movement
		if (transform.position.y <= -25) transform.position = new Vector3(0, 1, 0); //this is for resetting the player and if we need to do that, we have bigger design problems

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

		//Jump Logic
		if(jump_down && !jumping)
		{
			if (jumpSpoolTimer < maxJumpSpoolTime)
			{
				jumpSpoolTimer += deltaTime;
			}
		}
		else if (!jump_down && jumpSpoolTimer > 0.0f)
		{
			jumping = true;
			playerRigidbody.AddForce(playerRigidbody.transform.up * jumpForceMultiplier * (1 + jumpSpoolTimer), ForceMode.Impulse);
			jumpSpoolTimer = 0.0f;
		}

		//Dash Logic
		if(dash_down && !dashing)
		{
			if (dashSpoolTimer < maxDashSpoolTime)
			{
				dashSpoolTimer += deltaTime;
			}
		}
		else if (!dash_down && dashSpoolTimer > 0.0f)
		{
			dashing = true;
			playerRigidbody.AddForce(playerRigidbody.transform.forward * dashForceMultiplier * (1 + dashSpoolTimer), ForceMode.Impulse);
			dashSpoolTimer = 0.0f;
		}

		//Update Dash state/timer
		if(dashing)
		{
			dashCooldownTimer += deltaTime;
			if(dashCooldownTimer >= dashCooldownLength)
			{
				dashing = false;
				dashCooldownTimer = 0.0f;
			}
		}

		//Camera update code; the 2 and 5 are unfortunately magic numbers based on where we had previously positioned the camera manually to get the view and angle we wanted.
		followCamera.transform.position = Vector3.Lerp(followCamera.transform.position,
														new Vector3(transform.position.x + (transform.forward.x * -5f * playerScale),
																	transform.position.y + (2f * playerScale),
																	transform.position.z + (transform.forward.z * -5f * playerScale)
																	),
														playerSpeed);
		//Camera nearPlane update
		//followCamera.nearClipPlane = Vector3.Distance(transform.position, followCamera.transform.position) * 0.5f;
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
				//playerRigidbody.isKinematic = true;
				//Resize the player
				transform.localScale -= (collidedObject.transform.localScale * .5f);
				//Update internal scale variable for win calculation and such
				playerScale = transform.localScale.x;
				//Recycle the object
				collidedObject.SetActive(false);
				gameSpawner.recycleObject(collidedObject);
				//playerRigidbody.isKinematic = false;

				
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
				//playerRigidbody.isKinematic = true;
				//Resize the player
				transform.localScale += (collidedObject.transform.localScale * .1f);
				//Update internal scale variable for win calculation and such
				playerScale = transform.localScale.x;
				//Recycle the object
				collidedObject.SetActive(false);
				gameSpawner.recycleObject(collidedObject);
				//playerRigidbody.isKinematic = false;
			}
		}
	}
}
