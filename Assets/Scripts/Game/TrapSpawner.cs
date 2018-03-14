using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour {

    public float spawnTime = 1f;
    private float spawnTimer;
    public Transform trap;
    private readonly float[] position = { 1.32f, 0.12F, -1.08f };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            int randY = Random.Range(0, 3);
            Instantiate(trap, new Vector2(transform.position.x, position[randY]), Quaternion.identity);
            spawnTimer = spawnTime;
        }
    }
}
