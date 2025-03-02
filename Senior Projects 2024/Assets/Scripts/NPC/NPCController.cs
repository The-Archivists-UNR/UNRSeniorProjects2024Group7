//Authored by Lanielle(LLM Interaction)
/* TO DO
 * add/use count as interaction limit
 * can setAIText and AIReplyComplete be merged?
 * handle quest completion based on which npc player talked to
 * make a  function in llm dialogue to forget some prompts
 * use history housed in NPC controller instead of llm dialogue
 */

/**
 * This file defines the NPCController class
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Cinemachine.DocumentationSortingAttribute;
using System.Runtime.ConstrainedExecution;
using static UnityEditor.PlayerSettings;
using Unity.VisualScripting;

[System.Serializable]
[RequireComponent(typeof(SphereCollider))]

public class NPCController : MonoBehaviour
{
    public string name;
    public int relationshipScore;
    public string prompt;
    public List<string> dialogueTranscript;
    public string memory;
    //public Vector3 spawnPos;
    //public string scoreSavingFile;
    //public Boolean willingToTalk = true;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI AIText;
    public PanelMover textbox;
    public InputField playerText;

    public LLMInteraction LLM;

    private bool playerIsNear = false;
    private bool inConversation = false;
    //private int count;
    //private bool llmConvo = false;

    void Start()
    {
        memory = "";
        dialogueTranscript = new List<string>();
        //count = 10;
        playerText.onSubmit.AddListener(onInputFieldSubmit);
        playerText.Select();
    }

    void Update()
    {
        if (!playerIsNear) { return; }
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            StartDialogue();
        }
    }

    public void EndDialogue()
    {
        //textbox.isVisible = false;
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        if (inConversation)
        {
            string transcript = "";
            foreach (string str in dialogueTranscript) { transcript += str + "\n"; }
            Debug.Log(transcript);

            //make a special function in llm dialogue for this case so it will not be remembered
            LLM.SendChatRequestToGemini("How pleasant is Ophelia in this transcript on a scale from 1 to 10? " +
                    "Respond with only the number." + transcript, setRating);
            //LLM.getResponse("please summarize the following transcript: \n" + transcript, setNPCMemory);
            inConversation = false;
        }

        //change the following to call a numResponsesThisNPCIsWillingToGive variable that gets decremented
        //count = 0;
        //npc.willingToTalk = true;


    }

    private void StartDialogue()
    {
        nameText.text = name;
        textbox.isVisible = true;
        inConversation = true;
        scoreText.text = "Relationship: " + relationshipScore;

        //modify the following based on npc and quests?
        GameEventsManager.instance.miscEvents.PatronTalked();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        playerText.text = "";
        //llmConvo = true;

        if (memory == "") { LLM.SendPromptRequestToGemini(prompt, setAIText, AIReplyComplete); }
        else { LLM.SendChatRequestToGemini(prompt + "\n here's what happened so far:\n" + memory, setAIText, AIReplyComplete); }
    }

    private void onInputFieldSubmit(string message)
    {
        if(!inConversation) { return; }
        if (message.Trim() != "")
        {
            playerText.interactable = false;
            dialogueTranscript.Add("Ophelia: " + message);

            LLM.SendChatRequestToGemini(message, setAIText, AIReplyComplete);
            //LLM.getResponse("How pleasant is this message on a scale from 1 to 10? " +
            //    "Respond with only the number." + message, setRating);
        }
    }

    private void AIReplyComplete()
    {
        playerText.text = "";
        playerText.interactable = true;
        playerText.Select();
    }

    private void setAIText(string text)
    {
        AIText.text = text;
        dialogueTranscript.Add(name + ": " + text);
    }

    private void setRating(string number)
    {
        int rating = 0;
        int.TryParse(number, out rating);
        if (rating > 5) { relationshipScore += 1; }
        scoreText.text = "Relationship: "+relationshipScore;
    }

    private void setNPCMemory(string memory)
    {
        memory += memory;
        dialogueTranscript.Clear();
    }


    private void OnTriggerEnter(Collider otherColldier)
    {
        if (otherColldier.tag == "Player") { playerIsNear = true; }
    }
    private void OnTriggerExit(Collider otherColldier)
    {
        if (otherColldier.tag == "Player") { playerIsNear = false; }
    }



}