using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthImprovement : MonoBehaviour
{
    public static HealthImprovement inst;

    float[] healthArray = { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    string[] healthTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int healthCounter;

    public TextMeshProUGUI healthText;
    public OpheliaStats oStats;

    public TextMeshProUGUI healthPricetext;

    public moneyMgr mMgr;

    int healthPrice = 50;
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
        if (healthCounter < maxBuff)
        {
            healthCounter++;
        }
    }

    public void Decrease()
    {
        if (healthCounter > 0)
        {
            healthCounter--;
        }
    }

    public void CheckMax()
    {
        if (mMgr.currency >= healthPrice && firstMax == false && healthCounter == 0)
        {
            print("I have increased");
            mMgr.currency = mMgr.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            firstMax = true;
            coinText.UpdateMoneyText();
            // firstMax = true;
            print(firstMax);
        }

        if (mMgr.currency >= healthPrice && firstMax == true && secondMax == false && healthCounter == 1)
        {
            mMgr.currency = mMgr.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            secondMax = true;
        }

        if (mMgr.currency >= healthPrice && secondMax == true && thirdMax == false && healthCounter == 2)
        {
            mMgr.currency = mMgr.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            thirdMax = true;
        }

        if (mMgr.currency >= healthPrice && thirdMax == true && fourthMax == false && healthCounter == 3)
        {
            mMgr.currency = mMgr.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            fourthMax = true;
        }
    }
}
