using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;

	public float minX, maxX, y, spawnTime;

	void Start ()
	{
		InvokeRepeating ("Spawn", 0, spawnTime);
	}

	void Spawn()
	{
		float x = Random.Range (minX, maxX);
		Vector3 pos = new Vector3 (x, y, 0);

		GameObject g = Instantiate (prefab, pos, Quaternion.identity);
	}
}
