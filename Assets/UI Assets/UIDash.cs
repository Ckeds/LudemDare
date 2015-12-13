using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDash : MonoBehaviour
{
    private GameObject Player;
    Text text;
    int dash;

    // Use this for initialization
    void Awake ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        dash = (int)(Player.GetComponent<PlayerScript>().GetDashTime() * 100);
        text.text = "Dash: " + dash;
	}
}
