using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

// Authors: Fenn Edmonds and Kat Wayman
public enum ItemType
{ 
    Julie,
    Bonnie,
    Kirk,
    Oswald
}

public class FlipItem : MonoBehaviour, IPointerClickHandler
{
    private bool tagFlipped = false;

    public OpheliaStats oStats;

    public ItemType type;

    public bool previouslyActiveJulie;
    bool previouslyActiveKirk;
    bool previouslyActiveOswald;
    bool previouslyActiveBonnie;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Flip();
        }
    }

    //Fenn 
    public void UpdateOStats()
    {
        switch (type)
        {
            case ItemType.Julie:
                oStats.ogHP = oStats.ogHP + 25;
                previouslyActiveJulie = true;
            break;

            case ItemType.Kirk:
                // oStats.ogDamage = oStats.ogDamage + 25;
                // previouslyActiveBonnie = true;
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
        
        if(previouslyActiveKirk == true)
        {
            // oStats.og = oStats.ogDamage - 25;
            // previouslyActiveKirk = false;
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
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        // Perform a raycast to see if it hits any colliders
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            // Check if the hit object is this specific GameObject
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                Flip();
    //            }
    //        }
    //    }
    //}

    //Kat
    private void Flip()
    {
        tagFlipped = !tagFlipped;
        transform.DORotate(new(0, tagFlipped ? 0f : 180f, 0), 0.25f).SetUpdate(true);
    }
}
