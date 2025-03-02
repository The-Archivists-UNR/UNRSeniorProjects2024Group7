//Script: Door.cs
//Contributor: Liam Francisco
//Summary: Class for any door objects in a combat world
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoor : Interactable
{
    public bool locked;
    public NewRoom room;

    // adds this door to the PlayerMgr’s list of interactables so the player can reference it in its Interact method in PlayerController
    void Start()
    {
        PlayerMgr.inst.interactables.Add(this);
        room = gameObject.GetComponentInParent<NewRoom>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //loads the next room
    public override void Interact()
    {
        base.Interact();
        if (!locked)
        {
            NewGameMgr.inst.NextRoom();
        }
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