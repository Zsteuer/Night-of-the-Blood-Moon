using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class UseDoor : MonoBehaviour
{
    public struct Parameters
    {
        public string nextLevelName;
        public bool isColliding;

        public void InputName(string levelName, bool collide)
        {
            nextLevelName = levelName;
            isColliding = collide; 
        }
    }

    Parameters para = new Parameters();
    void Start()
    {
        para.isColliding = false;   
    }

    void Update()
    {
    EnterDoor();
    }

    void EnterDoor()
    {
    if (Input.GetKeyDown(KeyCode.W) && para.isColliding){
            SceneManager.LoadScene(para.nextLevelName, LoadSceneMode.Single);
           }
    }     
    
    private void OnCollisionEnter2D(Collision2D Player) 
    {
        para.isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D Player)
    {
        para.isColliding = false;
    }
}

