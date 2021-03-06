﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceSpawner : MonoBehaviour
{

	public GameObject goodResource;
	public GameObject badResource;
	public GameObject Player;

	public int stageWidth = 500;
	public int stageLength = 500;
	public int stageHeight = 50;

	public int playerSpawnDist = 50;

	public int numGood = 0;
	public int numBad = 0;

	public List<GameObject> goodRes;
	public List<GameObject> badRes;

    public bool recyleStuff = false;

	// Use this for initialization
	void Start ()
	{
		Physics.gravity = new Vector3(0, -20, 0);
		Player = GameObject.FindGameObjectWithTag("Player");
		goodRes = new List<GameObject>();
		badRes = new List<GameObject>();


		//Add Existing Resource Objects to the Spawner Lists (if they exist in a level)
		GameObject[] temporaryGameObjectArray;
		temporaryGameObjectArray = GameObject.FindGameObjectsWithTag("Good Resource");
		if(temporaryGameObjectArray.Length > 0)
		{
			goodRes.AddRange(temporaryGameObjectArray);
			temporaryGameObjectArray = new GameObject[0];
		}
		temporaryGameObjectArray = GameObject.FindGameObjectsWithTag("Bad Resource");
		if(temporaryGameObjectArray.Length > 0)
		{
			badRes.AddRange(temporaryGameObjectArray);
		}
		
		//Procedural Resource Generation
		float randX;
		float randY;
		float randZ;
		float randScale;
		for (int i = 0; i < numGood; i++)
		{
			randX = Random.Range(-stageLength / 2, stageLength/ 2);
			randY = Random.Range(1, stageHeight);
			randZ = Random.Range(-stageWidth / 2, stageWidth / 2);

			randScale = Random.Range(Player.GetComponent<PlayerScript>().playerScale * .5f, Player.GetComponent<PlayerScript>().playerScale * 1.0f);

			GameObject obj = (GameObject)Instantiate(goodResource);
			obj.transform.position = new Vector3(randX, randY, randZ);
			obj.transform.localScale = new Vector3 (randScale, randScale,randScale);
			obj.GetComponent<Rigidbody>().mass = obj.transform.localScale.x;

			if (obj.GetComponent<Rigidbody>().mass >= 10)
				obj.GetComponent<Rigidbody>().mass = 10;

			obj.SetActive(true);
			goodRes.Add(obj);
		}
		for (int i = 0; i < numBad; i++)
		{
			randX = Random.Range(-stageLength / 2, stageLength / 2);
			randY = Random.Range(1, stageHeight);
			randZ = Random.Range(-stageWidth / 2, stageWidth / 2);

			

			randScale = Random.Range(Player.GetComponent<PlayerScript>().playerScale * .75f, Player.GetComponent<PlayerScript>().playerScale * 1.0f);

			GameObject obj = (GameObject)Instantiate(badResource);
			obj.transform.position = new Vector3(randX, randY, randZ);
			obj.transform.localScale = new Vector3(randScale, randScale, randScale);
			obj.SetActive(true);
			badRes.Add(obj);
		}
	}

	public void recycleObject(GameObject o)
	{
		float randX;
		float randY;
		float randZ;
		float randScale;

		//randX = Random.Range(-stageLength / 2, stageLength / 2);
		//randY = Random.Range(1, stageHeight);
		//randZ = Random.Range(-stageWidth / 2, stageWidth / 2);

		randX = Random.Range(Player.transform.position.x - playerSpawnDist, Player.transform.position.x + playerSpawnDist);
		randY = Random.Range(Player.transform.position.y+5, Player.transform.position.y + 25);
		randZ = Random.Range(Player.transform.position.z - playerSpawnDist, Player.transform.position.z + playerSpawnDist);

		if (randX >= stageWidth/2)
			randX = (stageWidth/2) - 1;
		else if (randX <= -stageWidth/2)
			randX = (-stageWidth / 2) +1;

		if (randX >= stageLength / 2)
			randX = (stageLength / 2) - 1;
		else if (randX <= -stageLength / 2)
			randX = (-stageLength / 2) + 1;

		randScale = Random.Range(Player.GetComponent<PlayerScript>().playerScale * .5f, Player.GetComponent<PlayerScript>().playerScale * 1.25f);
		
		o.transform.position = new Vector3(randX, randY, randZ);
		o.transform.localScale = new Vector3(randScale, randScale, randScale);
		o.GetComponent<Rigidbody>().mass = o.transform.localScale.x;

		if (o.GetComponent<Rigidbody>().mass >= 10)
			o.GetComponent<Rigidbody>().mass = 10;
        if(recyleStuff)
		    o.SetActive(true);
	}
	void resetGame()
	{
		//Player.GetComponent<PlayerScript>.resetPlayer();
		foreach (GameObject o in goodRes)
		{
			recycleObject(o);
		}
		foreach (GameObject o in badRes)
		{
			recycleObject(o);
		}
	}
}
