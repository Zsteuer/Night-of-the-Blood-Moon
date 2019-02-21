using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : PlayerSwordAttack
{
    public static float enemyHealth = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DamageDone(float swordDamageDone)
    {
        enemyHealth -= (swordDamageDone * Strength);
        // Strength multiplier should be added here alongside sword damage to collided entity 
    }
}
