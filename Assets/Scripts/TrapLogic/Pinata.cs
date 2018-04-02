using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pinata : NetworkBehaviour {

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileTime=1f;
    private float p_timer;

	// Use this for initialization
	void Start () {
        p_timer = projectileTime;
	}
	
	// Update is called once per frame
	void Update () {
        p_timer -= Time.deltaTime;

        if (p_timer < 0) {
            //pick random player
            //get angle from x,y
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Vector3 movement = rotation * Vector3.forward;
            GameObject p_spawn = Instantiate(projectile, transform.position, rotation);
            NetworkServer.Spawn(p_spawn);
            p_spawn.GetComponent<BaseTrap>().velocity = movement;
            p_timer = projectileTime;
        }
	}
}
