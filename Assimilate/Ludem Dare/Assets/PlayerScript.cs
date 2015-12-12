using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	Rigidbody playerRigidbody;
	float playerSpeed = 5f;

	//Quick and dirty way to get the data we need from the input manager to move us around.
	//Check the Input Manager in Edit-> Project Settings-> Input for more.

	float current_horizontal_offset;
	float current_vertical_offset;
	bool fire1_down;
	bool fire2_down;

	//Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.

	// Use this for initialization
	void Start ()
	{
		playerRigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		current_horizontal_offset = Input.GetAxis("Horizontal");
		current_vertical_offset = Input.GetAxis("Vertical");
		fire1_down = Input.GetButton("Fire1");
		fire2_down = Input.GetButton("Fire2");

		if (current_horizontal_offset == 0.0f)
		{
		    //do nothing	
		}
		else if(current_horizontal_offset < 0)
		{
			playerRigidbody.AddRelativeForce(Vector3.left * playerSpeed);
		}
		else // greater than 0
		{
			playerRigidbody.AddRelativeForce(Vector3.right * playerSpeed);
		}
	}
}
