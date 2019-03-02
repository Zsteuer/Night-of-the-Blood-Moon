using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPickupScript : PlayerStats
{
    public float amount;
    // public AudioSource audioSource;
    // public AudioClip itemPickup;
    public AudioSource playClip;
    bool playSound;
    bool toggleChange;

    void Start()
    {
        playClip = GetComponent<AudioSource>();
        playSound = false;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && playSound == false)
        {
            playSound = true;
            playClip.Play();                 
            ChangeStrength(amount);
            Destroy(this.gameObject);
            
        }
    }
}

