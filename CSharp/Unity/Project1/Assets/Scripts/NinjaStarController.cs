using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour {
    private PlayerController player;
    private Rigidbody2D rb;
    public GameObject enemyDeathEffect;
    public GameObject impactEffect;

    public float SPEED;
    public float ROTATION_SPEED;
    public int KILL_POINTS;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();

        if (player.transform.localScale.x < 0)
        {
            SPEED = -SPEED;
            ROTATION_SPEED = -ROTATION_SPEED;
        }
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(SPEED, rb.velocity.y);
        rb.angularVelocity = ROTATION_SPEED;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ScoreManager.AddPoints(KILL_POINTS);
            Instantiate(enemyDeathEffect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
