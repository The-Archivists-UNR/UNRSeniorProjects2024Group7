using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyPanelMgr : MonoBehaviour
{

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;

    string[] stringPrices = {"50", "100", "150", "200", "250"};
    int[] prices = {50, 100, 150, 200, 250};


    int maxAttack = 0;
    public AttackImprovement attackImprovement;

    public void UpdateAttackStatText()
    {
        if (maxAttack == 0)
        {
            attackText.text = stringPrices[0];
        }
    }
}
