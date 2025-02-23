//Authored By Fenn(Quest Part) and Lanielle(LLM Interaction)

/* TO DO
 * remove count, replace with exit button
 * can setAIText and AIReplyComplete be merged?
 */

/**
 * This file defines the Dialogue class, which contains:
 * 
 * (variables)
 * text components for input and output
 * variables pertaining to display (textSpeed, PanelMovers, etc.)
 * a variable to indicate and access the associated NPC
 * an LLM variable, which in practice calls the LLMDialogue class
 * variables to track the duration and type of conversation
 * 
 * (public methods)
 * Start: prep for convo, add requisite listener
 * Update: check for enter key and respond
 * StartDialogue: starts LLM dialogue - Lanielle
 * StartQuestDialogue: starts hardcoded dialogue for quests - Fenn
 * EndQuestDialogue: a second set of hardcoded dialogue for quests - Fenn
 * 
 * (private methods)
 * SetAIText: places LLM output in textbox, used as callback function
 * onInputFieldSubmit: what aforementioned listener calls, handles conversation, duration limit found here - Lanielle
 * AIReplyComplete: ends AI request and changes player's relationship score - Lanielle
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using TMPro.Examples;
//using Unity.VisualScripting;
//using Palmmedia.ReportGenerator.Core;
//using static System.Net.Mime.MediaTypeNames;
//using LLMUnity;

//apparently a necessity when using functions as parameters for callbacks
public delegate void Callback<T>(T message);
public delegate void EmptyCallback();

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI scriptedTextComponent;
    public TextMeshProUGUI AIText;
    public float textSpeed;
    public PanelMover textbox;
    public PanelMover NPCtextbox;
    public InputField playerText;

    public NPCController npc;
    public LLMInteraction LLM;
    [HideInInspector] public List<string> codedlines;

    private int count;
    private bool llmConvo = false;
    private int index;

    void Start()
    {
        count = 0;
        npc.willingToTalk = true;
        playerText.onSubmit.AddListener(onInputFieldSubmit);
        playerText.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (llmConvo)
            //increment interaction count, reset if count is 5 - Lanielle
            {
                count++;
                if (count >= 5)
                {
                    textbox.isVisible = false;
                    textComponent.text = string.Empty;
                    GameEventsManager.instance.playerEvents.EnablePlayerMovement();
                    count = 0;
                    npc.willingToTalk = true;
                }
            }
            else
            //advance hardcoded dialogue - Fenn
            {
                if (scriptedTextComponent.text == codedlines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    scriptedTextComponent.text = codedlines[index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        textbox.isVisible = true;
        GameEventsManager.instance.miscEvents.PatronTalked();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        playerText.text = "";
        llmConvo = true;
        LLM.getResponse(npc.prompt, setAIText, AIReplyComplete);
    }

    public void StartQuestDialogue(List<string> quest)
    {
        NPCtextbox.isVisible = true;
        GameEventsManager.instance.miscEvents.PatronTalked();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        scriptedTextComponent.text = "";
        codedlines.Clear();
        codedlines = quest;
        index = 0;
        llmConvo = false;
        StartCoroutine(TypeLine());
    }

    private void setAIText(string text) {   AIText.text = text; }

    private void setRating(string number)
    {
        int rating = 0;
        int.TryParse(number, out rating);
        Debug.Log("rating: " + rating);
        if (rating > 5)
        {
            rating = 1;
        }
        else rating = 0;
        npc.changeRelationshipScore(rating);
    }

    public void EndQuestDialogue(List<string> quest)
    {
        NPCtextbox.isVisible = true;
        GameEventsManager.instance.miscEvents.PatronTalked();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        scriptedTextComponent.text = "";
        codedlines.Clear();
        codedlines = quest;
        index = 0;
        llmConvo = false;
        StartCoroutine(TypeLine());
    }

    private void onInputFieldSubmit(string message)
    {
        playerText.interactable = false;
        if (count > 2)
        {
            playerText.text = "The Ghost seems busy. Use 'enter' to exit.";
            npc.willingToTalk = false;

            LLM.getResponse(message, setAIText, AIReplyComplete);
        }
        else
        {
            LLM.getResponse(message, setAIText, AIReplyComplete);
        }
    }

    private void AIReplyComplete()
    {
        LLM.getResponse("Rate the pleasantness of this conversation on a scale from 1 to 10. " +
                "Respond with only the number.", setRating);
        if (npc.willingToTalk)
        {
            playerText.text = "";
            playerText.interactable = true;
            playerText.Select();
        }
    }

    //This function iterates through the characters in the dialogue string of each line. - Fenn
    IEnumerator TypeLine()
    {
        foreach (char c in codedlines[index].ToCharArray())
        {
            scriptedTextComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //This function goes to next line in the list of strings to continue dialogue - Fenn
    void NextLine()
    {
        if (index < codedlines.Count - 1)
        {
            index++;
            scriptedTextComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            NPCtextbox.isVisible = false;
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }
}