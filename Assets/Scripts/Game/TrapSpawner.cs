using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrapSpawner : NetworkBehaviour {

    public int startLevel = 0;
    public Level[] levels = new Level[3];

    private int currentLevel;
    private readonly float[] position = { 1.32f, 0.12f, -1.08f };
    private readonly float[] spawnXRange = new float[2];

    void Start () {
        currentLevel = startLevel;
		foreach (Level level in levels)
        {
            level.spawnTimer = level.spawnTime;
        }
        spawnXRange[0] = transform.position.x;
        spawnXRange[1] = transform.position.x + 5;
    }

    void Update () {
        levels[currentLevel].levelDuration -= Time.deltaTime;
        levels[currentLevel].spawnTimer -= Time.deltaTime;

        if (levels[currentLevel].spawnOnce) {
            if (!levels[currentLevel].hasSpawned)
            {
                CmdSpawnTrap(levels[currentLevel].trapsToSpawn[0], 1);
                levels[currentLevel].hasSpawned = true;
            }
        }

        // instantiates random trap defined from level every spawn time
        else if (levels[currentLevel].spawnTimer < 0 && levels[currentLevel].hasTraps())
        {
            int randTrap = Random.Range(0, levels[currentLevel].trapsToSpawn.Length);

            if (RandomBool())
                CmdSpawnTrap(levels[currentLevel].trapsToSpawn[randTrap], 0);
            if (RandomBool())
                CmdSpawnTrap(levels[currentLevel].trapsToSpawn[randTrap], 1);
            if (RandomBool())
                CmdSpawnTrap(levels[currentLevel].trapsToSpawn[randTrap], 2);

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
        Vector2 pos = new Vector2(Random.Range(spawnXRange[0], spawnXRange[1]), position[lane]);
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
    public bool spawnOnce;

    [HideInInspector]
    public float spawnTimer;
    [HideInInspector]
    public bool hasSpawned = false;

    public bool hasTraps()
    {
        return trapsToSpawn.Length > 0;
    }
}
