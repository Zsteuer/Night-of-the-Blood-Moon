using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack :  
    {
    public SpriteRenderer mySpriteRenderer;
    public float swordDamageDone = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void SwordAttack()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (mySpriteRenderer.flipX == true)
                {
                    RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.left);
                    if (swordHit.collider.name == "Enemy")
                    {
                        var myScript
                        DamageDone(swordDamageDone);
                    }

                }
                else if (mySpriteRenderer.flipX == false)
                {
                    RaycastHit2D swordHit = Physics2D.Raycast(transform.position, Vector2.right);
                    if (swordHit.collider != null)
                    {
                        DamageDone(swordDamageDone);
                    }
                }
            }

        }
    }

        void DamageDone(int swordDamageDone)
    {
        
    }
}
