using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirkBuff : MonoBehaviour
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
            KirkAdjuster(1);
        }

        if (nPCController.relationshipScore >= 15 && levelTwoHit == false)
        {
            levelTwoHit = true;
            KirkAdjuster(2);
        }

        if (nPCController.relationshipScore >= 30 && levelThreeHit == false)
        {
            levelThreeHit = true;
            KirkAdjuster(3);
        }
    }

    public void KirkAdjuster(int relationLVL)
    {
        switch (relationLVL)
        {
            case 1:
                oStats.ogHP += 2;
                oStats.ogSpeed += 1;
                oStats.ogDamage += 2;
                break;
            case 2:
                oStats.ogHP += 2;
                oStats.ogSpeed += 1;
                oStats.ogDamage += 2;
                break;
            case 3:
                oStats.ogHP += 2;
                oStats.ogSpeed += 1;
                oStats.ogDamage += 2;
                break;
        }

    }
}
