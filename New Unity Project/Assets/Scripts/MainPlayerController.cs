﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float isGroundedDistance;
    public Transform tagGround;
    public Animator myAnimator;
    public SpriteRenderer mySpriteRenderer;
    public LayerMask excludePlayer; 
    private Rigidbody2D myRigidBody;

       // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (isGrounded())
        {
            Jump();
        }
    }

    void Move()
    {
        Vector2 movement = Vector2.zero;
        myAnimator.speed = 0;
        if (Input.GetKey(KeyCode.A))
        {
            //move left
            movement.x = -speed;
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            //move right
            movement.x = speed;
             
        }
        myRigidBody.velocity = movement;
        myAnimator.speed = speed; 
    }

    void SwordAttack()
    {

    }

    void Jump()
    {
        Vector2 jumpMovement = myRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
            jumpMovement.y = jumpSpeed;
        }
        myRigidBody.velocity = jumpMovement;
    }

    public bool isGrounded()
    {
        Vector2 origin = transform.position;
                      
        if (Physics2D.Linecast(origin, tagGround.position, excludePlayer))
        {
            return true;
        } else
            return false;
        
    }
}
