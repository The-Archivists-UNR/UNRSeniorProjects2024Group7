using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Fenn Edmonds
public class NPCRelationshipMgr : MonoBehaviour
{
    public ItemType itemType;

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
        if(nPCController.relationshipScore >= 5 && levelOneHit == false)
        {
            levelOneHit = true;
            switch(itemType)
            {
                case ItemType.Julie:
                    JulieAdjuster(1);
                break;
                case ItemType.Kirk:
                    KirkAdjuster(1);
                break;
                case ItemType.Bonnie:
                    BonnieAdjuster(1);
                break;
                case ItemType.Oswald:
                    OswaldAdjuster(1);
                break;
            }
        }

        if(nPCController.relationshipScore >= 15 && levelTwoHit == false)
        {
            levelTwoHit = true;
            switch(itemType)
            {
                case ItemType.Julie:
                    JulieAdjuster(2);
                break;
                case ItemType.Kirk:
                    KirkAdjuster(2);
                break;
                case ItemType.Bonnie:
                    BonnieAdjuster(2);
                break;
                case ItemType.Oswald:
                    OswaldAdjuster(2);
                break;
            }
        }

        if(nPCController.relationshipScore >= 30 && levelThreeHit == false)
        {
            levelThreeHit = true;
            switch(itemType)
            {
                case ItemType.Julie:
                    JulieAdjuster(3);
                break;
                case ItemType.Kirk:
                    KirkAdjuster(3);
                break;
                case ItemType.Bonnie:
                    BonnieAdjuster(3);
                break;
                case ItemType.Oswald:
                    OswaldAdjuster(3);
                break;
            }
        }
    }

    public void JulieAdjuster(int relationLVL)
    {
        switch(relationLVL)
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

    public void KirkAdjuster(int relationLVL)
    {
        switch(relationLVL)
        {
            case 1:
                // oStats.ogHP += 5;
            break;
            case 2:
                // oStats.ogHP += 10;
            break;
            case 3:
                // oStats.ogHP += 15;
            break;
        }

    }

    public void BonnieAdjuster(int relationLVL)
    {
        switch(relationLVL)
        {
            case 1:
                oStats.ogSpeed += 2;
            break;
            case 2:
                oStats.ogSpeed += 3;
            break;
            case 3:
                oStats.ogSpeed += 5;
            break;
        }
    }

    public void OswaldAdjuster(int relationLVL)
    {
        switch(relationLVL)
        {
            case 1:
                oStats.ogDamage += 5;
            break;
            case 2:
                oStats.ogDamage += 10;
            break;
            case 3:
                oStats.ogDamage += 15;
            break;
        }
    }


}
