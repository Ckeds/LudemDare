using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceSpawner : MonoBehaviour
{

    public GameObject goodResource;
    public GameObject badResource;

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
        float randX;
        float randY;
        float randZ;
        for (int i = 0; i < numGood; i++)
        {
            randX = Random.Range(1, stageLength);
            randY = Random.Range(1, stageHeight);
            randZ = Random.Range(1, stageWidth);

            //GameObject obj = (GameObject)Instantiate(goodResource);
            //obj.SetActive(false);
            //goodRes.Add(obj);
        }
        for (int i = 0; i < numBad; i++)
        {

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
