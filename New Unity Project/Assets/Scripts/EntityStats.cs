using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : EnemyAttack
{
    public static float Health = 100.0f;   
    public static float Strength = 1.0f;
    public static float Agility = 1.0f;
    public static float Stamina = 1.0f; 
      
    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DamageDone(float swordDamageDone)
    { 
        Health = Health - (swordDamageDone * Strength);
        // Strength multiplier should be added here alongside sword damage to collided entity 
    }

    void ChangeHealth(float amount)
    {
        Health = Health + amount;
    }
    
    void ChangeStrength(float amount)
    {
        Strength = Strength + amount;
    }

    void ChangeAgility(float amount)
    {
        Agility = Agility + amount;
    }

    void ChangeStamina(float amount)
    {
        Stamina = Stamina + amount;
    }
}
