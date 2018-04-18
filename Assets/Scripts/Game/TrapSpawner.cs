using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrapSpawner : NetworkBehaviour {

    public int startLevel = 0;
    public Level[] levels = new Level[3];

    public int currentLevel;
    private readonly float[] position = { 2.03f, 0.65f, -0.82f };
    private readonly float[] laserPosition = { 1.650871f, 0.2f, -1.24f };
    private readonly float[] spawnXRange = new float[2];

    [SerializeField]
    private GameObject cuteTrap;
    private float cuteTrapTimer = 0f;
    private int cautionLane = 0;

    //A list with stored player GUI that are waiting for something to happen
    private List<SharedCanvas> observers = new List<SharedCanvas>();

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

        if (levels[currentLevel].spawnOnce && levels[currentLevel].hasTraps()) {
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

        if (levels[currentLevel].spawnCuteTrap)
        {
            cuteTrapTimer -= Time.deltaTime;
            if (cuteTrapTimer < 0)
            {
                cautionLane = Random.Range(0, 3);
                NotifyObservers(0, cautionLane);
                Invoke("CmdSpawnCuteTrap", 3f);
                cuteTrapTimer = 10f;
            }
        }
        
        // increase difficulty level when duration ends otherwise maintain level till everyone dies :D
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

    [Command]
    public void CmdSpawnCuteTrap()
    {
        if (cuteTrap != null)
        {
            Vector2 pos = new Vector2(cuteTrap.transform.position.x, laserPosition[cautionLane]);
            GameObject trapInstance = Instantiate(cuteTrap, pos, Quaternion.identity);
            NetworkServer.Spawn(trapInstance);
        }
    }

    //Send notifications if dangerous trap is arriving
    public void NotifyObservers(int eventType, int lane)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            Debug.Log("CAUTION LANE " + cautionLane);
            //Notify all observers even though some may not be interested in what has happened
            //Each observer should check if it is interested in this event
            observers[i].UpdateObserver(eventType, lane);
            // observers[i].OnCaution(lane);
        }
    }

    //Add observer to the list
    public void AddObserver(SharedCanvas observer)
    {
        observers.Add(observer);
    }

    //Remove observer from the list
    public void RemoveObserver(SetupLocalPlayer observer)
    {
    }
}

[System.Serializable]
public class Level
{
    public float levelDuration;
    public float spawnTime = 2f;
    public GameObject[] trapsToSpawn = new GameObject[1];
    public bool spawnOnce;
    public bool spawnCuteTrap;

    [HideInInspector]
    public float spawnTimer;
    [HideInInspector]
    public bool hasSpawned = false;

    public bool hasTraps()
    {
        return trapsToSpawn.Length > 0;
    }
}
