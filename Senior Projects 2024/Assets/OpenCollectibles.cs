using UnityEngine;
//author: Kat
//taking code from fenn's book switch 
public class OpenCollectibles : MonoBehaviour
{
    public GameObject Collectibles;
    public string interactKey = "e";
    private bool playerInCollider = false;

    // Pauses the time for when you're in a menu
    public void pauseTime()
    {
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        Time.timeScale = 0;
    }

    // Starts the time for when you're out of a menu
    public void startTime()
    {
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = true;
            print("entered collectibles");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = false;
            if (Collectibles != null)
            {
                Collectibles.SetActive(false);
                print("Closed UI");
            }
            print("exited collectibles");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerInCollider)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (Collectibles != null)
                {
                    Collectibles.SetActive(true);
                    print("Opened UI");
                    pauseTime();
                }
                else
                {
                    print("What UI?");
                }
            }
        }
    }


}