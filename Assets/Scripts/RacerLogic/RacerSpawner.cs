﻿using System.Collections;
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
        GameObject shieldInstance = Instantiate(shield, new Vector2(transform.position.x + 3f, transform.position.y), Quaternion.identity);
        shieldInstance.transform.parent = gameObject.transform;
        NetworkServer.Spawn(shieldInstance);
    }

    [Command]
    public void CmdSpawnHeal()
    {
        GameObject healInstance = Instantiate(heal, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
        healInstance.transform.parent = gameObject.transform;
        NetworkServer.Spawn(healInstance);
    }
}
