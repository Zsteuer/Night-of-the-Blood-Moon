using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChain : MonoBehaviour
{
    public static bool TutorialKey;
    // Start is called before the first frame update
    void Start()
    {
        TutorialKey = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TutorialKeyPickUp()
    {
        TutorialKey = true;
    }
}
