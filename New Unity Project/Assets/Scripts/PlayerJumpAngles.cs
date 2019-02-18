using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAngles : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    public float isGroundedDistance;
    public LayerMask excludePlayer;
    Transform tagGround;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    // public Sprite spriteStill;
    public Sprite[] upSprites;
    public Sprite[] downSprites;
    public Sprite[] leftSprites;
    private int upSpritesCounter = -1;
    private int leftSpritesCounter = -1;
    private int downSpritesCounter = -1;
    private int rightSpritesCounter = -1;
    private float timer = 0;
    // public Sprite spriteRight;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteLeft;
        tagGround = GameObject.Find(this.name + "/Ground").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = rigidBody.velocity;
        timer += Time.deltaTime;
        
        /*   if (vel.y > 0)
           {
               spriteRenderer.flipX = false;
               spriteRenderer.sprite = spriteUp;
           }
           if (vel.y < 0)
           {
               spriteRenderer.flipX = false;
               spriteRenderer.sprite = spriteDown;
           }
           if (vel.x > 0)
           {
               spriteRenderer.flipX = false;
               spriteRenderer.sprite = spriteLeft;
           }
           if (vel.x < 0)
           {
               spriteRenderer.flipX = true;
               spriteRenderer.sprite = spriteLeft;
           } */
        /*   if (vel.y > 0 && (upSpritesCounter == -1 || timer >= (float)0.25))
           {
               timer = 0;
               // spriteRenderer.flipX = false;
               upSpritesCounter++;
               downSpritesCounter = -1;
               leftSpritesCounter = -1;
               rightSpritesCounter = -1;
               if (upSpritesCounter >= upSprites.Length)
               {
                   upSpritesCounter = 0;
               }
               spriteRenderer.sprite = upSprites[upSpritesCounter];
           }
           if (vel.y < 0 && (downSpritesCounter == -1 || timer >= (float)0.25))
           {
               timer = 0;
               //  spriteRenderer.flipX = false;
               downSpritesCounter++;
               upSpritesCounter = -1;
               leftSpritesCounter = -1;
               rightSpritesCounter = -1;
               if (downSpritesCounter >= downSprites.Length)
               {
                   downSpritesCounter = 0;
               }
               spriteRenderer.sprite = downSprites[downSpritesCounter];
           } */
        if (!Physics2D.Linecast(transform.position, tagGround.position, excludePlayer))
        {
            spriteRenderer.sprite = spriteUp;
         //   if (rightSpritesCounter >= 0)
         //   {
         //       spriteRenderer.flipX = true;
        //    }
            rightSpritesCounter = -1;
            leftSpritesCounter = -1;
        } 
        else
        {
            if (vel.x > 0 && (leftSpritesCounter == -1 || timer >= (float)0.25))
            {
                timer = 0;
                spriteRenderer.flipX = false;
                leftSpritesCounter++;
                downSpritesCounter = -1;
                upSpritesCounter = -1;
                rightSpritesCounter = -1;
                if (leftSpritesCounter >= leftSprites.Length)
                {
                    leftSpritesCounter = 0;
                }
                spriteRenderer.sprite = leftSprites[leftSpritesCounter];
            }
            if (vel.x < 0 && (rightSpritesCounter == -1 || timer >= (float)0.25))
            {
                timer = 0;
                spriteRenderer.flipX = true;
                rightSpritesCounter++;
                downSpritesCounter = -1;
                upSpritesCounter = -1;
                leftSpritesCounter = -1;
                if (rightSpritesCounter >= leftSprites.Length)
                {
                    rightSpritesCounter = 0;
                }
                spriteRenderer.sprite = leftSprites[rightSpritesCounter];
            }
            if (vel.y == 0 && vel.x == 0)
            {
                timer = 0;
              /*  if (rightSpritesCounter > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (leftSpritesCounter > 0)
                {
                    spriteRenderer.flipX = false;
                }  */

                spriteRenderer.sprite = spriteLeft;
                //   spriteRenderer.sprite = spriteDown;
                rightSpritesCounter = -1;
               // downSpritesCounter = -1;
                //upSpritesCounter = -1;
                leftSpritesCounter = -1;
            }
        }

    }
}
