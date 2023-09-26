using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {
    public ParticleSystem thisParticleSystem;

	// Use this for initialization
	void Start () {
        thisParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (thisParticleSystem.isStopped)
        {
            Destroy(gameObject);
        }
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
