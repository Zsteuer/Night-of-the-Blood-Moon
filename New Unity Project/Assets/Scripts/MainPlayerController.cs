using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float isGroundedDistance;
    public LayerMask excludePlayer; 
    private Rigidbody2D myRigidBody;
    Transform tagGround;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        tagGround = GameObject.Find(this.name + "/Ground").transform;

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
    bool isGrounded()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        if (Physics2D.Linecast(transform.position, tagGround.position, excludePlayer))
        {
            return true;
        } else
            return false;
        
    }
}
