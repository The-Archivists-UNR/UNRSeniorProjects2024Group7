using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulieBuff : MonoBehaviour
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
            JulieAdjuster(1);  
        }

        if (nPCController.relationshipScore >= 15 && levelTwoHit == false)
        {
            levelTwoHit = true;
             JulieAdjuster(2);   
        }

        if (nPCController.relationshipScore >= 30 && levelThreeHit == false)
        {
            levelThreeHit = true;
             JulieAdjuster(3);
        }
    }

    public void NegStat()
    {
        oStats.ogHP -= 5;
    }

    public void JulieAdjuster(int relationLVL)
    {
        switch (relationLVL)
        {
            case 1:
                oStats.ogHP += 5;
                break;
            case 2:
                oStats.ogHP += 10;
                break;
            case 3:
                oStats.ogHP += 15;
                break;
        }

    }
}
