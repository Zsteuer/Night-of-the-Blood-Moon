using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : PlayerStats
{
    public SpriteRenderer mySpriteRenderer;
    public Animator myAnimator;
    public float swordDamageDone = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
    }

    void SwordAttack()
        //need to constraint this method so that it can only be used intermittently, adjusted by agility stat 
    {
        myAnimator.ResetTrigger("IsAttacking");
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (mySpriteRenderer.flipX == true)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.left);
                if (swordHit.collider.tag == "Enemy")
                {
                    EnemyHealth.DamageDone(swordDamageDone);
                    myAnimator.SetTrigger("IsAttacking");
                }

            }
            else if (mySpriteRenderer.flipX == false)
            {
                RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.right);
                if (swordHit.collider.tag == "Enemy")
                {
                    EnemyHealth.DamageDone(swordDamageDone);
                    myAnimator.SetTrigger("IsAttacking");
                }
            }
        }

    }

 }
