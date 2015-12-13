using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour
{
    private GameObject Player;
    Text text;
    int score;

    // Use this for initialization
    void Awake ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        score = (int)((Player.GetComponent<PlayerScript>().playerScale - 1)*100);
        text.text = "Score: " + score;
	}
}
