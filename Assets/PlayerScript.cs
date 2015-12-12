using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	//Player Attributes
	private Rigidbody playerRigidbody;
	private float playerSpeed = 0.2f;
	private float rotationSpeed = 0.75f;
	private float jumpForceMultiplier = 10.0f;
	private bool jumping = false;
	private float dashForceMultiplier = 10.0f;
	private float dashTimer = 0.0f;
	private float dashCooldownLength = 3.0f;
	private bool dashing = false;

	/*Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.
	 *Base scale of blob is currently: 1
	 *As the player eats new good objects, increase scale by "some" modifier. (For now, we will increase our scale by 1/2 the collided object's scale)
	 *Bad things decrease scale; when the scale hits a certain threshold the level should be able to end.
	 */
	public float playerScale = 1.0f;

	//Quick and dirty way to get the data we need from the input manager to move us around.
	//Check the Input Manager in Edit-> Project Settings-> Input for more.
	private float current_horizontal_offset = 0.0f;
	private float current_vertical_offset = 0.0f;
	private bool jump_down = false;
	private bool dash_down = false;

	// Use this for initialization
	void Start ()
	{
		playerRigidbody = this.GetComponent<Rigidbody>();
		transform.localScale = new Vector3(playerScale , playerScale , playerScale);
	}

	// Update is called once per frame; FixedUpdate per physics step
	void FixedUpdate ()
	{
		current_horizontal_offset = Input.GetAxis("Horizontal");
		current_vertical_offset = Input.GetAxis("Vertical");
		jump_down = Input.GetButton("Jump");
		dash_down = Input.GetButton("Dash");

		//Movement - in a not so great if block
		if (current_vertical_offset == 0.0f)
		{
			//do nothing
		}
		else if(current_vertical_offset < 0f) //S or Down when not using joystick
		{
			playerRigidbody.AddForce(-playerRigidbody.transform.forward * playerSpeed, ForceMode.Impulse);
		}
		else // less than 0; W or Up when not using joystick
		{
			playerRigidbody.AddForce(playerRigidbody.transform.forward * playerSpeed, ForceMode.Impulse);
		}

		//Rotation
		transform.Rotate(new Vector3(0, (current_horizontal_offset * rotationSpeed), 0), Space.World);

		//Player Action Buttons
		if(jump_down && !jumping)
		{
			Debug.Log("Jumping");
			playerRigidbody.AddForce(playerRigidbody.transform.up * jumpForceMultiplier, ForceMode.Impulse);
			jumping = true;
		}

		if(dash_down && !dashing)
		{
			Debug.Log("Dashing");
			playerRigidbody.AddForce(playerRigidbody.transform.forward * dashForceMultiplier, ForceMode.Impulse);
			dashing = true;
		}

		if(dashing)
		{
			dashTimer += Time.deltaTime;
			if(dashTimer >= dashCooldownLength)
			{
				dashing = false;
				dashTimer = 0.0f;
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		var collidedObject = collision.gameObject;
		if (collidedObject.tag == "Bad Resource")
		{
			Debug.Log("BAD THING HIT");
			/* Collided object needs a uniform scale vector, but you can't >= a vector.
			 * Currently the resource recycling manager will set uniform scales, so we can just check against a single vector value.
			 * Otherwise resources need a small script attached with a scale attribute.
			 */
			if (playerScale >= collidedObject.transform.localScale.x)
			{
				this.transform.localScale -= (collidedObject.transform.localScale / 2);
				//Destroy to be replaced with a recycle command via the Resource Manager
				Destroy(collidedObject);
			}
		}
		if (collidedObject.tag == "Good Resource")
		{
			Debug.Log("GOOD THING HIT");
			/* Collided object needs a uniform scale vector, but you can't >= a vector.
			 * Currently the resource recycling manager will set uniform scales, so we can just check against a single vector value.
			 * Otherwise resources need a small script attached with a scale attribute.
			 */
			if (playerScale >= collidedObject.transform.localScale.x)
			{
				this.transform.localScale += (collidedObject.transform.localScale / 2);
				//Destroy to be replaced with a recycle command via the Resource Manager
				Destroy(collidedObject);
			}
		}

		if (jumping && collidedObject.tag == "Terrain")
		{
			jumping = false;
		}
	}
}
