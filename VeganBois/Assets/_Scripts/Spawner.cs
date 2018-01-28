using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;

	public float minX, maxX, y, spawnTime, maxSpawned, thresh;

	public static int count;

    GameObject[] pickU;

    void Start ()
	{
		count = 0;
		InvokeRepeating ("Spawn", 0, spawnTime);
		AudioManager.instance.Play ("game");
		Cursor.visible = false;
	}

	void Spawn()
	{
		if (count > maxSpawned)
			return; // CLEAN CODE YEAH!

        float x = Random.Range (minX, maxX);
		Vector3 pos = new Vector3 (x, y, 0);

        pickU = GameObject.FindGameObjectsWithTag("pick");
        while (!CleanSpawn(x))
        {
            x = Random.Range(minX, maxX);
            pos = new Vector3(x, y, 0);
        }

		Instantiate (prefab, pos, Quaternion.identity);
		count++;
	}

    bool CleanSpawn(float x)
    {
        bool isClean = true;

        foreach (GameObject p in pickU)
        {
            if (Mathf.Abs(x - p.transform.position.x) < thresh)
            {
                isClean = false;
            }
        }
        return isClean;
    }
}
