using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour 
{
	[Header("Configuration")]
	public GameObject target;
	public GameObject projectilePrefab;

	public float targetDistance, projectileSpeed;
	public int maxBullets = 3;

	Vector3 direction = Vector3.left;
	bool targetActive;
	public int bullets = 1;
	PlayerMove player;

    private void Start()
    {
        player = GetComponent<PlayerMove>();
    }

    void Update ()
	{
        handleDirection ();
		handleTarget ();
		handleShoot ();
	}

	void handleDirection()
	{
		float x = Input.GetAxis (player.id + "Horizontal");
		float y = Input.GetAxis (player.id + "Vertical");
		if (x != 0 || y != 0) 
		{
			direction = new Vector3 (x, y, 0).normalized;

			float angle = Vector3.SignedAngle (!player.faceLeft ? Vector3.right : Vector3.left, direction, Vector3.forward);
			target.transform.rotation = Quaternion.Euler (0, 0, angle);
		}
	}

	void handleShoot()
	{
		if (Input.GetButtonUp (player.id+"X") && bullets > 0)
		{
			//Debug.Log ("SHOOT!");
			GameObject g = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
			g.GetComponent<ProjectileController> ().isVegan = player.isVegan;
			g.GetComponent<ProjectileController> ().playerId = int.Parse (player.id);
			g.GetComponent<Rigidbody2D> ().velocity = direction * projectileSpeed;
			g.layer = player.isVegan ? LayerMask.NameToLayer ("Veggie") : LayerMask.NameToLayer ("Meat");
			--bullets;
		}
	}

	void handleTarget()
	{
		if (Input.GetButtonDown (player.id + "X"))
			targetActive = true;
		if (Input.GetButtonUp (player.id + "X"))
			targetActive = false;
		
		target.SetActive (targetActive);

		if (target.activeSelf) 
		{
			player.move = false;
			if (direction != Vector3.zero)
				target.transform.position = transform.position + direction * targetDistance;
		}

		if (Input.GetButtonUp (player.id + "X"))
			player.move = true;
	}

	public void Refill()
	{
		if (bullets < maxBullets)
			bullets++;
	}
}
