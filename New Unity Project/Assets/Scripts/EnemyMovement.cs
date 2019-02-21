using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerPosition;
    public Boolean playerDetected;
    public float speed;
    public float jumpSpeed;
    public LayerMask excludeEnemy;
    //public LayerMask excludeEnemyAndPlayer;
    Transform tagGround;
    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider;
    private float jumpHeight;
    private float myWidth;
    private float myHeight;
    private float jumpDistance; // actually just the distance to the tallest point
    // Start is called before the first frame update
    public float fastFallSpeed;
    bool hasFastFalled;
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        jumpHeight = (jumpSpeed * jumpSpeed) / 2*myRigidBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
        playerDetected = true;
        jumpDistance = (jumpHeight / myRigidBody.gravityScale)*speed;
        myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        hasFastFalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            Vector3 currRot = transform.eulerAngles;
            currRot.y = 0; // fixes any rotations from before the player was detected
            transform.eulerAngles = currRot;
            //   Debug.Log("test")
            Vector2 movement = Vector2.zero;
          /*  if (isGrounded())
            {
                hasFastFalled = false;
            }
            else
            {
                if (playerPosition.position.y < transform.position.y - 5 && Math.Abs(playerPosition.position.x - transform.position.x) < 5)
                {
                    FastFall();
                }
            } */
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
                //  Debug.Log("this is what's happenening");
                movement.y = jumpSpeed;
            }
            else
            {
                movement.y = myRigidBody.velocity.y;
            }
            if (movement.y == 0) // double checks to see if we need to jump over something
            {
                Vector2 bottomOfSprite = transform.position; // this was an attempt to look for collisions with something lower than the midpoint of our sprite, but it didn't work
                bottomOfSprite.y = bottomOfSprite.y - (boxCollider.bounds.size.y / 2) + (float).01;
                RaycastHit2D hit = Physics2D.Raycast(bottomOfSprite, Vector3.right, (float)0.4, excludeEnemy);
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
                    Debug.Log("but we do go in here");
                    if (transform.position.y + jumpHeight >= collidedWithHeight + collidedWith.transform.position.y && isGrounded() // if we can jump over it 
                      && !(collidedWith.layer == LayerMask.NameToLayer("Player") || (collidedWith.transform.parent != null && collidedWith.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player")))
                        ) // if it isn't the player or a child of the player
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
        else // Code in this clause adopted from https://www.devination.com/2015/07/unity-2d-platformer-tutorial-part-4.html
        {
            if (!noDitchNearby() || isBlocked())
            {
                Vector3 currRot = transform.eulerAngles;
                currRot.y += 180;
                transform.eulerAngles = currRot;
            }

            //Always move forward
            Vector2 myVel = myRigidBody.velocity;
            myVel.x = transform.right.x * speed;
            myRigidBody.velocity = myVel; 
        }
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
    void FastFall()
    {
        if (!hasFastFalled)
        {
            Vector2 movement = myRigidBody.velocity;
            hasFastFalled = true;
            movement.y -= fastFallSpeed;
            myRigidBody.velocity = movement;
        }
    }
    /* Vector2 toVector2(this Vector3 vec3) // credit: https://www.devination.com/2015/07/unity-extension-method-tutorial.html
    {
        return new Vector2(vec3.x, vec3.y);
    } */
    bool noDitchNearby() // adopted from https://www.devination.com/2015/07/unity-2d-platformer-tutorial-part-4.html
    {
       // Vector2 bottomOfSprite = transform.position; // this was an attempt to look for collisions with something lower than the midpoint of our sprite, but it didn't work
     //   bottomOfSprite.y = bottomOfSprite.y - (boxCollider.bounds.size.y / 2) + (float).01;
        Vector2 lineCastPos = transform.position;
        lineCastPos.x += transform.right.x * (float) .1;
     //  lineCastPos.x = transform.position.x - transform.right.x * myWidth + Vector2.up.x * myHeight;
     //  lineCastPos.y = transform.position.y - transform.right.y * myWidth + Vector2.up.y * myHeight;
      //  Vector2 lineCastPos = transform.position - transform.right.toVector2() * myWidth + Vector2.up * myHeight;
        if (Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, excludeEnemy)){
            return true;
        }
        Debug.Log("There's a ditch");
        return false;
    }
    bool isBlocked() // adopted from https://www.devination.com/2015/07/unity-2d-platformer-tutorial-part-4.html
    {
        Vector2 bottomOfSprite = transform.position; // this was an attempt to look for collisions with something lower than the midpoint of our sprite, but it didn't work
        bottomOfSprite.y = bottomOfSprite.y - (boxCollider.bounds.size.y / 2) + (float).01;
        // Vector2 lineCastPos = Vector2.zero;
        //lineCastPos.x = transform.position.x - transform.right.x * myWidth + Vector2.up.x * myHeight;
        // lineCastPos.y = transform.position.y - transform.right.y * myWidth + Vector2.up.y * myHeight;
        // Vector2 dir = lineCastPos;
        // dir.x = dir.x - transform.right.x*.1f;
        //  dir.y = dir.y - transform.right.y*.1f;
        Vector2 dir = bottomOfSprite;
        dir.x = bottomOfSprite.x + transform.right.x * (float).5;
        if (Physics2D.Linecast(bottomOfSprite, dir, excludeEnemy))
        {
            return true;
            Debug.Log("he's blocked");
        }
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
