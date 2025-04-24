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

    public TextMeshProUGUI speedPricetext;

    public moneyMgr mMgr;

    int speedPrice = 50;
    string[] stringPrices = {"50", "100", "150", "200", "250", "Maxed"};
    int[] prices = {50, 100, 150, 200, 250, 0};

    int maxBuff = 0;

    bool firstMax = false;
    bool secondMax = false;
    bool thirdMax = false;
    bool fourthMax = false;
    bool fifthMax = false;

    public CoinTextUpdator coinText;

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
        if (speedCounter < maxBuff)
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

    public void CheckMax()
    {
        if (mMgr.currency >= speedPrice && firstMax == false && speedCounter == 0)
        {
            print("I have increased");
            mMgr.currency = mMgr.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            firstMax = true;
            otherButton.firstMax = true;
            coinText.UpdateMoneyText();
            // firstMax = true;
            print(firstMax);
        }

        if (mMgr.currency >= speedPrice && firstMax == true && secondMax == false && speedCounter == 1)
        {
            mMgr.currency = mMgr.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            secondMax = true;
            otherButton.secondMax = true;
        }

        if (mMgr.currency >= speedPrice && secondMax == true && thirdMax == false && speedCounter == 2)
        {
            mMgr.currency = mMgr.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            thirdMax = true;
            otherButton.thirdMax = true;
        }

        if (mMgr.currency >= speedPrice && thirdMax == true && fourthMax == false && speedCounter == 3)
        {
            mMgr.currency = mMgr.currency - speedPrice;
            maxBuff++;
            speedPricetext.text = stringPrices[maxBuff];
            speedPrice = prices[maxBuff];
            coinText.UpdateMoneyText();
            fourthMax = true;
            otherButton.fourthMax = true;
        }
    }
}
