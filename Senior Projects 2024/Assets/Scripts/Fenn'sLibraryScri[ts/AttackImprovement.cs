using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Author: Fenn Edmonds

public class AttackImprovement : MonoBehaviour
{
    float[] attackArray = {0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f};
    string[] attackTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int attackCounter;

    public TextMeshProUGUI attackText;
    public OpheliaStats oStats;
    public AttackImprovement otherButton;

    // Start is called before the first frame update
    void Start()
    {
        attackText.text = attackTextArray[attackCounter];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAttack()
    {
        if (attackCounter > 0)
        {
            float attackBuff = 1 + attackArray[attackCounter];
            oStats.AttackPercent = attackBuff;
        }
        if (attackCounter == 0)
        {
            oStats.AttackPercent = 1;
        }
        attackText.text = attackTextArray[attackCounter];
    }

    public void Increase()
    {
        if (attackCounter < 5)
        {
            attackCounter++;
            otherButton.attackCounter++;
        }
    }

    public void Decrease()
    {
        if (attackCounter > 0)
        {
            attackCounter--;
            otherButton.attackCounter--;
        }
    }
}
