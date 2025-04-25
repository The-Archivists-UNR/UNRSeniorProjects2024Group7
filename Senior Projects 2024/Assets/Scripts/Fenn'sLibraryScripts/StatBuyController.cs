using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Author: Fenn Edmonds
public class StatBuyController : MonoBehaviour
{
    public PanelMover panelMover;
    public GameObject interactableText;
    public TextMeshProUGUI interactText;
    public int interactableType;

    public string playerTag = "Player";
    public string interactKey = "e";
    private bool playerInRange = false;
    private Transform playerTransform;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            interactText.text = "[E] Increase Stats";
            playerInRange = true;
            playerTransform = other.transform;
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
            interactText.text = "";
            panelMover.isVisible = true;
        }
    }
}
