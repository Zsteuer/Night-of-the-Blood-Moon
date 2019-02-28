using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPickupScript : PlayerStats
{
    private bool isHit = false;
    public float amount;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isHit)
        {
            Debug.Log("strengh = " + Strength);
            isHit = true;
            if (audioSource != null && itemPickup != null)
            {
                audioSource.PlayOneShot(itemPickup);
            }
            ChangeStrength(amount);
            Debug.Log("strengh = " + Strength);
            Destroy(gameObject);
        }

    }
}

