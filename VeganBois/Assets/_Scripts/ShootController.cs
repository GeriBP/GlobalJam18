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
		Vector2 direction;

		target.transform.position = transform.position + direction * targetDistance;
	}
}
