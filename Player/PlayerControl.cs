﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float runSpeed = 40;
	public float jumpSpeed = 600;
	public bool canMove = true;
	public bool isGrounded;
	Rigidbody2D rb;
	

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (canMove && !GetComponent<PlayerHealth>().isDead) {
			PlayerMove();
			Flip();
		}
	}

	void PlayerMove() {
		if (Input.GetAxis("Horizontal") != 0) {
			rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y) ;
		}
		if (Input.GetButtonDown("Jump") && isGrounded) {
			rb.velocity = Vector2.up * jumpSpeed;
		}
	}

	void Flip() {
		if (Input.GetAxis("Horizontal") > 0) {
			GetComponent<SpriteRenderer>().flipX = false;
			GetComponent<CapsuleCollider2D>().offset = new Vector2(0.06f, -0.04f);
       	 	GameObject.Find("Ghost").GetComponent<SpriteRenderer>().flipX = false;
		}

		if (Input.GetAxis("Horizontal") < 0) {
			GetComponent<SpriteRenderer>().flipX = true;
			GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.06f, -0.04f);
        	GameObject.Find("Ghost").GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}
}
