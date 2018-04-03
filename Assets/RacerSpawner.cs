using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RacerSpawner : NetworkBehaviour
{
    public GameObject shield;
    public GameObject heal;

    [Command]
    public void CmdSpawnShield()
    {
        GameObject shieldInstance = Instantiate(shield, new Vector2(transform.position.x + 5, transform.position.y), Quaternion.identity);
        shieldInstance.transform.parent = gameObject.transform;
        NetworkServer.Spawn(shieldInstance);
    }

    [Command]
    public void CmdSpawnHeal()
    {
        GameObject healInstance = Instantiate(heal, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        NetworkServer.Spawn(healInstance);
    }
}
