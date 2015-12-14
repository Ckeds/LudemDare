using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDash : MonoBehaviour
{
	private PlayerScript Player;
	private Text text;
	private float dash;
	private float maxDashSpool;

	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		maxDashSpool = Player.GetMaxDashSpoolTime();
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		dash = (Player.GetDashTime() / maxDashSpool) * 100;
		text.text = "Dash Charge: " + dash + "%";
	}
}
