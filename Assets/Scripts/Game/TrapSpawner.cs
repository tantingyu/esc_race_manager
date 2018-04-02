using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrapSpawner : NetworkBehaviour {

    public int startLevel = 0;
    public Level[] levels = new Level[3];

    private int currentLevel;
    private readonly float[] position = { 1.32f, 0.12f, -1.08f };

    public GameObject bearTrap;

    void Start () {
        currentLevel = startLevel;
		foreach (Level level in levels)
        {
            level.spawnTimer = level.spawnTime;
        }
	}

    void Update () {
        levels[currentLevel].levelDuration -= Time.deltaTime;
        levels[currentLevel].spawnTimer -= Time.deltaTime;

        // instantiates random trap defined from level every spawn time
        if (levels[currentLevel].spawnTimer < 0)
        {
            int randTrap = Random.Range(0, levels[currentLevel].trapsToSpawn.Length);

            if (RandomBool())
                CmdSpawnTrap(bearTrap, 0);
            if (RandomBool())
                CmdSpawnTrap(bearTrap, 1);
            if (RandomBool())
                CmdSpawnTrap(bearTrap, 2);

            levels[currentLevel].spawnTimer = levels[currentLevel].spawnTime;
        }

        // increase difficulty level when duration ends
        if (levels[currentLevel].levelDuration < 0 && currentLevel < levels.Length-1)
            currentLevel++;
    }
    
    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }

    [Command]
    public void CmdSpawnTrap(GameObject trap, int lane)
    {
        Vector2 pos = new Vector2(transform.position.x, position[lane]);
        GameObject trapInstance = Instantiate(trap, pos, Quaternion.identity);
        NetworkServer.Spawn(trapInstance);
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
