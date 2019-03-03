using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrol : MonoBehaviour
{
    public float speed;
    public float ledgeDistance;
    public float aggroDistance;
    public float attackDistance;
    public bool playerHit;
    public LayerMask onlyPlayer;
    public LayerMask excludePlayer;
    public Animator myAnimator;
    private bool movingRight = true;
    private Vector2 sightDirection = Vector2.zero;
    private RaycastHit2D lineOfSight;
    private RaycastHit2D swipeAttack;
    public Transform groundDetection;
    private Transform playerPosition;
    private float timeBtwAttack;
    public float startTimeBtwAttack;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        playerHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        SkeletonWalk();
        SkeletonAlert();
    }

    void SkeletonWalk()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, ledgeDistance, excludePlayer);
        myAnimator.SetFloat("speed", 1);
        if (!groundInfo.collider)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    void SkeletonAlert()
    {

        RaycastHit2D lineOfSightRight = Physics2D.Raycast(transform.position, Vector2.right, aggroDistance, onlyPlayer);
        RaycastHit2D lineOfSightLeft = Physics2D.Raycast(transform.position, Vector2.left, aggroDistance, onlyPlayer);
        Debug.DrawRay(transform.position, sightDirection);

        if (lineOfSightRight.collider != null || lineOfSightLeft.collider != null)
        {
            //aproach player
            playerHit = false;
            SkeletonAttack();
            timeBtwAttack = startTimeBtwAttack;
            myAnimator.SetBool("isAttacking", false);
            Debug.Log("meme activate");
        }
    }




    void SkeletonAttack()
    {
        RaycastHit2D swipeAttackRight = Physics2D.Raycast(transform.position, Vector2.right, attackDistance, onlyPlayer);
        RaycastHit2D swipeAttackLeft = Physics2D.Raycast(transform.position, Vector2.left, attackDistance, onlyPlayer);
        myAnimator.SetBool("isAttacking", true);
        if (swipeAttackRight.collider != null || swipeAttackLeft.collider != null)
        {

            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if (playerHealth.lives > 0)
            {
                playerHealth.lives = 1;

            }

        }
    }

}