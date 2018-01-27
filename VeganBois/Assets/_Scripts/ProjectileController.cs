using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	Rigidbody2D rb;
	public bool isVegan;
	public int playerId;

	SpriteRenderer sprite;
	public Sprite[] veggieSprites;
	public Sprite[] meatSprites;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();

		sprite = GetComponent<SpriteRenderer> ();
		if (isVegan)
			sprite.sprite = veggieSprites[Random.Range (0, veggieSprites.Length)];
		else
			sprite.sprite = meatSprites[Random.Range (0, meatSprites.Length)];

		InvokeRepeating ("checkSpeed", 1, 1);

		transform.rotation = Quaternion.Euler (0, 0, Random.Range (0.0f, 360.0f));
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
			FourPlayerM manager = FindObjectOfType<FourPlayerM> ();
			//if (!manager)
				// Two player manager
			PlayerMove player = other.gameObject.GetComponent<PlayerMove> ();
			if (manager != null)
				manager.ManageHit (playerId, int.Parse (player.id), isVegan);
			
			Die ();
		}
	}
}
