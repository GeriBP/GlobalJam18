﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public string id;
    public bool isVegan;
    [Header ("Player Movement")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpYSpeed;
    [SerializeField]
    float secondMultiplier;
    [SerializeField]
    float airMultiplier;
    [SerializeField]
    int nJumps;
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
    [SerializeField]
    GameObject landing;
    [SerializeField]
    GameObject takeOff;
    [SerializeField]
    GameObject takeOff2;
	[SerializeField]
	GameObject ammo;

    public bool move = true;

    private Rigidbody2D myRb;
    private bool canJump = true;
    private int currJumps;
    public bool faceLeft = true;
    private Animator mynim;

    private bool grounded = false;
    private Vector2 normal;

	void Start () {
        myRb = GetComponent<Rigidbody2D>();
        currJumps = nJumps;
        mynim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        GroundChecking();
        if (canJump && currJumps > 0 && Input.GetButtonDown(id + "A"))
        {
            canJump = false;
            Invoke("EnableJump", jumpTime);
            myRb.velocity = new Vector2(myRb.velocity.x, 0.0f);
            float applyForce = jumpForce;
            if (currJumps < nJumps)
            {
				// Second jump
				AudioManager.instance.Play ("jump2");
                applyForce *= secondMultiplier;
            }
			else
				AudioManager.instance.Play ("jump1");
            myRb.AddForce(Vector2.up * applyForce, ForceMode2D.Impulse);
            --currJumps;
            if(grounded) Instantiate(takeOff, groundP.transform.position, Quaternion.identity);
            else Instantiate(takeOff2, groundP.transform.position, Quaternion.identity);
        }

        //Jump "Game-feel" improvement
        if (myRb.velocity.y < jumpYSpeed) //If we are falling
        {
            //We apply more force downwards to fall faster
            myRb.velocity -= Vector2.down * Physics2D.gravity.y * fallMult * Time.deltaTime;
        }
        else if (myRb.velocity.y > 0 && !Input.GetButton(id + "A")) //If we are going up and not pressing the jump button
        {
            //We apply more force downwards to fall faster
            myRb.velocity -= Vector2.down * Physics2D.gravity.y * lowJumpMult * Time.deltaTime;
        }
        if (myRb.velocity.x < 0 && !faceLeft && myRb.velocity.magnitude > 2.0f)
        {
            Flip();
        }
        else if (myRb.velocity.x > 0 && faceLeft && myRb.velocity.magnitude > 2.0f)
        {
            Flip();
        }
        mynim.SetBool("grounded", grounded);
        mynim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
    }

    private void GroundChecking()
    {
        if (!grounded && checkGround()) //if we land
        {
            Instantiate(landing, groundP.transform.position, Quaternion.identity);
            currJumps = nJumps;
        }
        grounded = checkGround();
    }

    private void EnableJump()
    {
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (grounded && Input.GetAxis(id + "Horizontal") > 0 && move)
        {
            myRb.velocity = new Vector2(moveSpeed, myRb.velocity.y);
        }
        else if (grounded && Input.GetAxis(id + "Horizontal") < 0 && move)
        {
            myRb.velocity = new Vector2(-moveSpeed, myRb.velocity.y);
        }
        if (!grounded && Input.GetAxis(id + "Horizontal") > 0)
        {
            myRb.velocity = new Vector2(moveSpeed * airMultiplier, myRb.velocity.y);
        }
        else if (!grounded && Input.GetAxis(id + "Horizontal") < 0)
        {
            myRb.velocity = new Vector2(-moveSpeed * airMultiplier, myRb.velocity.y);
        }
        if (Input.GetAxis(id + "Horizontal") == 0)
        {
            myRb.velocity = new Vector2(0.0f, myRb.velocity.y);
        }
		if (!move && grounded)
		{
			myRb.inertia = 0;
			myRb.velocity = new Vector2(0.0f, myRb.velocity.y);
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

    void Flip()
    {
        faceLeft = !faceLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
		ammo.transform.localScale = new Vector3(ammo.transform.localScale.x * -1.0f, ammo.transform.localScale.y, ammo.transform.localScale.z);
    }
}
