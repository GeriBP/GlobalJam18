using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public string id;
    [Header ("Player Movement")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpTime;
    [SerializeField]
    float fallMult;
    [SerializeField]
    float lowJumpMult;
    [SerializeField]
    GameObject groundP;
    [SerializeField]
    LayerMask mask;

    private Rigidbody2D myRb;
    private bool canJump = true;

    private bool grounded = false;
    private Vector2 normal;
	void Start () {
        myRb = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        grounded = checkGround();
        if (canJump && grounded && Input.GetKeyDown(KeyCode.W))
        {
            canJump = false;
            Invoke("EnableJump", jumpTime);
            myRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Jump "Game-feel" improvement
        if (myRb.velocity.y < 0) //If we are falling
        {
            //We apply more force downwards to fall faster
            myRb.velocity -= Vector2.down * Physics2D.gravity.y * fallMult * Time.deltaTime;
        }
        else if (myRb.velocity.y > 0 && !Input.GetKey(KeyCode.W)) //If we are going up and not pressing the jump button
        {
            //We apply more force downwards to fall faster
            myRb.velocity -= Vector2.down * Physics2D.gravity.y * lowJumpMult * Time.deltaTime;
        }
    }

    private void EnableJump()
    {
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (grounded && Input.GetAxis(id + "Horizontal") > 0)
        {
            myRb.velocity = new Vector2(moveSpeed, myRb.velocity.y);
        }
        else if (grounded && Input.GetAxis(id + "Horizontal") < 0)
        {
            myRb.velocity = new Vector2(-moveSpeed, myRb.velocity.y);
        }
    }

    private bool checkGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundP.transform.position, Vector2.down, 0.1f, mask);
        Debug.DrawRay(groundP.transform.position, Vector2.down, Color.red);
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
