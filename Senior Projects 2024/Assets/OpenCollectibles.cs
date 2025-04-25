using TMPro;
using UnityEngine;
//author: Kat
//taking code from fenn's book switch 
public class OpenCollectibles : MonoBehaviour
{
    public GameObject Collectibles;
    public PanelMover panel;
    public string interactKey = "e";
    private bool playerInCollider = false;
    public TextMeshProUGUI interactText;

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
            interactText.text = "[E] Open Collectables";
            playerInCollider = true;
            print("entered collectibles");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.text = "";
            playerInCollider = false;
            if (Collectibles != null)
            {
                //Collectibles.SetActive(false);
                panel.isVisible = false;
                 print("Closed UI");
            }
            print("exited collectibles");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerInCollider)
        {
            interactText.text = "[E] Open Collectables";
            if (Input.GetKeyDown(interactKey))
            {
                panel.isVisible = true;
                if (Collectibles != null)
                {
                    interactText.text = "";
                    //Collectibles.SetActive(true);
                    //panel.isVisible = true;
                    print("Opened UI");
                    pauseTime();
                }
                else
                {
                    interactText.text = "";
                    print("What UI?");
                }
            }
        }
    }


}