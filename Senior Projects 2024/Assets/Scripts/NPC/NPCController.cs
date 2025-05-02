//Authored by Lanielle(LLM Interaction), and Fenn(buffs)
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

//using System;
//using System.Collections;
using System.Collections.Generic;
//using TMPro.Examples;
using UnityEngine;
//using UnityEngine.InputSystem;
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
    //public List<string> recentTranscript;
    //public string memory;
    //public string scoreSavingFile;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI AIText;
    public PanelMover textbox;
    public InputField playerText;
    public Sprite NPCSprite;
    public Image spriteBox;

    public LLMInteraction LLM;

    public ItemType currentNPC;

    public JulieBuff jBuff;
    public BonnieBuff bBuff;
    public KirkBuff kBuff;
    public SamBuff sBuff;

    private bool playerIsNear = false;
    private bool inConversation = false;

    public TextMeshProUGUI interactText;
    //private int count;

    //set up memory and enter key listener
    void Start()
    {
        //memory = "";
        dialogueTranscript = new List<string>();
        //count = 10;
        playerText.onSubmit.AddListener(onInputFieldSubmit);
        playerText.Select();
    }

    //check for e key
    void Update()
    {
        if (!playerIsNear) { return; }
        if (Input.GetKeyDown(KeyCode.E)) { StartDialogue(); }
    }

    public void EndDialogue()
    {
        //textbox.isVisible = false;
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        if (inConversation)
        {
            inConversation = false;

            //if (recentTranscript.Count>1) //transcripts with 1 item include only the LLM speaking
            //{
            //    string transcript = "";
            //    foreach (string str in recentTranscript) { transcript += str + "\n"; }
            //    //LLM.getResponse("please summarize the following transcript: \n" + transcript, setNPCMemory);
            //}
        }
    }

    private void StartDialogue()
    {
        if (inConversation) { return; }

        //set up for conversation
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        nameText.text = name;
        spriteBox.sprite = NPCSprite;
        textbox.isVisible = true;
        inConversation = true;
        scoreText.text = "Relationship: " + relationshipScore;
        playerText.text = "";

        ////send different prompt if this is the first interaction, otherwise, include memory
        //if (memory == "") { LLM.getResponse(prompt, setAIText, AIReplyComplete); }
        //else { LLM.getResponse(prompt + "\n here's what happened so far:\n" + memory, setAIText, AIReplyComplete); }
        //replaced by stuff below, relocate use of memory?

        //don't regenerate a response if the LLM said something and the user hasn't responded to it
        if (dialogueTranscript.Count == 0) { LLM.getResponse(prompt, setAIText, AIReplyComplete); }
        else { AIText.text = dialogueTranscript[dialogueTranscript.Count - 1].Substring(name.Length+2); AIReplyComplete(); }

        //update stat corresponding to character
        switch (currentNPC)
        {
            case ItemType.Julie:
                jBuff.ChangeStats();
                break;
            case ItemType.Bonnie:
                bBuff.ChangeStats();
                break;
            case ItemType.Kirk:
                kBuff.ChangeStats();
                break;
            case ItemType.Sam:
                //print("statcalled2");
                sBuff.ChangeStats();
                break;
        }
    }

    private void onInputFieldSubmit(string message)
    {
        if (!inConversation) { return; }
        if (message.Trim() != "")
        {
            playerText.interactable = false;
            dialogueTranscript.Add("Ophelia: " + message+"\n");
            //recentTranscript.Add("Ophelia: " + message);

            LLM.getResponse("If the following dialogue is rude or incoherent respond with only the word \"TRUE\","+
                "otherwise respond normally.\n\n" + message, setAIText, AIReplyComplete);
            LLM.getResponse("How pleasant is this message on a scale from 1 to 10? " +
                "Respond with only the number." + message, setRating);
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
        //if rude or incoherent (llm has been told to say "TRUE" in those cases)
        if (text.Contains("TRUE"))
        {
            relationshipScore -= 1;
            scoreText.text = "Relationship: " + relationshipScore;
            AIText.text = "I'm not sure I understand.";
            dialogueTranscript.RemoveAt(dialogueTranscript.Count-1);
            //recentTranscript.RemoveAt(recentTranscript.Count-1);
            
            //debuff applied
            switch (currentNPC)
            {
                case ItemType.Julie:
                    print("statcalled");
                    jBuff.NegStat();
                    break;
                case ItemType.Bonnie:
                    bBuff.NegStat();
                    break;
                case ItemType.Kirk:
                    kBuff.NegStat();
                    break;
                case ItemType.Sam:
                    print("statcalled");
                    sBuff.NegStat();
                    break;
            }
        }
        else
        {
            AIText.text = text;
            dialogueTranscript.Add(name + ": " + text+"\n");
            //recentTranscript.Add(name + ": " + text);
        }
    }

    private void setRating(string number)
    {
        int rating = 0;
        int.TryParse(number, out rating);
        if (rating > 5) { relationshipScore += 1; }
        scoreText.text = "Relationship: " + relationshipScore;
    }

    //private void setNPCMemory(string memory)
    //{
    //    this.memory += memory;
    //    recentTranscript.Clear();
    //}


    private void OnTriggerEnter(Collider otherColldier)
    {
        if (otherColldier.tag == "Player") { 
            playerIsNear = true;
            interactText.text = "[E] Talk";
        }
    }
    private void OnTriggerExit(Collider otherColldier)
    {
        if (otherColldier.tag == "Player") { playerIsNear = false; interactText.text = ""; }
    }



}