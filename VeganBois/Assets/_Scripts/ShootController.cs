using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour 
{
	public GameObject target;

	public float targetDistance;

	void Start () 
	{
		
	}

	void Update () 
	{
		float x = Input.GetAxis ("1Horizontal");
		float y = Input.GetAxis ("1Vertical");
		Vector3 direction = new Vector3(x, y, 0);

		target.transform.position = transform.position + direction * targetDistance;
	}
}
