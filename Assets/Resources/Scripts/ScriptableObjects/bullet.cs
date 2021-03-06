﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {



 	public bulletScriptable bulletScript;
	private string tagTarget;


	private float speed;
	private int damage;


	private Rigidbody2D rb2d;
	public GameObject pool;

    public GameObject player_bullet_explosion;


	// Update is called once per frame
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Awake()
	{
		rb2d=GetComponent<Rigidbody2D>();
        pool = transform.parent.gameObject;
        // transform.rotation=(transform.localRotation);
    }

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>


	void FixedUpdate () {
		Vector2 moving=transform.right*speed*Time.deltaTime;

		rb2d.MovePosition(rb2d.position+moving);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(tagTarget=="Enemy"&& other.tag=="Enemy")
		{
			other.GetComponent<DumbEnemyAI>().enemyHealth-=damage;

            Instantiate(player_bullet_explosion, transform.position, Quaternion.identity);
            CancelInvoke();

            RET();
		}
		if((tagTarget=="Player" || tagTarget == "Player_Boss")&& other.tag=="Player")
		{
			other.GetComponent<Player>().TakeDamage(damage);
            CancelInvoke();

            RET();
		}

        if(other.tag == "Untagged")
        {
            if (tagTarget == "Player_Boss")
                return;
            if(tagTarget == "Enemy")
                Instantiate(player_bullet_explosion, transform.position, Quaternion.identity);
            CancelInvoke();
            RET();
        }

	}
	
	public void ReturnToPool(string shooter_type)
	{	
		if(shooter_type == "enemy")
		{
			speed=bulletScript.EnemySpeed;
			damage=bulletScript.EnemyDamage;
			tagTarget="Player";
			gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bulletScript.bulletTypes[2];
		}
		else if(shooter_type == "player")
		{
			if(bulletScript.upgraded){
				speed = bulletScript.UpPlayerSpeed;
				damage = bulletScript.UpPlayerDamage;
				gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material=bulletScript.special;
				tagTarget = "Enemy";
			}
			else
			{
                speed = bulletScript.UpPlayerSpeed;
                damage = bulletScript.PlayerDamage;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bulletScript.bulletTypes[0];
                tagTarget = "Enemy";
			}
		}
        else if(shooter_type == "boss")
        {
            speed = bulletScript.EnemySpeed;
            damage = bulletScript.EnemyDamage;
            tagTarget = "Player_Boss";
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bulletScript.bulletTypes[2];
        }
        gameObject.transform.parent = null;
        Invoke("RET",4);
	}

	void RET()
	{
        transform.position = pool.transform.position;
		gameObject.transform.SetParent(pool.transform);
		gameObject.SetActive(false);
    }
}
