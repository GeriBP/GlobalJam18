﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

	public bool isVegan;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player") && other.GetComponent<ShootController> ().ammo < other.GetComponent<ShootController> ().maxAmmo)
		{
			AudioManager.instance.Play ("pickup");
			Spawner.count--;
			other.GetComponent<ShootController> ().Refill ();
			Destroy (gameObject);
		}
	}
}
