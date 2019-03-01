using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorSecondDungeon : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.W) && isColliding && KeyChain.SecondDungeonKey == false)
        {
            TextBackground.SetActive(true);
            TextPrompt.SetActive(true);
            StartCoroutine("Wait");
        }

        if ((Input.GetKeyDown(KeyCode.W) && isColliding) && KeyChain.SecondDungeonKey == true)
        {
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        TextPrompt.SetActive(false);
        TextBackground.SetActive(false);
    }
}
