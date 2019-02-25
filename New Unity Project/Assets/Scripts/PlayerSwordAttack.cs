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
  //  AnimatorClipInfo[] CurrentClipInfo;
    public bool isAttackingHere;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        timer = 0;
        firstTime = true;
      //  myAnimator = gameObject.GetComponent<Animator>();
    //    CurrentClipInfo = this.myAnimator.GetCurrentAnimatorClipInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
        timer += Time.deltaTime;
        //    if (CurrentClipInfo[0].clip.name;
        isAttackingHere = myAnimator.GetCurrentAnimatorStateInfo(0).IsName("AttackCycle");
    }

    
    void SwordAttack()
        //need to constraint this method so that it can only be used intermittently, adjusted by agility stat 
    {
        
        if (Input.GetKeyDown(KeyCode.J) && (timer >= attackLength || firstTime))
        {
            Debug.Log("Strength = " + Strength);
           // ChangeStrength(1);
            if (mySpriteRenderer.flipX == true)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, excludePlayer);
                if (swordHit.collider != null && swordHit.collider.tag == "Enemy")
                {
                    Debug.Log("We got him");
                    //     EnemyHealth.DamageDone(swordDamageDone * Strength); // I changed this to pass in Strength
                    GameObject target = swordHit.collider.gameObject;
                    target.GetComponentInParent<EnemyHealth>().DamageDone(swordDamageDone * Strength);
                //    GameObject.Find("Enemy").GetComponent<EnemyHealth>().DamageDone(swordDamageDone * Strength); // you need to change this line because it won't work for more than one enemy.
                }
                myAnimator.SetTrigger("IsAttacking");
            }
            else if (mySpriteRenderer.flipX == false)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, excludePlayer);
                if (swordHit.collider != null && swordHit.collider.tag == "Enemy")
                {
                    Debug.Log("We got him");
                    GameObject target = swordHit.collider.gameObject;
                    target.GetComponentInParent<EnemyHealth>().DamageDone(swordDamageDone * Strength);
                    // EnemyHealth.DamageDone(swordDamageDone*Strength); // I changed this to pass in Strength
                   // GameObject.Find("Enemy").GetComponent<EnemyHealth>().DamageDone(swordDamageDone * Strength); // you need to change this line because it won't work for more than one enemy.
                }
                  myAnimator.SetTrigger("IsAttacking");
            }
            timer = 0;
            firstTime = false;
        }
                       
    }
 }
