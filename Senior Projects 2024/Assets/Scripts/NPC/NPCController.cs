//Authored by Lanielle(LLM Interaction)
/* TO DO
 * add/use count as interaction limit
 * can setAIText and AIReplyComplete be merged?
 * handle quest completion based on which npc player talked to
 * make a  function in llm dialogue to forget some prompts -----what does this mean???
 * use history housed in NPC controller instead of llm dialogue
 * handle casses where player is already viewing quest dialogue
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
using TMPro;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(SphereCollider))]

public class NPCController : MonoBehaviour
{
    new public string name;
    public int relationshipScore;
    public string prompt;
    public List<string> dialogueTranscript;
    public List<string> recentTranscript;
    public string memory;
    //public Vector3 spawnPos;
    //public string scoreSavingFile;
    //public Boolean willingToTalk = true;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI AIText;
    public PanelMover textbox;
    public InputField playerText;
    public Sprite NPCSprite;
    public Image spriteBox;

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
            foreach (string str in recentTranscript) { transcript += str + "\n"; }
            Debug.Log(transcript);

            //make a special function in llm dialogue for this case so it will not be remembered
            LLM.getResponse("How pleasant is Ophelia in this transcript on a scale from 1 to 10? " +
                    "Respond with only the number." + transcript, setRating);
            LLM.getResponse("please summarize the following transcript: \n" + transcript, setNPCMemory);
            inConversation = false;
        }

        //change the following to call a numResponsesThisNPCIsWillingToGive variable that gets decremented
        //count = 0;
        //npc.willingToTalk = true;


    }

    private void StartDialogue()
    {
        // TO DO: DO NOT ALLOW THE REMAINDER OF THIS FUNCTION TO RUN IF PLAYER IS
        // ENGAGED IN HARDCODED QUEST DIALOGUE
        if (inConversation) { return; }
        nameText.text = name;
        spriteBox.sprite = NPCSprite;
        textbox.isVisible = true;
        inConversation = true;
        scoreText.text = "Relationship: " + relationshipScore;

        //modify the following based on npc and quests?
        GameEventsManager.instance.miscEvents.PatronTalked();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        playerText.text = "";
        //llmConvo = true;

        if (memory == "") { LLM.getResponse(prompt, setAIText, AIReplyComplete); Debug.Log("1"); }
        else { LLM.getResponse(prompt + "\n here's what happened so far:\n" + memory, setAIText, AIReplyComplete); Debug.Log("2"); }
    }

    private void onInputFieldSubmit(string message)
    {
        if (!inConversation) { return; }
        if (message.Trim() != "")
        {
            playerText.interactable = false;
            dialogueTranscript.Add("Ophelia: " + message);
            recentTranscript.Add("Ophelia: " + message);

            LLM.getResponse(message, setAIText, AIReplyComplete);
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
        recentTranscript.Add(name + ": " + text);
    }

    private void setRating(string number)
    {
        int rating = 0;
        int.TryParse(number, out rating);
        if (rating > 5) { relationshipScore += 1; }
        scoreText.text = "Relationship: " + relationshipScore;
    }

    private void setNPCMemory(string memory)
    {
        this.memory += memory;
        recentTranscript.Clear();
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