using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour {

    public float spawnTime=10f;
    public GameObject[] objectsToSpawn;
    private float timer;

    // Use this for initialization
    void Start () {
        timer = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            int r = Random.Range(0, objectsToSpawn.Length);
            Instantiate(objectsToSpawn[r], new Vector3(Random.Range(28f, 50f), objectsToSpawn[r].transform.position.y, objectsToSpawn[r].transform.position.z), 
                Quaternion.identity);
            timer = spawnTime;
        }
	}
}
