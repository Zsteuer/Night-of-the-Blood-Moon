using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTutorial : MonoBehaviour
{
    public bool isColliding;
    public GameObject UseDoor;
    public GameObject TextPrompt;
    public GameObject TextBackground;
    // Start is called before the first frame update
    void Start()
    {
        UseDoor.SetActive(false);
        TextPrompt.SetActive(false);
        TextBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDoor();
    }

    void CheckDoor()
    {
        if ((Input.GetKeyDown(KeyCode.W) && isColliding) && KeyChain.TutorialKey == false)
        {
                TextBackground.SetActive(true);
                TextPrompt.SetActive(true);
                StartCoroutine("WaitLocked");
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

    IEnumerator WaitLocked()
    {
        yield return new WaitForSeconds(3);
        TextPrompt.SetActive(false);
        TextBackground.SetActive(false);
    }
}
