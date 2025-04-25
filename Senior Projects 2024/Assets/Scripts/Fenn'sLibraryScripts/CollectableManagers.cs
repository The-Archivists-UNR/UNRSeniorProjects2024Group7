using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManagers : MonoBehaviour
{

    public OpheliaStats oStats;
    public static CollectableManagers inst;

    public bool previouslyActiveJulie = false;
    public bool previouslyActiveKirk = false;
    public bool previouslyActiveOswald = false;
    public bool previouslyActiveBonnie = false;


    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
        //moneyText.text = "Tokens: " + currency;
    }

    public void UpdateOStats(ItemType type)
    {
        switch (type)
        {
            case ItemType.Julie:
                OpheliaStats.inst.ogHP = OpheliaStats.inst.ogHP + 25;
                previouslyActiveJulie = true;
                break;

            case ItemType.Kirk:
                 OpheliaStats.inst.ogHP = OpheliaStats.inst.ogHP + 5;
                OpheliaStats.inst.ogSpeed = OpheliaStats.inst.ogSpeed + 2;
                OpheliaStats.inst.ogDamage = OpheliaStats.inst.ogDamage + 5;
                previouslyActiveKirk = true;
                break;

            case ItemType.Bonnie:
                OpheliaStats.inst.ogSpeed = OpheliaStats.inst.ogSpeed + 5;
                previouslyActiveBonnie = true;
                break;

            case ItemType.Oswald:
                OpheliaStats.inst.ogDamage = OpheliaStats.inst.ogDamage + 20;
                previouslyActiveOswald = true;
                break;
        }
    }

    //Fenn
    public void PreviouslyActivatedCheck()
    {
        print("running check");
        if (previouslyActiveJulie == true)
        {
            OpheliaStats.inst.ogHP = OpheliaStats.inst.ogHP - 25;
            previouslyActiveJulie = false;
        }

        if (previouslyActiveKirk == true)
        {
            OpheliaStats.inst.ogHP = OpheliaStats.inst.ogHP - 5;
            OpheliaStats.inst.ogSpeed = OpheliaStats.inst.ogSpeed - 2;
            OpheliaStats.inst.ogDamage = OpheliaStats.inst.ogDamage - 5;
            previouslyActiveKirk = false;
        }

        if (previouslyActiveBonnie == true)
        {
            OpheliaStats.inst.ogSpeed = OpheliaStats.inst.ogSpeed - 5;
            previouslyActiveBonnie = false;
        }

        if (previouslyActiveOswald == true)
        {
            OpheliaStats.inst.ogDamage = OpheliaStats.inst.ogDamage - 20;
            previouslyActiveOswald = false;
        }
    }
}
