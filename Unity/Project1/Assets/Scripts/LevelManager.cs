using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public float RESPAWN_FREEZE_TIME = 0.7f;
    public int POINT_PENALTY_DEATH = 2;

    public GameObject currentCheckpoint;
    public GameObject deathParticle;
    public GameObject respawnParticle;

    private PlayerController player;
    private CameraController cameraController;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        currentCheckpoint = GameObject.FindGameObjectWithTag("Checkpoint1");
        deathParticle = Resources.Load("DeathParticles") as GameObject;
        respawnParticle = Resources.Load("RespawnParticles") as GameObject;
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update () {
	}

    public void RespawnPlayer()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        cameraController.isFollowing = false;
        player.FreezePlayer();
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        ScoreManager.RemovePoints(POINT_PENALTY_DEATH);
        yield return new WaitForSeconds(RESPAWN_FREEZE_TIME);
        Debug.Log(currentCheckpoint);
        player.transform.position = currentCheckpoint.transform.position;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        cameraController.isFollowing = true;
    }
}
