using UnityEngine;
using System.Collections;

public class VictoryPoint : MonoBehaviour {
    // Class globals
    private Texture2D victoryNotification;
    private bool victory = false;

    // Use this for initialization
    void Start () {
        victoryNotification = (Texture2D)Resources.Load("victory");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // GUI elements
    void OnGUI()
    {
        if(victory)
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - (victoryNotification.width / 2), (Screen.height / 2) - (victoryNotification.height / 2), 320, 240), victoryNotification);
        }
    }

    // Player reaches victory
    void OnTriggerEnter(Collider collision)
    {
        var collidedObject = collision.gameObject;

        if (collidedObject.tag == "Player")
        {
            Debug.Log("VICTORY!");
            victory = true;
            Application.LoadLevel("Level_1");
        }
    }
}