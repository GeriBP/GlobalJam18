using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	Rigidbody2D rb;
	Collider2D col;
	public int playerLayer;
	public bool isVegan;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();

		col = GetComponent<Collider2D> ();
		//col.enabled = false;

		InvokeRepeating ("checkSpeed", 1, 1);
	}
	
	void checkSpeed()
	{
		if (rb.velocity.sqrMagnitude == 0)
		{
			Die ();
		}
	}

	void Die()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") && other.gameObject.GetComponent<PlayerMove> ().isVegan != isVegan)
		{
			other.gameObject.SetActive (false);
			FourPlayerM manager = FindObjectOfType<FourPlayerM> ();
			//if (!manager)
				// Two player manager
			PlayerMove player = other.gameObject.GetComponent<PlayerMove> ();
			if (manager)
				manager.ManageHit (0, int.Parse (player.id), player.isVegan);
			else
				Destroy (other.gameObject);
			Die ();
		}
	}
}
