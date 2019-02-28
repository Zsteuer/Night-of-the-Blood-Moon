using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private bool isHit = false; // stops bump from when it detects the player twice
    // Start is called before the first frame update
   // public AudioSource audioSource;
   // public AudioClip itemPickup;
    public GameObject playsTheSound;
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
            if (playerHealth.lives < playerHealth.maxLives && !isHit)
            {
                playerHealth.lives++;
                /*    if (audioSource != null && itemPickup != null)
                    {
                        audioSource.PlayOneShot(itemPickup);
                    } */
                if (playsTheSound != null)
                {
                    Instantiate(playsTheSound, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
                isHit = true;
            }
        }
    }
}
