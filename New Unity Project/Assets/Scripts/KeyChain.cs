using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChain : MonoBehaviour
{
    public static bool TutorialKey;
    public static bool SecondDungeonKey;
    // Start is called before the first frame update
    void Start()
    {
        TutorialKey = false;
        SecondDungeonKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TutorialKeyPickUp()
    {
        TutorialKey = true;
       
    }
    public static void SecondDungeonKeyPickUp()
    {
        SecondDungeonKey = true; 
    }
}
