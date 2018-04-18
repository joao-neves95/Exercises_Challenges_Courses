using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDownTriggerController : MonoBehaviour {
    public GameObject SPIKE;
    public bool OFF;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !OFF)
        {
            SPIKE.GetComponent<SpikeDownController>().DropSpike();
        }
    }
}
