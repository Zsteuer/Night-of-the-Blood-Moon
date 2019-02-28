using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject playsTheSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            KeyChain.TutorialKeyPickUp();
            if (playsTheSound != null)
            {
                Instantiate(playsTheSound, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        
    }
}
