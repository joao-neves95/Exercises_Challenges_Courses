using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {
    public GameObject player;
    public bool following;

    // Use this for initialization
    void Start () {
        following = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!following)
            return;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
