using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
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
    private float timer; // times the last time since the player was ahead
    private float lastVel;
    private bool needsToEscape; // if the enemy needs to get out of the way
    private float leftTimer;
    private float rightTimer;
    public Sprite defaultSprite;
    public Sprite attackSprite;
    public Sprite[] walkSprites;
    private int leftMovementCounter = -1;
    private int rightMovementCounter = -1;
    private float movementTimer = 0; // used for motion. How long have we been walking in this direction?
    private float attackTimer = 0; // How long we have been attacking.
    public float attackTime; // How frequent we shoot raycasts during the attack
    Boolean isAttacking; // set true in attack and false in update. Keeps us from adding the Time.deltaTime BEFORE attacking
    public AudioSource audioSource;
    public AudioClip playerHitClip;
    void Start()
    {
        isAttacking = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerPosition = GameObject.Find("Player").transform;
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        jumpHeight = (jumpSpeed * jumpSpeed) / 2*myRigidBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
        playerDetected = true;
        jumpDistance = (jumpHeight / myRigidBody.gravityScale)*speed;
        myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        hasFastFalled = false;
        timer = 0;
        needsToEscape = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            if (isPlayerAhead() || isPlayerBelow() || playerSuperClose()) // both of these detect if the player is in range. PlayerSuperClose should take care of collisions
            {
                movementTimer = 0;
                Vector2 attackMovement = myRigidBody.velocity;
                attackMovement.x = 0; // we still want it to be able to fall, so we don't change y quite yet
                                      //  attackMovement.y = 0; // we let it fast fall in front of the player
                myRigidBody.velocity = attackMovement;
                Attack();
            }
            else
            {
                isAttacking = false;
                movementTimer += Time.deltaTime;
                attackTimer = 0;
                Vector3 currRot = transform.eulerAngles;
                currRot.y = 0; // fixes any rotations from before the player was detected
                transform.eulerAngles = currRot;
                //   Debug.Log("test")
                Vector2 movement = Vector2.zero;
                float previousX = myRigidBody.velocity.x; // We'll use this to help the enemy get out of the way if he's right above or below the villain
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
                    Debug.Log("you're still getting in here somehow");
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
                /*    if ((isPlayerDirectlyAbove() || isPlayerDirectlyBelow()) && movement.x == 0) // this teaches the enemy to get out of the way
                    {
                        movement.x = speed;
                    } */
                if (isGrounded() && isPlayerDirectlyBelow()) // is he on top of the player?
                {
                    movement.y = jumpSpeed; // good that you got him to jump. Problem is that this updates frame by frame
                }
                /*    if (Math.Abs(playerPosition.position.x - transform.position.x) < .5) // keeps the flickering animation thing from happening
                    {
                        movement.x = 0;
                    } */
                if (isPlayerDirectlyAbove() || isPlayerDirectlyBelow())
                {
                    needsToEscape = true;
                }
                if (needsToEscape)
                {
                    Debug.Log("needsToExcape");
                    if (timer < .25)
                    {
                        Debug.Log("still taking time");
                        movement.x = speed;
                        timer += Time.deltaTime;
                    }

                    else
                    {
                        Debug.Log("checking to see if escaped");
                        if (!(isPlayerDirectlyBelow() || isPlayerDirectlyAbove()))
                        {
                            needsToEscape = false;
                            Debug.Log("sucessfully escaped");
                        }
                        timer = 0;
                    }
                }

            myRigidBody.velocity = movement;
                if (playerPosition.position.x > transform.position.x + .1) // taking care of direction
                {
                    spriteRenderer.flipX = false;
                }
                if (playerPosition.position.x < transform.position.x - .1)
                {
                    spriteRenderer.flipX = true;
                }
                if (myRigidBody.velocity.x > 0 && (movementTimer >= .25 || rightMovementCounter == -1))
                {
                    movementTimer = 0;
                    rightMovementCounter++;
                    leftMovementCounter = -1;
                    if (rightMovementCounter >= walkSprites.Length)
                    {
                        rightMovementCounter = 0;
                    }
                    spriteRenderer.sprite = walkSprites[rightMovementCounter];
                }
                if (myRigidBody.velocity.x < 0 && (movementTimer >= .25 || leftMovementCounter == -1))
                {
                    movementTimer = 0;
                    leftMovementCounter++;
                    rightMovementCounter = -1;
                    if (leftMovementCounter >= walkSprites.Length)
                    {
                        leftMovementCounter = 0;
                    }
                    spriteRenderer.sprite = walkSprites[leftMovementCounter];
                }
                if (myRigidBody.velocity.x == 0)
                {
                    leftMovementCounter = -1;
                    rightMovementCounter = -1;
                    spriteRenderer.sprite = defaultSprite;
                }
            }
       
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
        if (isPlayerBelow())
        {
            Debug.Log("Player is below");
        }
        if (isPlayerAhead())
        {
            Debug.Log("Player is ahead");
            Debug.Log(myRigidBody.velocity.x);
        }
        else
        {
            Debug.Log("Player is not ahead");
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
    bool isPlayerBelow()
    {
        Vector3 ahead = transform.position;
        if (myRigidBody.velocity.x > 0)
        {
            ahead.x += (float)1;
        }
        else
        {
            ahead.x -= (float)1;
        }
        RaycastHit2D hit = Physics2D.Raycast(ahead, Vector3.down, (float)100, excludeEnemy); // arbitrarily large number
        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
        //    Debug.Log("Player is below");
            return true;
        }
        return false;
    }
    bool isPlayerAhead()
    {
        Vector3 ahead = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, (float) 1, excludeEnemy);
        if (myRigidBody.velocity.x < 0)
        {
           hit = Physics2D.Raycast(transform.position, Vector3.left, (float)1, excludeEnemy);
        }
      //  RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, (float)100, excludeEnemy); // arbitrarily large number
        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //    Debug.Log("Player is below");
            return true;
        }
        return false;
    }
    bool isPlayerDirectlyBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, (float)100, excludeEnemy);
        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
                Debug.Log("Player is directly");
            return true;
        }
        return false;
    }
    bool isPlayerDirectlyAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, (float)100, excludeEnemy);
        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //    Debug.Log("Player is below");
            return true;
        }
        return false;
 //   }
}
    bool somethingOnYourRight() // not used
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, (float).01, excludeEnemy);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    bool somethingOnYourLeft() // not used
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left, (float).01, excludeEnemy);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    bool playerSuperClose()
    {
        float yDistance = playerPosition.position.y - transform.position.y;
        float xDistance = playerPosition.position.x - transform.position.x;
        float square = xDistance * xDistance + yDistance * yDistance;
        float sqrt = (float) Math.Sqrt(square);
        return (sqrt < .75);
    }
    //}
    void Attack()
    {
        spriteRenderer.sprite = attackSprite;
        if (attackTimer > attackTime)
        {
            Debug.Log("Bang!");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, (float)1, excludeEnemy);
            if (playerPosition.position.x < transform.position.x - .01)
            {
                hit = Physics2D.Raycast(transform.position, Vector3.left, (float)1, excludeEnemy);
            }
            if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (audioSource != null && playerHitClip != null)
                {
                    audioSource.PlayOneShot(playerHitClip);
                }
                PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
                if (playerHealth.lives > 0)
                {
                    playerHealth.lives--;
                }
            }
            attackTimer = 0;
        }
        else
        {
         //   Debug.Log("attackTimer = " + attackTimer + ". attackTime =" + attackTime + ".");
        }
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
        }
        isAttacking = true;
        Debug.Log("this is where the enemy would be attacking if we finished this part of the script");
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
  /*  void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 movement = myRigidBody.velocity;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movement.x = 0;
        }
        myRigidBody.velocity = movement;
    } */
}
