using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public PanelMover textbox;
    public TimellineController timelineMgr;

    private int index;
    public bool inprogress;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        //     GameEventsManager.instance.miscEvents.PatronTalked();
        textComponent.text = string.Empty;
        index = 0;
        textbox.isVisible = true;
        inprogress = true;
        //     GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        StartCoroutine(TypeLine());
        //     // for(int i = 0; i < lines.Length; i++)
        //     //     NextLine();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textbox.isVisible = false;
            inprogress = false;
            //timelineMgr.dialogueCanStart = true;
            // GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }
}
