using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private Animator anim;
    private LevelManager levelManager;


    public float MOVE_SPEED;
    public float SPEED_ON_WATER;

    public int MAX_HEALTH;

    private static int playerHealth;

    public static int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
    }

    public float JUMP_HEIGTH;
    public bool onWater = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool rotating;
    private bool doubleJumped = false;

    public float BLINK_DELAY;
    private float timeToBlink;
    private bool blinking = false;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        ResetHealth(MAX_HEALTH);
        timeToBlink = BLINK_DELAY;
    }

    // Update is called once per frame
    void Update() {
        Blinking();
    }

    private void FixedUpdate()
    {
        if (playerHealth <= 0)
        {
            levelManager.RespawnPlayer();
            ResetHealth(MAX_HEALTH);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);
        Movement();
        Jumping();
    }

    private void Movement()
    {
        if (!onWater)
        {
            float moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveX * MOVE_SPEED, rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            rb.gravityScale = 2f;
        }
        else if (onWater)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(moveX * SPEED_ON_WATER, moveY * SPEED_ON_WATER);
            rb.gravityScale = 3f;
            anim.SetFloat("Speed", 0.0f);
        }

        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        else if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
    }


    private void Jumping()
    {
        if (grounded)
        {
            doubleJumped = false;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            Jump();
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !grounded && !doubleJumped)
        {
            Jump();
            doubleJumped = true;
        }
    }

    private void Jump() => rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y * Time.deltaTime + JUMP_HEIGTH), ForceMode2D.Impulse);

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

    public static void RemoveHealth(int healthToRemove)
    {
        playerHealth -= healthToRemove;
        Debug.Log(playerHealth);
    }

    public static void AddHealth(int healthToAdd)
    {
        playerHealth += healthToAdd;
    }

    public static void RemoveAllHealth()
    {
        playerHealth = 0;
    }

    public static void ResetHealth(int MAX_HEALTH)
    {
        playerHealth = MAX_HEALTH;
        Debug.Log(playerHealth);
    }

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
