using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerStats
{
    public int lives;
    public int maxLives;
    private GameObject healthPanel;
    private UIHealthPanel myUIHealthPanel;
   // public LayerMask enemy;
    // Start is called before the first frame update
    void Start()
    {
        healthPanel = GameObject.Find("HealthPanel");
        myUIHealthPanel = healthPanel.GetComponent<UIHealthPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        myUIHealthPanel.SetLives(maxLives, lives);
    }
}
