using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : PlayerStats
{
    public float speed;
    public float jumpSpeed;
    public float isGroundedDistance;
    public Transform tagGround;
    public Animator myAnimator;
    public SpriteRenderer mySpriteRenderer;
    public LayerMask excludePlayer;
    private Rigidbody2D myRigidBody;
    private float jumpHeight;
    private float lastY;
    public float fastFallSpeed;
    bool hasFastFalled;
    public AudioSource audioSource;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public AudioClip runSound;
    
       // Start is called before the first frame update
    void Start()
    {
      //  speed += Agility; // Do we want this?
        lastY = transform.position.y; // grabs the y of the last frame
        myRigidBody = GetComponent<Rigidbody2D>();
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myAnimator = GetComponent<Animator>();
        jumpHeight = (jumpSpeed * jumpSpeed) / myRigidBody.gravityScale;
        hasFastFalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
      //  FallCheck();
        if (IsGrounded())
        {
            hasFastFalled = false;
            if (myAnimator.GetBool("IsFalling"))
            {
                if (audioSource != null && landSound != null)
                {
                    audioSource.PlayOneShot(landSound);
                }
            }
            myAnimator.SetBool("IsJumping", false); // jump might set this true if you successfully jump
            myAnimator.SetBool("IsFalling", false); // you can't be falling if you're grounded
            Jump();
        //    myAnimator.SetBool("IsJumping", false);
        }
        else
        {
            myAnimator.SetBool("IsFalling", false); // fall check might set this to true if you are falling
            FallCheck();
            FastFall();
        }
        lastY = transform.position.y; // grabs the position of y for every frame
      /*  if (IsGrounded() && !myAnimator.GetBool("IsFalling"))
        {
            Debug.Log("this might help");
        } */
    }

    void Move()
    {
        Vector2 movement = Vector2.zero;
        movement.y = myRigidBody.velocity.y;
        myAnimator.SetFloat("Speed", 0); 
        if (Input.GetKey(KeyCode.A))
        {
            //move left
            movement.x = -speed;
            myAnimator.SetFloat("Speed", speed);
            mySpriteRenderer.flipX = true;

        }
        if (Input.GetKey(KeyCode.D))
        {
            //move right
            movement.x = speed;
            myAnimator.SetFloat("Speed", speed);
            mySpriteRenderer.flipX = false;

        }
      /*  if (IsGrounded())
        {
            myAnimator.SetBool("IsJumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        } */
        myRigidBody.velocity = movement;
        }

    void FastFall()
    {
        if (Input.GetKeyDown(KeyCode.S) && !hasFastFalled)
        {
            Vector2 movement = myRigidBody.velocity;
            hasFastFalled = true;
            movement.y = -fastFallSpeed;
            myRigidBody.velocity = movement;
        }
    }
    void FallCheck() // Work on this
    {
        /*    if (myRigidBody.velocity.y < -0.1)
            {
                myAnimator.SetBool("IsFalling" , true);
            }
            else
            {
                myAnimator.SetBool("IsFalling" , false);
            } */
            
        if (transform.position.y < lastY || myRigidBody.velocity.y < 0)
        {
            myAnimator.SetBool("IsFalling", true);
            myAnimator.SetBool("IsJumping", false); // you can't be jumping and falling at the same time
        }
        else
        {
            myAnimator.SetBool("IsFalling", false);
          //  myAnimator.SetBool("IsJumping", true); // this is only called when you aren't grounded, so this should be right
        }
     //   lastY = transform.position.y;
    } 

    void Jump()
    {
        Vector2 jumpMovement = myRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
            jumpMovement.y = jumpSpeed;
            myAnimator.SetBool("IsJumping", true); 
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    /*    else
        {
            myAnimator.SetBool("IsJumping", false);
        } */
        myRigidBody.velocity = jumpMovement;
    }

    public bool IsGrounded()
    {
        Vector2 origin = transform.position;

        if (Physics2D.Linecast(origin, tagGround.position, excludePlayer))
        {
            return true;
        }
        else
        {
           // Debug.Log("this does return false ocassionally");
            return false;
        }
        
    }
}
