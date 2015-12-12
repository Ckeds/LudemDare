using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	//Player Attributes
	Rigidbody playerRigidbody;
	float playerSpeed = .2f;
	float rotationSpeed = 0.75f;

	//Quick and dirty way to get the data we need from the input manager to move us around.
	//Check the Input Manager in Edit-> Project Settings-> Input for more.

	float current_horizontal_offset = 0f;
	float current_vertical_offset = 0f;
	bool jump_down = false;
	bool dash_down = false;

	/*Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.
	 *Base scale of blob is currently: 1
	 *As the player eats new good objects, increase scale by "some" modifier. (For now, we will increase our scale by 1/2 the collided object's scale)
	 *Bad things decrease scale; when the scale hits a certain threshold the level should be able to end.
	 */
	float myScale = 1f;

	// Use this for initialization
	void Start ()
	{
		playerRigidbody = this.GetComponent<Rigidbody>();
		transform.localScale = new Vector3(myScale , myScale , myScale);
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
		else if(current_vertical_offset < 0f) //S or Down
		{
			transform.Translate(Vector3.back * playerSpeed);
		}
		else // less than 0; W or Up
		{
			transform.Translate(Vector3.forward * playerSpeed);
		}

		//Rotation
		transform.Rotate(new Vector3(0, (current_horizontal_offset * rotationSpeed), 0), Space.World);
	}

	void OnCollisionEnter(Collision collision)
	{
		var collidedObject = collision.gameObject;
		if(collidedObject.tag == "Bad Resource")
		{
			Debug.Log("BAD THING HIT");
		}
		if (collidedObject.tag == "Good Resource")
		{
			Debug.Log("GOOD THING HIT");
			//if(this.transform.localScale >= collidedObject.transform.localScale) //collided object needs a uniform scale vector, but you can't >= a vector. Needs a small script attached with a scale attribute
			this.transform.localScale += (collidedObject.transform.localScale / 2);
			Destroy(collidedObject);
		}
	}
}
