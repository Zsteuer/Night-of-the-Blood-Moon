using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerPosition;
    public float speed;
    public float jumpSpeed;
    public LayerMask excludeEnemy;
    Transform tagGround;
    private Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        if (playerPosition.position.x - transform.position.x > 0)
        {
            movement.x = speed;
        }
        if (playerPosition.position.x - transform.position.x < 0)
        {
            movement.x = -speed;
        }
        if (isGrounded() && (Math.Abs(playerPosition.position.x - transform.position.x) < 5) && playerPosition.position.y > transform.position.y && !GameObject.Find("Player").GetComponent<MainPlayerController>().isGrounded()) 
        {
            movement.y = jumpSpeed;
        }
        else
        {
            movement.y = 0;
        }
        myRigidBody.velocity = movement;
    }
    bool isGrounded()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        if (Physics2D.Linecast(transform.position, tagGround.position, excludeEnemy))
        {
            return true;
        }
        else
            return false;

    }
}
