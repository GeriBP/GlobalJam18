using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [SerializeField]
    Rigidbody2D myRb;
    [SerializeField]
    float force, offset, jumpForce;
    [SerializeField]
    GameObject groundP;
    [SerializeField]
    LineRenderer lineR, lineRF;
    private bool grounded = false;
    private bool jumpUp = true;
    private Vector2 normal;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        grounded = checkGround();
        //Debug.Log(grounded);

        //Set normal debug
        lineR.SetPosition(0, new Vector3(transform.position.x, transform.position.y - offset, -1));
        lineR.SetPosition(1, new Vector3(transform.position.x + normal.x, transform.position.y - offset + normal.y, -1));

        //Set velocity debug
        lineRF.SetPosition(0, new Vector3(0, 0, -1));
        lineRF.SetPosition(1, new Vector3(normal.y,  -normal.x, -1));

        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            if(grounded) myRb.AddForce(new Vector2(normal.y, -normal.x) * Input.GetAxis("Horizontal") * force, ForceMode2D.Force);
            else myRb.AddForce(new Vector2(normal.y, -normal.x) * Input.GetAxis("Horizontal") * force * 0.5f, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.W) && grounded && jumpUp)
        {
            jumpUp = false;
            StartCoroutine(jumpBlock());
            myRb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator jumpBlock()
    {
        yield return new WaitForSeconds(0.2f);
        jumpUp = true;
    }

    private bool checkGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundP.transform.position, Vector2.down, 1f);
        bool hitBool = false;
        if (hit.collider != null)
        {
            normal = hit.normal;
            hitBool = true;
            return hitBool;
        }
        else
        {
            normal = new Vector2(0,1);
            return hitBool;
        }
    }

}
