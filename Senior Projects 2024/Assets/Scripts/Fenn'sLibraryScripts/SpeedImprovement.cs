using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedImprovement : MonoBehaviour
{
    public static SpeedImprovement inst;
    
    float[] speedArray = { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    string[] speedTextArray = { "0%", "5%", "10%", "15%", "20%", "25%" };
    public int speedCounter;

    public TextMeshProUGUI speedText;

    public TextMeshProUGUI speedPricetext;

    int speedPrice = 50;
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
            OpheliaStats.inst.SpeedPercent = speedBuff;
        }
        if (speedCounter == 0)
        {
            OpheliaStats.inst.SpeedPercent = 1;
        }
        speedText.text = speedTextArray[speedCounter];
    }

    public void Increase()
    {
        if (speedCounter < maxBuff)
        {
            speedCounter++;
        }
    }

    public void Decrease()
    {
        if (speedCounter > 0)
        {
            speedCounter--;
        }
    }

    public void CheckMax()
    {
        if (moneyMgr.inst.currency >= speedPrice && firstMax == false && speedCounter == 0)
        {
            print("I have increased");
            moneyMgr.inst.currency = moneyMgr.inst.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            firstMax = true;
            coinText.UpdateMoneyText();
            // firstMax = true;
            print(firstMax);
        }

        if (moneyMgr.inst.currency >= speedPrice && firstMax == true && secondMax == false && speedCounter == 1)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            secondMax = true;
        }

        if (moneyMgr.inst.currency >= speedPrice && secondMax == true && thirdMax == false && speedCounter == 2)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            thirdMax = true;
        }

        if (moneyMgr.inst.currency >= speedPrice && thirdMax == true && fourthMax == false && speedCounter == 3)
        {
            moneyMgr.inst.currency = moneyMgr.inst.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            fourthMax = true;
        }
    }
}
