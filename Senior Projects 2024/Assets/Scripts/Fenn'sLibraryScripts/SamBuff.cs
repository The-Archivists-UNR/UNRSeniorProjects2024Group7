using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamBuff : MonoBehaviour
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
            SamAdjuster(1);
        }

        if (nPCController.relationshipScore >= 15 && levelTwoHit == false)
        {
            levelTwoHit = true;
            SamAdjuster(2);
        }

        if (nPCController.relationshipScore >= 30 && levelThreeHit == false)
        {
            levelThreeHit = true;
            SamAdjuster(3);
        }
    }

    public void NegStat()
    {
        OpheliaStats.inst.ogDamage -= 1;
    }

    public void SamAdjuster(int relationLVL)
    {
        switch (relationLVL)
        {
            case 1:
                OpheliaStats.inst.ogDamage += 5;
                break;
            case 2:
                OpheliaStats.inst.ogDamage += 10;
                break;
            case 3:
                OpheliaStats.inst.ogDamage += 15;
                break;
        }

    }
}
