using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : PlayerStats
{
    private float timer;
    public SpriteRenderer mySpriteRenderer;
    public Rigidbody2D myRigidBody;
    public Animator myAnimator;
    public LayerMask excludePlayer;
    public float swordDamageDone = 1.0f;
    public float attackLength;
    private bool firstTime; // this keeps you from waiting the first time
    public bool isAttackingHere;
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip enemyHitClip;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        timer = 0;
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
        timer += Time.deltaTime;
        isAttackingHere = myAnimator.GetCurrentAnimatorStateInfo(0).IsName("AttackCycle");
    }

    
    void SwordAttack() 
    {
        
        if (Input.GetKeyDown(KeyCode.J) && (timer >= attackLength || firstTime))
        {
            Debug.Log("Strength = " + Strength);
           if (audioSource != null && attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
            if (mySpriteRenderer.flipX == true)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, excludePlayer);
                if (swordHit.collider != null && swordHit.collider.tag == "Enemy")
                {
                    Debug.Log("We got him");
                    if (audioSource != null && enemyHitClip != null)
                    {
                        audioSource.PlayOneShot(enemyHitClip);
                    } 
                    GameObject target = swordHit.collider.gameObject;
                    target.GetComponentInParent<EnemyHealth>().DamageDone(swordDamageDone * Strength);
                }
                myAnimator.SetTrigger("IsAttacking");
            }
            else if (mySpriteRenderer.flipX == false)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, excludePlayer);
                if (swordHit.collider != null && swordHit.collider.tag == "Enemy")
                {
                    Debug.Log("We got him");
                    if (audioSource != null && enemyHitClip != null)
                    {
                        audioSource.PlayOneShot(enemyHitClip);
                    } 
                    GameObject target = swordHit.collider.gameObject;
                    target.GetComponentInParent<EnemyHealth>().DamageDone(swordDamageDone * Strength);
                }
                  myAnimator.SetTrigger("IsAttacking");
            }
            timer = 0;
            firstTime = false;
        }
                       
    }
 }
