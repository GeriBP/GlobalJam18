using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

	public bool isVegan;

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") && other.GetComponent<PlayerMove> ().isVegan == isVegan)
		{
			other.GetComponent<ShootController> ().Refill ();
			Destroy (gameObject);
		}
	}
}
