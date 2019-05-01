using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictorySceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            resetStaticVars();
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }
    }
    void resetStaticVars()
    {
        EnemyHealth.tutorialEnemyKilled = false;
        KeyChain.TutorialKey = false;
        KeyChain.SecondDungeonKey = false;
        PlayerStats.Strength = 25f;
        DoorsEntered.foundDoor = false;
    }
}
