using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthImprovement : MonoBehaviour
{
    float[] healthArray = { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    string[] healthTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int healthCounter;

    public TextMeshProUGUI healthText;
    public OpheliaStats oStats;
    public HealthImprovement otherButton;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = healthTextArray[healthCounter];

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth()
    {
        if (healthCounter > 0)
        {
            float healthBuff = 1 + healthArray[healthCounter];
            oStats.HpPercent = healthBuff;
        }
        if (healthCounter == 0)
        {
            oStats.HpPercent = 1;
        }
        healthText.text = healthTextArray[healthCounter];
    }

    public void Increase()
    {
        if (healthCounter < 5)
        {
            healthCounter++;
            otherButton.healthCounter++;
        }
    }

    public void Decrease()
    {
        if (healthCounter > 0)
        {
            healthCounter--;
            otherButton.healthCounter--;
        }
    }
}
