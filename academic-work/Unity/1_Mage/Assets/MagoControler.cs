using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoControler : MonoBehaviour {
	private Rigidbody2D rb;
	private Animator anim;
	public float MaxSpeed;
	private bool facingRight;

	public Transform GroundCheck;
	public float GroundRadius = 0.2f;
	public LayerMask WhatIsGround;
	public bool grounded;

	// Use this for initialization
	void Start () {
		MaxSpeed = 10.0f;
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
			rb.AddForce (new Vector2 (0.0f, 800.0f));
		}
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (GroundCheck.position, GroundRadius, WhatIsGround);
		float move = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(move * MaxSpeed, rb.velocity.y);
		anim.SetFloat ("Speed", Mathf.Abs(move));

		if ((facingRight && move < 0) || (!facingRight && move > 0)) {
			Flip ();
		}


		// float moveV = Input.GetAxis ("Vertical");
		// rb.velocity = new Vector2(rb.velocity.x, moveV * MaxSpeed);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
