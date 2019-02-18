using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody; 
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            //move left
            movement.x = -speed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            //move right
            movement.x = speed;
        }

        myRigidBody.velocity = movement;
    }
    
}
