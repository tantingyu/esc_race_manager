using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironmentSpawner : NetworkBehaviour {

    public float spawnTime = 10f;
    public GameObject[] objectsToSpawn;
    private float timer;

    public GameObject fence;
    public GameObject grandstand;

    void Start () {
        timer = spawnTime;
	}
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            int r = Random.Range(0, objectsToSpawn.Length);
            if (r == 0)
                CmdSpawnBackground(fence);
            if (r == 1)
                CmdSpawnBackground(grandstand);
            timer = spawnTime;
        }
	}

    [Command]
    void CmdSpawnBackground(GameObject background)
    {
        Vector3 pos = new Vector3(Random.Range(28f, 50f), background.transform.position.y, background.transform.position.z);
        GameObject backgroundInstance = Instantiate(background, pos, Quaternion.identity);
        NetworkServer.Spawn(backgroundInstance);
    }
}
