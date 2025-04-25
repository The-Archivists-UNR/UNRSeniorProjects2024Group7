using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManagers : MonoBehaviour
{

    public OpheliaStats oStats;
    public static CollectableManagers inst;

    bool previouslyActiveJulie = false;
    bool previouslyActiveKirk = false;
    bool previouslyActiveOswald = false;
    bool previouslyActiveBonnie = false;


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
                oStats.ogHP = oStats.ogHP + 25;
                previouslyActiveJulie = true;
                break;

            case ItemType.Kirk:
                 oStats.ogHP = oStats.ogHP + 5;
                oStats.ogSpeed = oStats.ogSpeed + 2;
                oStats.ogDamage = oStats.ogDamage + 5;
                previouslyActiveKirk = true;
                break;

            case ItemType.Bonnie:
                oStats.ogSpeed = oStats.ogSpeed + 5;
                previouslyActiveBonnie = true;
                break;

            case ItemType.Oswald:
                oStats.ogDamage = oStats.ogDamage + 20;
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
            oStats.ogHP = oStats.ogHP - 25;
            previouslyActiveJulie = false;
        }

        if (previouslyActiveKirk == true)
        {
            oStats.ogHP = oStats.ogHP - 5;
            oStats.ogSpeed = oStats.ogSpeed - 2;
            oStats.ogDamage = oStats.ogDamage - 5;
            previouslyActiveKirk = false;
        }

        if (previouslyActiveBonnie == true)
        {
            oStats.ogSpeed = oStats.ogSpeed - 5;
            previouslyActiveBonnie = false;
        }

        if (previouslyActiveOswald == true)
        {
            oStats.ogDamage = oStats.ogDamage - 20;
            previouslyActiveOswald = false;
        }
    }
}
