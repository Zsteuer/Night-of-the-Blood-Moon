using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UseDoor : MonoBehaviour
{
    public string nextLevelName;
    public bool isColliding = false;

    void Start()
    {
       isColliding = false;   
    }

    void Update()
    {
    EnterDoor();
    }

    void EnterDoor()
    {
    if (Input.GetKeyDown(KeyCode.W) && isColliding){
            SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
           }
    }     
    
    private void OnTriggerEnter2D(Collider2D Player) 
    {
        if (Player.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}

