using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDoor : NewDoor
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMgr.inst.interactables.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
    }

    //detects when the player is in the interacting range of the door.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !locked)
        {
            interactable = true;
        }
    }

    //detects when the player leaves the interacting range of the door.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = false;
        }
    }
}
