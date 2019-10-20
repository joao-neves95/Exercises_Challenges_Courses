using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    private PlayerController player;
    private MainCameraController mainCamera;

    public Text coinsPickedText;
    public float RESPAWN_FREEZE_TIME;
    public GameObject currentRespawnLocation;

    private static int coinsPicked;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        currentRespawnLocation = GameObject.FindGameObjectWithTag("Checkpoint_1");
        mainCamera = FindObjectOfType<MainCameraController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PrintCoinsToUi();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        ResetCoinsPicked();
        mainCamera.following = false;
        player.FreezePlayer();
        player.transform.position = new Vector3(currentRespawnLocation.transform.position.x, currentRespawnLocation.transform.position.y, -2);
        yield return new WaitForSeconds(RESPAWN_FREEZE_TIME);
        PlayerController.ResetHealth(player.MAX_HEALTH);
        mainCamera.following = true;
    }

    public static void AddCoins(int coinsToAdd)
    {
        coinsPicked += coinsToAdd;
    }

    public static void RemoveCoins(int coinsToRemove)
    {
        coinsPicked -= coinsToRemove;

    }

    public static void ResetCoinsPicked()
    {
        coinsPicked = 0;
    }

    private void PrintCoinsToUi()
    {
        if (coinsPicked < 0)
            coinsPicked = 0;

        coinsPickedText.text = coinsPicked.ToString();
    }
}
