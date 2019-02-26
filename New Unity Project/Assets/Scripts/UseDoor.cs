using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UseDoor : MonoBehaviour
{
    public string nextLevelName;
    private bool colliding = false;
    void Update()
    {
    EnterDoor();
    }

    void EnterDoor()
    {
    while (colliding)
    {
        if (Input.GetKeyDown(KeyCode.W)){
            SceneManager.LoadScene(nextLevelName);
        }
    }     
    }
    private void OnCollisionEnter2D(Collider2D Player) 
    {
        colliding = true;
      }

    private void OnCollisionExit2D(Collider2D Player)
    {
        colliding = false;
    }
}
