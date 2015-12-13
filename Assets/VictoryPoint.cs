using UnityEngine;
using System.Collections;

public class VictoryPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Player reaches victory
    void OnTriggerEnter(Collider collision)
    {
        var collidedObject = collision.gameObject;

        if (collidedObject.tag == "Player")
        {
            Debug.Log("VICTORY!");
            Application.LoadLevel("Level_1");
        }
    }
}