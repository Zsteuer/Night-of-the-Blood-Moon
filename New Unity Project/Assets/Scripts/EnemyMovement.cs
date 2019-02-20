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
    private BoxCollider2D boxCollider;
    private float jumpHeight;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        jumpHeight = (jumpSpeed * jumpSpeed) / 2*myRigidBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     //   Debug.Log("test");
        Vector2 movement = Vector2.zero;
        if (playerPosition.position.x - transform.position.x > 0)
        {
            movement.x = speed;
        }
        if (playerPosition.position.x - transform.position.x < 0)
        {
            movement.x = -speed;
        }
        if (isGrounded() && (Math.Abs(playerPosition.position.x - transform.position.x) < 5) && playerPosition.position.y > transform.position.y && !GameObject.Find("Player").GetComponent<MainPlayerController>().IsGrounded()) 
        {
            movement.y = jumpSpeed;
        }
        else
        {
            movement.y = myRigidBody.velocity.y;
        }
        if (movement.y == 0) // double checks to see if we need to jump over something
        {
            Vector2 bottomOfSprite = transform.position; // this was an attempt to look for collisions with something lower than the midpoint of our sprite, but it didn't work
            bottomOfSprite.y = bottomOfSprite.y - (boxCollider.bounds.size.y/2) + (float) .01;
            RaycastHit2D hit = Physics2D.Raycast(bottomOfSprite, Vector3.right, (float) 0.4, excludeEnemy);
          //  Debug.Log(bottomOfSprite.y);
            if (movement.x < 0)
            {
                hit = Physics2D.Raycast(transform.position, Vector3.left, (float)0.4, excludeEnemy);
            }
            if (hit.collider != null)
            {
                GameObject collidedWith = hit.transform.gameObject;
                float collidedWithHeight = collidedWith.GetComponent<Collider2D>().bounds.size.y;
            //    Debug.Log("collidedWithHeight= " + collidedWithHeight);
            //    Debug.Log("JumpHeight =" + jumpHeight);
            //    Debug.Log("transform.position.y =" + transform.position.y);
           //     Debug.Log("collidedWith.transform.position.y =" + collidedWith.transform.position.y);
                if (transform.position.y + jumpHeight >= collidedWithHeight + collidedWith.transform.position.y && isGrounded())
                {
                   Debug.Log("we do go in here");
                    movement.y = jumpSpeed;
                }
                //   else
                //    {
                //         movement.y = 0;
                //     }
            }
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
   /* void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 movement = Vector2.zero;
        myRigidBody.velocity = movement;
        Debug.Log("bingo");
        GameObject collidedWith = collision.gameObject;
        float collidedWithHeight = collidedWith.GetComponent<Collider2D>().bounds.size.y;
        Debug.Log("collidedWithHeight= " + collidedWithHeight);
        Debug.Log("JumpHeight =" + jumpHeight);
        Debug.Log("transform.position.y =" + transform.position.y);
        Debug.Log("collidedWith.transform.position.y =" + collidedWith.transform.position.y);
        if (transform.position.y + jumpHeight >= collidedWithHeight + collidedWith.transform.position.y && isGrounded())
        {
            Debug.Log("we do go in here");
            movement.y = jumpSpeed;
        }
     //   else
    //    {
   //         movement.y = 0;
   //     }
        myRigidBody.velocity = movement;
    } */
}
