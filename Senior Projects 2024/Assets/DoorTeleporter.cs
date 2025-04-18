using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//so this code works in that it does teleport the player but it only teleports the player to the corner of the room closest to the desired room.
//many such cases
public class DoorTeleporter : MonoBehaviour
{
    public Transform doorDestination;
    public string playerTag = "Player";
    public string interactKey = "e";
    private bool playerInRange = false;
    private Transform playerTransform;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            playerTransform = other.transform;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            playerTransform = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        if (playerTransform != null && doorDestination != null)
        {
            playerTransform.position = doorDestination.position;
            playerTransform.rotation = doorDestination.rotation;

        }
        else
        {
            if (playerTransform == null)
            {
                print("player transform is null");
            }
            if (doorDestination == null)
            {
                print("teleport is null");
            }
        }

    }

}
