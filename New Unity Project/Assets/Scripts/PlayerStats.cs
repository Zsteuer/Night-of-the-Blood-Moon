using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public static float Strength = 25.0f;

    
   public static void ChangeStrength(float amount)
    {
         Strength += amount;
    }

}
