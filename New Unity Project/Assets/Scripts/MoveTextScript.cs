using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextScript : MonoBehaviour
{
    public GameObject textBackground;
    // Start is called before the first frame update
    void Start()
    {
        textBackground.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Destroy(gameObject);
            Destroy(textBackground);
        }
    }
}
