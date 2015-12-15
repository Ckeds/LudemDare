using UnityEngine;
using System.Collections;

public class UI_Layout : MonoBehaviour
{
	private PlayerScript Player;


	private float dash;
	private float maxDashSpool;
	private float jump;
	private float playerMaxJumpSpool;
	private int score;

	// Use this for initialization
	void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		maxDashSpool = Player.GetMaxDashSpoolTime();
		playerMaxJumpSpool = Player.GetMaxJumpSpoolTime();

	}
	
	// Update is called once per frame
	void Update ()
	{
		score = (int)((Player.playerScale - 1) * 100);
		jump = (Player.GetJumpTime() / playerMaxJumpSpool) * 100;
		dash = (Player.GetDashTime() / maxDashSpool) * 100;
	}
}
