using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour {
    public bool OFF;
    public float ROTATION_SPEED;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (OFF)
            return;

        transform.Rotate(Vector3.forward * ROTATION_SPEED);
    }

}
