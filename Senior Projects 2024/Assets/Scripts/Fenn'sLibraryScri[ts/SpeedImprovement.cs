using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedImprovement : MonoBehaviour
{
    float[] speedArray = { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    string[] speedTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int speedCounter;

    public TextMeshProUGUI speedText;
    public OpheliaStats oStats;
    public SpeedImprovement otherButton;

    // Start is called before the first frame update
    void Start()
    {
        speedText.text = speedTextArray[speedCounter];

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSpeed()
    {
        if (speedCounter > 0)
        {
            float speedBuff = 1 + speedArray[speedCounter];
            oStats.SpeedPercent = speedBuff;
        }
        if (speedCounter == 0)
        {
            oStats.SpeedPercent = 1;
        }
        speedText.text = speedTextArray[speedCounter];
    }

    public void Increase()
    {
        if (speedCounter < 5)
        {
            speedCounter++;
            otherButton.speedCounter++;
        }
    }

    public void Decrease()
    {
        if (speedCounter > 0)
        {
            speedCounter--;
            otherButton.speedCounter--;
        }
    }
}
