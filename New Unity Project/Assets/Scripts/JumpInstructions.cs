using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpInstructions : MonoBehaviour
{
    private float timer;
    private Text txt;
    private bool hasHitSpace;
    private bool hasHitJump;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        txt = GetComponent<Text>();
        hasHitSpace = false;
        hasHitJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            hasHitSpace = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            hasHitJump = true;
        }
        if (hasHitSpace && hasHitJump && timer >= 6)
        {
            txt.text = "Tip: you can press S to fall faster in air";
            destroyThis();
        }
        timer += Time.deltaTime;
    }
    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
