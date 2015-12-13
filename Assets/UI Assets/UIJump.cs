using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIJump : MonoBehaviour
{
    private GameObject Player;
    Text text;
    int jump;

    // Use this for initialization
    void Awake ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        jump = (int)(Player.GetComponent<PlayerScript>().GetJumpTime() * 100);
        text.text = "Jump: " + jump;
	}
}
