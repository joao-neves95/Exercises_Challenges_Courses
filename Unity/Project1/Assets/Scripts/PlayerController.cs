using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private Animator anim;
    private LevelManager levelManager;
    public GameObject ninjaStar;
    public Transform firePoint;

    public float MOVE_SPEED;
    public float JUMP_HEIGHT;
    public float SHOT_DELAY;
    private float shotDelayCounter;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJump;

	// Use this for initialization.
	void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        levelManager = gameObject.AddComponent<LevelManager>();
    }
	
	// Update is called once per frame.
	void Update() {
        Movement();
        Atacking();
	}

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        Jumping();
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * MOVE_SPEED, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void Jumping()
    {
        if (grounded)
        {
            doubleJump = false;
        }

        anim.SetBool("Grounded", grounded);

        if ((Input.GetKeyDown(KeyCode.UpArrow)) && grounded && doubleJump == false)
        {
            Jump();
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow)) && !grounded && doubleJump == false)
        {
            Jump();
            doubleJump = true;
        }
    }

    private void Jump() => rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y * Time.deltaTime + JUMP_HEIGHT), ForceMode2D.Impulse);

    private void Atacking()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            shotDelayCounter = SHOT_DELAY;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = SHOT_DELAY;
                Shoot();
            }
        }
    }

    private void Shoot() => Instantiate(ninjaStar, firePoint.position, firePoint.rotation);

    public void FreezePlayer()
    {
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        float temp = rb.gravityScale;
        rb.GetComponent<Renderer>().enabled = false;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(levelManager.RESPAWN_FREEZE_TIME);
        rb.gravityScale = temp;
        rb.GetComponent<Renderer>().enabled = true;
    }
}
