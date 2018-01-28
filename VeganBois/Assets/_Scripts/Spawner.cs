using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;

	public float minX, maxX, y, spawnTime, maxSpawned;

	public static int count;

	void Start ()
	{
		count = 0;
		InvokeRepeating ("Spawn", 0, spawnTime);
	}

	void Spawn()
	{
		if (count > maxSpawned)
			return;	// CLEAN CODE YEAH!
		
		float x = Random.Range (minX, maxX);
		Vector3 pos = new Vector3 (x, y, 0);
		Instantiate (prefab, pos, Quaternion.identity);
		count++;
	}
}
