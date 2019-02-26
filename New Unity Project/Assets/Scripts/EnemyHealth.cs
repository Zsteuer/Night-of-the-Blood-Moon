using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EnemyHealth : PlayerSwordAttack // Zach: We should come back to this and make this extend MonoBehavior. The only reason I'm not is because PlayerSwordAttack calls it right now, which I'm pretty sure is not what you meant
public class EnemyHealth : MonoBehaviour
{
    // public static float enemyHealth = 100.0f; // Zach: Do we want this static? Because if it's static all enemies will have the same health.
      public float enemyHealth;
      public GameObject theExplosion;
    private float spriteBlinkingFrameTimer = 0.0f; // https://answers.unity.com/questions/1134985/sprite-blinking-effect-when-player-hit.html
    private float spriteBlinkingFrameDuration = 0.1f;
    private float spriteBrinkingTotalTimer = 0.0f;
    private float spriteBlinkingTotalDuration = 0.5f;
    public bool isBlinking = false;
    // Another issue that I've found is that the enemy doesn't respawn when you die.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth == 0) // destroys the enemy when he dies
        {
            StartCoroutine(Die());
        }
        else if (isBlinking)
        {
            SpriteBlink();
        }
    }

    //public static void DamageDone(float swordDamageDone)
    public void DamageDone(float swordDamageDone)
    {
        /*  enemyHealth -= (swordDamageDone * Strength);
         *  // Strength multiplier should be added here alongside sword damage to collided entity 
         *  Zach (Replying to Jason's code): interesting, but I feel like swordDamageDone*Strength should be calculated in the PlayerSwordAttack script and passed into this one. I would still do:
         *  */
        enemyHealth -= swordDamageDone;
        isBlinking = true;
        spriteBlinkingFrameTimer = 0;
        spriteBrinkingTotalTimer = 0;
    }
    IEnumerator Die() // we will edit this to give animations and whatnot
    {
        yield return new WaitForSeconds(0);
        Instantiate(theExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void SpriteBlink()
    {
        spriteBrinkingTotalTimer += Time.deltaTime;
        if (spriteBrinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            isBlinking = false;
            spriteBrinkingTotalTimer = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            spriteBlinkingFrameTimer += Time.deltaTime;
            if (spriteBlinkingFrameTimer >= spriteBlinkingFrameDuration)
            {
                spriteBlinkingFrameTimer = 0;
                if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
}
