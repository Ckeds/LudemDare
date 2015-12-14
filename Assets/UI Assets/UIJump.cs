using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIJump : MonoBehaviour
{
	private PlayerScript Player;
	private Text text;
	private float jump;
	private float playerMaxJumpSpool;

	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		playerMaxJumpSpool = Player.GetMaxJumpSpoolTime();
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		jump = (Player.GetJumpTime() / playerMaxJumpSpool) * 100;
		text.text = "Jump Charge: " + jump + "%";
	}
}
