using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public static float Strength = 25.0f;
    public static float Agility = 1.0f;
    public static float Stamina = 1.0f;  


    // Update is called once per frame
    void Update()
    {
   //     ChangeStrength(Time.deltaTime);
    }

    
    void ChangeStrength(float amount)
    {
         Strength += amount;
    }

    void ChangeAgility(float amount)
    {
         Agility += amount;
    }

    void ChangeStamina(float amount)
    {
          Stamina += amount;
    }
}
