using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	Rigidbody2D rb;
	Collider2D col;
	public int playerLayer;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();

		col = GetComponent<Collider2D> ();
		col.enabled = false;

		InvokeRepeating ("checkSpeed", 1, 1);
	}
	
	void checkSpeed()
	{
		if (rb.velocity.sqrMagnitude < 1) 
		{
			Die ();
		}
	}

	void Die()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") && other.gameObject.layer != playerLayer)
		{
			other.gameObject.SetActive (false);
			Die ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		col.enabled = true;
	}
}
