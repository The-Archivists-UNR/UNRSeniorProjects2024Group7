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

    public TextMeshProUGUI healthPricetext;


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
        healthPricetext.text = stringPrices[maxBuff];
        healthPrice = prices[maxBuff];

    }

    // Update is called once per frame
    void Update()
    {
        healthPricetext.text = stringPrices[maxBuff];
    }

    public void UpdateHealth()
    {
        if (healthCounter > 0)
        {
            float healthBuff = 1 + healthArray[healthCounter];
            OpheliaStats.inst.HpPercent = healthBuff;
        }
        if (healthCounter == 0)
        {
            OpheliaStats.inst.HpPercent = 1;
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
        if (moneyMgr.inst.currency >= healthPrice && firstMax == false && healthCounter == 0)
        {
            print("I have increased");
            moneyMgr.inst.currency = moneyMgr.inst.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            firstMax = true;
            coinText.UpdateMoneyText();
            // firstMax = true;
            print(firstMax);
        }

        if (moneyMgr.inst.currency >= healthPrice && firstMax == true && secondMax == false && healthCounter == 1)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            secondMax = true;
        }

        if (moneyMgr.inst.currency >= healthPrice && secondMax == true && thirdMax == false && healthCounter == 2)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            thirdMax = true;
        }

        if (moneyMgr.inst.currency >= healthPrice && thirdMax == true && fourthMax == false && healthCounter == 3)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - healthPrice;
            maxBuff++;
            healthPricetext.text = stringPrices[maxBuff];
            healthPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            fourthMax = true;
        }
    }
}
