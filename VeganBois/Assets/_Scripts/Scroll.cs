using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {
    public float speed, limit, offset, z;
    public GameObject sky1, sky2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        sky1.transform.position += Vector3.left * speed * Time.deltaTime;
        sky2.transform.position += Vector3.left * speed * Time.deltaTime;
        if (sky1.transform.position.x < limit) sky1.transform.position = new Vector3(sky2.transform.position.x + offset, sky2.transform.position.y, z);
        if (sky2.transform.position.x < limit) sky2.transform.position = new Vector3(sky1.transform.position.x + offset, sky1.transform.position.y, z);
    }
}
