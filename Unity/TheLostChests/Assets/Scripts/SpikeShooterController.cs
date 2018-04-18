using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeShooterController : MonoBehaviour {
    public GameObject SPIKE_BULLET;
    public float SHOOT_DELAY;

    private float delay;

	// Use this for initialization
	void Start () {
        delay = SHOOT_DELAY;
	}
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}

    private void Shoot()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            Instantiate(SPIKE_BULLET, transform.position, transform.rotation);
            delay = SHOOT_DELAY;
        }

    }
}
