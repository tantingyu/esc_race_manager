using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrapSpawner : NetworkBehaviour {

    public int startLevel = 0;
    public Level[] levels=new Level[3];

    private int currentLevel;
    private readonly float[] position = { 1.32f, 0.12f, -1.08f };

    // Use this for initialization
    void Start () {
        currentLevel = startLevel;
		foreach (Level level in levels)
        {
            level.spawnTimer = level.spawnTime;
        }
	}
	
	// Update is called once per frame
	void Update () {
        levels[currentLevel].levelDuration -= Time.deltaTime;
        levels[currentLevel].spawnTimer -= Time.deltaTime;

        //instantiates random trap defined from level every spawn time
        if (levels[currentLevel].spawnTimer < 0)
        {
            int randTrap = Random.Range(0, levels[currentLevel].trapsToSpawn.Length);
            //int randY = Random.Range(0, 3);
            
            if (RandomBool())
                NetworkServer.Spawn(Instantiate(levels[currentLevel].trapsToSpawn[randTrap], new Vector2(transform.position.x, position[0]), Quaternion.identity));
            if (RandomBool())
                NetworkServer.Spawn(Instantiate(levels[currentLevel].trapsToSpawn[randTrap], new Vector2(transform.position.x, position[1]), Quaternion.identity));
            if (RandomBool())
                NetworkServer.Spawn(Instantiate(levels[currentLevel].trapsToSpawn[randTrap], new Vector2(transform.position.x, position[2]), Quaternion.identity));
            levels[currentLevel].spawnTimer = levels[currentLevel].spawnTime;
        }

        //increase difficulty level when duration ends
        if (levels[currentLevel].levelDuration < 0 && currentLevel < levels.Length-1)
            currentLevel++;
        
    }
    
    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
}

[System.Serializable]
public class Level
{
    public float levelDuration;
    public float spawnTime = 2f;
    public GameObject[] trapsToSpawn = new GameObject[1];

    [HideInInspector]
    public float spawnTimer;
}
