using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawner : EnemyHealth
{
    // Start is called before the first frame update
    bool done;
    public GameObject textBackground;
    void Start()
    {
        GetComponent<Text>().enabled = false;
        done = false;
        textBackground.SetActive(false);
     }

    // Update is called once per frame
    void Update()
    {
        if (tutorialEnemyKilled && !done) // https://answers.unity.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
        {
            textBackground.SetActive(true);
            GetComponent<Text>().enabled = true;
            StartCoroutine(Appear());
        }
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(4);
        done = true;
        textBackground.SetActive(false);
        GetComponent<Text>().enabled = false;
        Destroy(gameObject);
    }
}
