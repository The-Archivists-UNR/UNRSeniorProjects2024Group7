using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
//so this code works in that it does teleport the player but it only teleports the player to the corner of the room closest to the desired room.
//many such cases

public enum FloorDestination
{
    StatBuffRoom,
    Courtyard,
    Hub,
    Fantasy,
    Noire,
    Scifi,
    Upstairs,
    Downstairs
}
public class DoorTeleporter : MonoBehaviour
{
    public Transform doorDestination;
    public string playerTag = "Player";
    public string interactKey = "e";
    private bool playerInRange = false;
    private Transform playerTransform;

    public TextMeshProUGUI interactText;

    public FloorDestination destination;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            playerTransform = other.transform;

            switch (destination)
            {
                case FloorDestination.StatBuffRoom:
                    interactText.text = "[E] Enter Stat Buff Room";
                break;
                case FloorDestination.Courtyard:
                    interactText.text = "[E] Enter Courtyard ";
                break;
                case FloorDestination.Hub:
                    interactText.text = "[E] Enter Hub";
                break;
                case FloorDestination.Fantasy:
                    interactText.text = "[E] Enter Fantasy Section";
                break;
                case FloorDestination.Noire:
                    interactText.text = "[E] Enter Newspaper Section";
                    break;
                case FloorDestination.Scifi:
                    interactText.text = "[E] Enter Scifi Section";
                break;
                case FloorDestination.Upstairs:
                    interactText.text = "[E] Go Upstairs";
                break;
                case FloorDestination.Downstairs:
                    interactText.text = "[E] Go Downstairs";
                break;
            }
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            interactText.text = "";
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
            NavMeshAgent playerAgent = playerTransform.GetComponent<NavMeshAgent>();
            playerAgent.enabled = false;
            playerTransform.position = doorDestination.position;
            playerTransform.rotation = doorDestination.rotation;
            playerAgent.enabled = true;
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
