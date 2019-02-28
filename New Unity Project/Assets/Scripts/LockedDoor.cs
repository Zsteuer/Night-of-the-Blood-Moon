using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool isColliding;
    public GameObject UseDoor;
    // Start is called before the first frame update
    void Start()
    {
        UseDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDoor();
    }

    void CheckDoor()
    {
        if (Input.GetKeyDown(KeyCode.W) && isColliding && KeyChain.TutorialKey == false)
        {
            print("sowwy");
        }

        if((Input.GetKeyDown(KeyCode.W) && isColliding) && KeyChain.TutorialKey == true){
            UseDoor.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}
