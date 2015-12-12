using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Quick and dirty way to get the data we need from the input manager to move us around.
		//Check the Input Manager in Edit-> Project Settings-> Input for more.

		//var current_horizontal_offset = Input.GetAxis("Horizontal");
		//var current_vertical_offset = Input.GetAxis("Vertical");
		//var fire1_down = Input.GetButton("Fire1");
		//var fire2_down = Input.GetButton("Fire2");

		//Resources are tagged as good and bad (literally, i.e. "Bad Resource") for the purpose of collision detection for the short-term.
	}
}
