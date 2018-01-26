using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public string id;
    [Header ("Player Movement")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    GameObject groundP;
    [SerializeField]
    LayerMask mask;

    private bool grounded = false;
    private Vector2 normal;
	void Start () {
		
	}
	
	void Update () {
        grounded = checkGround();
    }

    private void FixedUpdate()
    {
        
    }

    private bool checkGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundP.transform.position, Vector2.down, 1f, mask);
        Debug.DrawRay(groundP.transform.position, Vector2.down, Color.red, 1f);
        bool hitBool = false;
        if (hit.collider != null)
        {
            normal = hit.normal;
            hitBool = true;
            return hitBool;
        }
        else
        {
            normal = new Vector2(0, 1);
            return hitBool;
        }
    }
}
