using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 

public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Vector3 cachedScale;
    bool clicked = false;
    public saveSelect slotChoosen;
    public int assignedSlot;

    void Start() {

        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {

        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData) {

        transform.localScale = cachedScale;
    }

    public void onClick()
    {
        if (clicked == false)
        {
            foreach (GameObject slot in slotChoosen.slots)
            {
                slot.transform.localScale = new Vector3(1f, 1f, 1f);
                UIHover foundSlot = slot.GetComponent<UIHover>();
                foundSlot.cachedScale = new Vector3(1f, 1f, 1f);
                foundSlot.clicked = false;
            }
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            cachedScale = transform.localScale;
            slotChoosen.saveSlotNum = assignedSlot;
            clicked = true;
        }
        // if (clicked == true)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
