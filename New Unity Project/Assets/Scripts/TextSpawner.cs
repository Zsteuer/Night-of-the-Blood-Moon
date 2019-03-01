using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawner : EnemyHealth
{
    // Start is called before the first frame update
    bool done;
    void Start()
    {
        GetComponent<Text>().enabled = false;
        done = false;
     }

    // Update is called once per frame
    void Update()
    {
        if (tutorialEnemyKilled && !done) // https://answers.unity.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
        {
            StartCoroutine(Appear());
        }
    }

    IEnumerator Appear()
    {
        done = true;
        GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(4);
        GetComponent<Text>().enabled = false;
        Destroy(gameObject);
    }
}
