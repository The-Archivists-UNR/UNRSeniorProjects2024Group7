using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonnieBuff : MonoBehaviour
{
    public NPCController nPCController;
    public OpheliaStats oStats;

    bool levelOneHit = false;
    bool levelTwoHit = false;
    bool levelThreeHit = false;
    // Start is called before the first frame update
    void Start()
    {
        ChangeStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeStats()
    {
        if (nPCController.relationshipScore >= 5 && levelOneHit == false)
        {
            levelOneHit = true;
            BonnieAdjuster(1);
        }

        if (nPCController.relationshipScore >= 15 && levelTwoHit == false)
        {
            levelTwoHit = true;
            BonnieAdjuster(2);

        }

        if (nPCController.relationshipScore >= 30 && levelThreeHit == false)
        {
            levelThreeHit = true;
            BonnieAdjuster(3);
        }
    }

    public void NegStat()
    {
        OpheliaStats.inst.ogSpeed -= 1;
    }

    public void BonnieAdjuster(int relationLVL)
    {
        switch (relationLVL)
        {
            case 1:
                OpheliaStats.inst.ogSpeed += 1;
                break;
            case 2:
                OpheliaStats.inst.ogSpeed += 2;
                break;
            case 3:
                OpheliaStats.inst.ogSpeed += 3;
                break;
        }
    }
}
