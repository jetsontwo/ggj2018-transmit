﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb2d;
	// Update is called once per frame
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Awake()
	{
		rb2d=GetComponent<Rigidbody2D>();
        Invoke("bulletBeGone", 3f);

        // transform.rotation=(transform.localRotation);
    }
	void FixedUpdate () {
		Vector2 moving=transform.right*speed*Time.deltaTime;

		rb2d.MovePosition(rb2d.position+moving);
	}

	void bulletBeGone()
	{
		Destroy(this.gameObject);
	}
}
