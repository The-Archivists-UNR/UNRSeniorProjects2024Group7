using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Author: Fenn Edmonds

public class AttackImprovement : MonoBehaviour
{
    public static AttackImprovement inst;
    
    float[] attackArray = {0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f};
    string[] attackTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int attackCounter;

    public TextMeshProUGUI attackText;
    public OpheliaStats oStats;

    public TextMeshProUGUI attackPricetext;

    public moneyMgr mMgr;

    int attackPrice = 50;
    string[] stringPrices = {"50", "100", "150", "200", "250", "Maxed"};
    int[] prices = {50, 100, 150, 200, 250, 0};

    public int maxBuff = 0;

    bool firstMax = false;
    bool secondMax = false;
    bool thirdMax = false;
    bool fourthMax = false;
    bool fifthMax = false;

    public CoinTextUpdator coinText;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        attackText.text = attackTextArray[attackCounter];
        attackPricetext.text = stringPrices[maxBuff];
        attackPrice = prices[maxBuff];
        
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
            OpheliaStats.inst.AttackPercent = attackBuff;
        }
        if (attackCounter == 0)
        {
            OpheliaStats.inst.AttackPercent = 1;
        }
        attackText.text = attackTextArray[attackCounter];
        
        // print(attackPrice);
        // coinText.UpdateMoneyText();
    }

    public void Increase()
    {
        if (attackCounter < maxBuff)
        {
            attackCounter++;
        }
    }

    public void CheckMax()
    {
        if (mMgr.currency >= attackPrice && firstMax == false && attackCounter == 0)
        {
            print("I have increased");
            mMgr.currency = mMgr.currency - attackPrice;
            maxBuff++;
            attackPricetext.text = stringPrices[maxBuff];
            attackPrice = prices[maxBuff];
            firstMax = true;
            coinText.UpdateMoneyText();
            // firstMax = true;
            print(firstMax);
        }

        if (mMgr.currency >= attackPrice && firstMax == true && secondMax == false && attackCounter == 1)
        {
            mMgr.currency = mMgr.currency - attackPrice;
            maxBuff++;
            attackPricetext.text = stringPrices[maxBuff];
            attackPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            secondMax = true;
        }

        if (mMgr.currency >= attackPrice && secondMax == true && thirdMax == false && attackCounter == 2)
        {
            mMgr.currency = mMgr.currency - attackPrice;
            maxBuff++;
            attackPricetext.text = stringPrices[maxBuff];
            attackPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            thirdMax = true;
        }

        if (mMgr.currency >= attackPrice && thirdMax == true && fourthMax == false && attackCounter == 3)
        {
            mMgr.currency = mMgr.currency - attackPrice;
            maxBuff++;
            attackPricetext.text = stringPrices[maxBuff];
            attackPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            fourthMax = true;
        }
    }

    public void Decrease()
    {
        if (attackCounter > 0)
        {
            attackCounter--;
        }
    }
}
