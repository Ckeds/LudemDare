using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public float teleX = 0;
    public float teleY = 0;
    public float teleZ = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        var collidedObject = collision.gameObject;

        if (collidedObject.tag == "Player")
        {

        }
    }
}