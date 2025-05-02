using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoor : MonoBehaviour
{
    private Coroutine clearMessageRoutine;
    private bool playerIsNear = false; //This just checks for if the player is close to activate the trigger

    void Start()
    {
        //PlayerMgr.inst.interactables.Add(this);
    }

    //Call upon the game event manager to add my events
    private void OnEnable()
    {
        //print("Here1");
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
        //print("Here2");
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // simulate submit
        {
            SubmitPressed();
        }
    }

    //I forgot to get rid of my debugs and I'm scared to break something so pretend
    //Like they don't exist thank you :))
    private void SubmitPressed()
    {
        if (!playerIsNear)
        {

            //print("Here3");
            return;
        }
        //interactable = true;
        if (PlayerMgr.inst.interactText != null)
            PlayerMgr.inst.interactText.text = "The Courtyard looks beautiful...";
        else
            Debug.LogWarning("interactText is null!");
        print("Here4");


        if (clearMessageRoutine != null)
        {
            StopCoroutine(clearMessageRoutine);
        }

        clearMessageRoutine = StartCoroutine(ClearInteractMessageAfterDelay(3f)); // 3 seconds
    }

    private IEnumerator ClearInteractMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerMgr.inst.interactText.text = "";
    }

    // Checks for the player entering the trigger area in which you can interact with the book
    private void OnTriggerEnter(Collider otherColldier)
    {
        if (otherColldier.tag == "Player")
        {
            Debug.Log("Player entered door trigger");
            playerIsNear = true;
        }
    }

    // Checks for the player leaving the trigger area in which you can interact with the book 
    // (and makes it so they can't interact with it outside a certain range)
    private void OnTriggerExit(Collider otherColldier)
    {
        if (otherColldier.tag == "Player")
        {
            playerIsNear = false;
        }
    }
}