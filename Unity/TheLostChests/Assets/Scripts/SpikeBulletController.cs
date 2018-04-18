using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBulletController : MonoBehaviour {
    private Rigidbody2D rb;

    public float SPEED;
    public bool SHOOT_RIGHT;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (SHOOT_RIGHT)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb.velocity = new Vector2(SPEED, rb.velocity.y);
        }
        else if (!SHOOT_RIGHT)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb.velocity = new Vector2(-SPEED, rb.velocity.y);
        }
    }
}
