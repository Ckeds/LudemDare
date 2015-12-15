using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour
{
	private PlayerScript Player;
	private Text text;
	private int score;

	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		score = (int)((Player.playerScale)*100);
		text.text = "Size: " + score;
	}
}
