using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject uiObject;
    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            uiObject.SetActive(true);
            StartCoroutine("Wait");
        }
    }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(5);
            Destroy(uiObject);
            Destroy(gameObject);
        }
    }


