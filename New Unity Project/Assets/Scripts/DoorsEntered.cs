using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsEntered : MonoBehaviour
{
    public static bool foundDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        if (foundDoor == true)
        {
            Vector3 newPos = transform.position;
            newPos.x += 12.5f;
            transform.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
