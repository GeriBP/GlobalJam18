using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour 
{
	public GameObject target;
	public GameObject projectilePrefab;

	public float targetDistance, projectileSpeed;

	Vector3 direction;

	void Start () 
	{
		
	}

	void Update () 
	{
		float x = Input.GetAxis ("1Horizontal");
		float y = Input.GetAxis ("1Vertical");
		direction = new Vector3(x, y, 0).normalized;

		target.transform.position = transform.position + direction * targetDistance;

		handleShoot ();
	}

	void handleShoot()
	{
		if (Input.GetButtonDown ("1X")) 
		{
			//Debug.Log ("SHOOT!");
			GameObject g = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
			g.GetComponent<Rigidbody2D> ().velocity = direction * projectileSpeed;
		}
	}
}
