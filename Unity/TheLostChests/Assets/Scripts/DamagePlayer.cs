using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    private PlayerController player;

    public int DAMAGE;
    public int COINS_TO_REMOVE;
    public float PUSH_MAGNITUDE;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            PlayerController.RemoveHealth(DAMAGE);
            LevelManager.RemoveCoins(COINS_TO_REMOVE);
            PushRigidbody2D(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerController.RemoveHealth(DAMAGE);
            LevelManager.RemoveCoins(COINS_TO_REMOVE);
            PushRigidbody2D(collision);
        }

        Destroyer();
    }

    private void PushRigidbody2D(Collider2D collision)
    {
        Vector2 force = -(transform.position - collision.transform.position).normalized;
        player.GetComponent<Rigidbody2D>().AddForce(force * PUSH_MAGNITUDE);
    }

    private void PushRigidbody2D(Collision2D collision)
    {
        Vector2 force = -(transform.position - collision.transform.position).normalized;
        player.GetComponent<Rigidbody2D>().AddForce(force * PUSH_MAGNITUDE);
    }

    private void Destroyer()
    {
        if (gameObject.tag == "Destroy")
            Destroy(gameObject);
    }
}
