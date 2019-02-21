using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public static float Strength = 1.0f;
    public static float Agility = 1.0f;
    public static float Stamina = 1.0f; 
      
    // Update is called once per frame
    void Update()
    {
        
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
