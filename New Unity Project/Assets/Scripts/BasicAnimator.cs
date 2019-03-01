using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    private float timer;
    public Sprite otherSprite;
    public Sprite thisSprite;
    private bool thisSpriteBool;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteBool = true;
        spriteRenderer.sprite = thisSprite;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            timer = 0;
            if (thisSpriteBool)
            {
                thisSpriteBool = false;
                spriteRenderer.sprite = otherSprite;
            }
            else
            {
                thisSpriteBool = true;
                spriteRenderer.sprite = thisSprite;
            }
        }
    }
}
