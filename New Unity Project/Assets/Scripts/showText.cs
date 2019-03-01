using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class showText : MonoBehaviour
{
    public GameObject TextPrompt;
    public GameObject TextBackground;  
    // Start is called before the first frame update
    void Start()
    {
        TextPrompt.SetActive(false);
        TextBackground.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            TextBackground.SetActive(true);
            TextPrompt.SetActive(true);
            StartCoroutine("Wait");
        }
    }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            Destroy(TextPrompt);
            TextBackground.SetActive(false);
            Destroy(gameObject);
        }
    }


