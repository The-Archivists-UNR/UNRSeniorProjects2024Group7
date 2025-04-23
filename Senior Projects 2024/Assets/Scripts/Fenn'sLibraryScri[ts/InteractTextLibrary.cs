using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


//Author: Fenn Edmonods
public class InteractTextLibrary : MonoBehaviour
{

    public TextMeshProUGUI interactText;
    int sceneHolder;

    public int interactableType;

    public SceneSwitch sceneMgr;
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
            sceneMgr.currentScene = sceneHolder;
            sceneMgr.LoadScene();
        }
    }

    public void variableUpdate()
    {
        switch(interactableType)
        {
            case 0:
                interactText.text = "[E] Write Reports (Minigame)";
                sceneHolder = 0;

            break;
        }
    }
}
