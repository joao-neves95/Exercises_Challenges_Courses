using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public LevelManager levelManager;

    public bool OFF;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player" && !OFF)
        {
            PlayerController.RemoveAllHealth();
            levelManager.RespawnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && gameObject.tag != "FalseKillTrigger" && !OFF)
        {
            PlayerController.RemoveAllHealth();
            levelManager.RespawnPlayer();
        }
    }
}
