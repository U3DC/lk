﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;
	public GameObject sword;

	void Start () {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
		sword = GameObject.Find("Sword");
	}

	void Update() {
		PlayerAnimate();
		SwordPosition();
	}

	void PlayerAnimate() {
		if (playerControl.canMove) {
			animator.SetBool("run", Input.GetAxis("Horizontal") != 0 ? true : false);
			sword.GetComponent<Animator>().Play(Input.GetAxis("Horizontal") != 0 ? "Sword_Run" : "Sword_Idle");
		}

		animator.SetBool("air", !playerControl.isGrounded);
		// TODO: Try to refactor this IF & check if sword atk jump do not mess landing anim
		if (!playerControl.isGrounded) {
			sword.GetComponent<Animator>().Play("Sword_Jump");
		}

		// TODO: Can all be done with conditional operators
		// if (playerAttack) {
		// 	if (playerAttack.isAttacking) {
		// 		animator.SetBool("attack", true);
		// 	}
		// 	else {
		// 		animator.SetBool("attack", false);
		// 	}
		// }

		// if (playerHealth) {
		// 	if (playerHealth.isHurt) {
		// 		animator.SetBool("hurt", true);
		// 	}
		// 	else {
		// 		animator.SetBool("hurt", false);
		// 	}

		// 	// TOFIX: If isHurt is not used the ghost appears before the death animation
		// 	if (playerHealth.isDead && !playerHealth.isHurt) {
		// 		GetComponent<Animator>().SetTrigger("die");
		// 	}
		// }
	}

	// Check if this method can be refactored
	void SwordPosition() {
		if (GetComponent<SpriteRenderer>().flipX) {
			sword.GetComponent<SpriteRenderer>().flipX = true;
			sword.transform.localPosition = new Vector2(-0.74f, sword.transform.localPosition.y);
			sword.GetComponent<BoxCollider2D>().offset = new Vector2(-0.25f, 0.45f);
		} else {
			sword.GetComponent<SpriteRenderer>().flipX = false;
			sword.transform.localPosition = new Vector2(0.74f, sword.transform.localPosition.y);
			sword.GetComponent<BoxCollider2D>().offset = new Vector2(0.25f, 0.45f);
		}
	}

}
