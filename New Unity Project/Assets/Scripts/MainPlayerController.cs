using System.Collections;
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
    private float jumpHeight;


       // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        tagGround = GameObject.Find(this.name + "/Ground").transform;
        myAnimator = GetComponent<Animator>();
        jumpHeight = (jumpSpeed * jumpSpeed) / myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FallCheck();
        if (IsGrounded())
        {
            Jump();
        //    myAnimator.SetBool("IsJumping", false);
        } 
    }

    void Move()
    {
        Vector2 movement = Vector2.zero;
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

   /* void SwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (mySpriteRenderer.flipX == true)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.left);
                if (swordHit.collider != null)
                {

                }

            }
            else if (mySpriteRenderer.flipX == false)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.right);
                if (swordHit.collider != null)
                {

                }
            }
        }
    
    } */
        
    void FallCheck() // Work on this
    {
        if (myRigidBody.velocity.y < -0.1)
        {
            myAnimator.SetBool("IsFalling" , true);
        }
        else
        {
            myAnimator.SetBool("IsFalling" , false);
        }
    }

    void Jump()
    {
        Vector2 jumpMovement = myRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
            jumpMovement.y = jumpSpeed;
            myAnimator.SetBool("IsJumping", true); 
        }
       /* else
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
            Debug.Log("this does return false ocassionally");
            return false;
        }
        
    }
}
