using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceController : MonoBehaviour {
    private Rigidbody2D rb;
    private Animator anim;

    public bool OFF;
    public float MAX_HIGHT;
    public float MOVE_SPEED;

    private float startingHight;
    private bool movingUp;

    public float BLINK_DELAY;
    private float timeToBlink;
    private bool blinking = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startingHight = groundCheck.transform.position.y;
        timeToBlink = BLINK_DELAY;
    }
	
	// Update is called once per frame
	void Update () {
        if (OFF)
            return;
        Direction();
        Movement();
        Blinking();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Direction()
    {
        if (grounded)
        {
            movingUp = true;
        }
        else if (rb.position.y >= startingHight + MAX_HIGHT)
        {
            movingUp = false;
        }
    }

    private void Movement()
    {
        if (movingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, MOVE_SPEED);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -MOVE_SPEED);
        }

    }

    private void Blinking()
    {
        timeToBlink -= Time.deltaTime;

        if (timeToBlink <= 0)
        {
            blinking = true;
            timeToBlink = BLINK_DELAY;
        }
        else
        {
            blinking = false;
        }

        anim.SetBool("Blinking", blinking);
    }
}
