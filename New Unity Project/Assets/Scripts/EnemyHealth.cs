using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : PlayerSwordAttack // Zach: We should come back to this and make this extend MonoBehavior. The only reason I'm not is because PlayerSwordAttack calls it right now, which I'm pretty sure is not what you meant
{
    public static float enemyHealth = 100.0f; // Zach: Do we want this static? Because if it's static all enemies will have the same health.
    // Another issue that I've found is that the enemy doesn't respawn when you die.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth == 0) // destroys the enemy when he dies
        {
            StartCoroutine(Die());
        }
    }

    public static void DamageDone(float swordDamageDone)
    {
        /*  enemyHealth -= (swordDamageDone * Strength);
         *  // Strength multiplier should be added here alongside sword damage to collided entity 
         *  Zach (Replying to Jason's code): interesting, but I feel like swordDamageDone*Strength should be calculated in the PlayerSwordAttack script and passed into this one. I would still do:
         *  */
        enemyHealth -= swordDamageDone;
    }
    IEnumerator Die() // we will edit this to give animations and whatnot
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
