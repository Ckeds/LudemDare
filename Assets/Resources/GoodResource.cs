using UnityEngine;
using System.Collections;

public class GoodResource : MonoBehaviour
{
	private ResourceSpawner gameSpawner;

	// Use this for initialization
	void Start ()
	{
		gameSpawner = GameObject.FindGameObjectWithTag("ResourceSpawner").GetComponent<ResourceSpawner>();

		//gameSpawner.goodRes.Add(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
