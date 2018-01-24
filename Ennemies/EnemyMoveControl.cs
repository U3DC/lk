using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    public bool canMove = true;
    public float moveSpeed = 3;
    GameObject player;
    SpriteRenderer sprite;

    void Start() {
	    player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
	}

    void Update() {
        if (canMove && !player.GetComponent<PlayerHealth>().isDead) {
            if (player.transform.position.x > transform.position.x) {
                sprite.flipX = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
			} else {
                sprite.flipX = false;                
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
			}
        }

        followPlayer();
    }

    void followPlayer() {
        float Xpos = player.transform.position.x - transform.position.x;
        float Ypos = player.transform.position.y - transform.position.y;
        
        if (Xpos < 9.0f && Xpos > -9.0f && Ypos < 2.5f && Ypos > -1) {
            canMove = true;
        } else {
            canMove = false;
        }
    }
}