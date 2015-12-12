using UnityEngine;
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

	public int numGood = 1000;
	public int numBad = 1000;

	List<GameObject> goodRes;
	List<GameObject> badRes;

	// Use this for initialization
	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		goodRes = new List<GameObject>();
		badRes = new List<GameObject>();
		float randX;
		float randY;
		float randZ;
		float randScale;
		for (int i = 0; i < numGood; i++)
		{
			randX = Random.Range(-stageLength / 2, stageLength/ 2);
			randY = Random.Range(1, stageHeight);
			randZ = Random.Range(-stageWidth / 2, stageWidth / 2);

			randScale = Random.Range(Player.GetComponent<PlayerScript>().playerScale * .75f, Player.GetComponent<PlayerScript>().playerScale * 1.25f);

			GameObject obj = (GameObject)Instantiate(goodResource);
			obj.transform.position = new Vector3(randX, randY, randZ);
			obj.transform.localScale = new Vector3 (randScale, randScale,randScale);
			obj.SetActive(true);
			goodRes.Add(obj);
		}
		for (int i = 0; i < numBad; i++)
		{
			randX = Random.Range(-stageLength / 2, stageLength / 2);
			randY = Random.Range(1, stageHeight);
			randZ = Random.Range(-stageWidth / 2, stageWidth / 2);

			randScale = Random.Range(Player.GetComponent<PlayerScript>().playerScale * .75f, Player.GetComponent<PlayerScript>().playerScale * 1.25f);

			GameObject obj = (GameObject)Instantiate(badResource);
			obj.transform.position = new Vector3(randX, randY, randZ);
			obj.transform.localScale = new Vector3(randScale, randScale, randScale);
			obj.SetActive(true);
			badRes.Add(obj);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void moveObject(GameObject o)
	{

	}
}
