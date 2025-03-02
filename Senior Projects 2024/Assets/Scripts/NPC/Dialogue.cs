//Authored By Fenn(Quest Part) and Lanielle(LLM Interaction)

/* TO DO
 * remove count, replace with exit button
 * can setAIText and AIReplyComplete be merged?
 * handle quest completion based on which npc player talked to
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
 * StartQuestDialogue: starts hardcoded dialogue for quests - Fenn
 * EndQuestDialogue: a second set of hardcoded dialogue for quests - Fenn
 */



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

    //private int count;
    private bool llmConvo = false;
    private int index;

    void Start()
    {
        //playerText.onSubmit.AddListener(onInputFieldSubmit);
        //playerText.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!llmConvo)
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