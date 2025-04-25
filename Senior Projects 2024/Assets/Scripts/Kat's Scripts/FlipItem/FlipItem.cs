using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

// Authors: Fenn Edmonds and Kat Wayman
public enum ItemType
{ 
    Julie,
    Bonnie,
    Kirk,
    Oswald,
    Sam
}

public class FlipItem : MonoBehaviour, IPointerClickHandler
{
    private bool tagFlipped = false;

    public OpheliaStats oStats;

    public ItemType type;

    public CollectableManagers CMgr;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Flip();
        }
    }

    public void CallMgr()
    {
        CMgr.PreviouslyActivatedCheck();
        CMgr.UpdateOStats(type);
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
