using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDownController : MonoBehaviour {
    private Rigidbody2D rb;
    private KillPlayer killPlayer;

    public float DROP_FORCE;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        killPlayer = gameObject.GetComponent<KillPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        GroundedController();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public void DropSpike()
    {
        rb.AddForce(Vector2.down * DROP_FORCE, ForceMode2D.Impulse);
    }

    private void GroundedController()
    {
        if (grounded)
        {
            killPlayer.OFF = true;
            rb.simulated = false;
        }
    }
}
