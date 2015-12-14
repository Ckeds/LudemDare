using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public float teleX = 0;
    public float teleY = 0;
    public float teleZ = 0;

    private GameObject Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            Player.transform.position = new Vector3(teleX, teleY, teleZ);
        }
    }
}