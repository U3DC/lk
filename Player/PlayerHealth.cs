﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	GameObject enemy;
	public int pushX = 10;
	public int pushY = 10;
	Image healthBar;
	public bool isDead;

	void Start() {
		healthBar = GameObject.Find("Content").GetComponent<Image>();
	}

	void Update() {
		if(healthBar.fillAmount <= 0) {
			StartCoroutine(Death());
		}
	}

	// TODO: Fix couritine issue, player can move after death
	IEnumerator PushBack(GameObject enemy) {
		GetComponent<PlayerControl>().canMove = false;
		if (enemy.transform.position.x > transform.position.x) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushX, pushY), ForceMode2D.Impulse);
		} else {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(pushX, pushY), ForceMode2D.Impulse);			
		}
		yield return new WaitForSeconds(0.5f);
		GetComponent<PlayerControl>().canMove = true;
	}
	
	IEnumerator Death() {
		isDead = true;
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Scene_1_RoadtoForest");
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			StartCoroutine(PushBack(other.gameObject));
			healthBar.fillAmount -= 0.25f;
		}
	}
}