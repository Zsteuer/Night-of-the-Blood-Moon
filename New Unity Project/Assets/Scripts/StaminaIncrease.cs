using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaIncrease : MonoBehaviour
{
    private bool isHit = false;
    public AudioSource audioSource;
    public AudioClip itemPickup;

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
            Debug.Log("The stamina heart is detecting the player");
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            int curLives = playerHealth.lives;
            if (playerHealth.maxLives < 6 && !isHit)
            {
                playerHealth.maxLives = playerHealth.maxLives + 1;
            }
            if (playerHealth.lives < playerHealth.maxLives && !isHit)
            {
                playerHealth.lives = playerHealth.lives + 1; ;
                Destroy(gameObject);
            } 
            if (curLives < 6) // if it did either of those things
            {
                isHit = true;
                if (audioSource != null && itemPickup != null)
                {
                    audioSource.PlayOneShot(itemPickup);
                }
                Destroy(gameObject);
            }
        }
    }
}
