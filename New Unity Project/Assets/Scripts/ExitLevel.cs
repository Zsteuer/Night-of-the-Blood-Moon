using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    int chancesToExit = 0;
    public String nextLevelSceneName;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float xDistance = Math.Abs(transform.position.x - playerTransform.position.x);
        float yDistance = Math.Abs(transform.position.y - playerTransform.position.y);
        if (xDistance <= .2 && yDistance <= .4)
        {
          //  Debug.Log("The player is close to the door " + chancesToExit);
            chancesToExit++;
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene(nextLevelSceneName);
            }
        }
    }
}
