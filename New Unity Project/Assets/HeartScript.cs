using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("The heart is detecting the player");
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if (playerHealth.lives < playerHealth.maxLives)
            {
                playerHealth.lives++;
                Destroy(gameObject);
            }
        }
    }
}
