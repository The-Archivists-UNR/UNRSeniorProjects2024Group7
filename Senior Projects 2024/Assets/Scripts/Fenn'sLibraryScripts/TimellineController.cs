using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


//Author: Fenn Edmonds

public class TimellineController : MonoBehaviour
{

    public PlayableDirector playableDirectorStart;
    public PlayableDirector playableDirectorNPC;
    public PlayableDirector playableDirectorMinigames;
    public PlayableDirector playableDirectorBuffs;
    public PlayableDirector playableDirectorEnd;



    public CutsceneDialogue dialogue;

    string[] linesNPCS = {"These three patrons can be freely chatted with.", "They're very excited to meet you!" };
    string[] linesMinigames = {"Here you'll find different minigames you can play.", "Each was intended to make working the library fun!"};
    string[] linesStatbuff = {"Lastly, in the stacks you'll find where you can buy stat increases!", "And in the file cabinet you can equip collectables!", "Now that you know everything, its up to you to lift the books curses.", "Good luck!"};

    public bool dialogueCanStart;

    //Wow this is super redudant but i am brainfried and can't think of a better way to do this
    bool sceneZero = true;
    bool sceneOne = false;
    bool sceneTwo = false;
    bool sceneThree = false;
    bool sceneFour = false;
    bool sceneFive = false;
    bool sceneSix = false;
    bool sceneSeven = false;
    bool sceneEight = false;
    bool sceneNine = false;

    public SceneSwitch sceneSwitch;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print("state:" + playableDirectorStart.state);
        if(playableDirectorStart.state == PlayState.Paused && sceneZero == true)
        {
            //print("This is being sent through");
            //dialogueCanStart = true;
            sceneOne = true;
            sceneZero = false;
            //sceneOne = true;
        }
        if (sceneOne == true && dialogue.inprogress == false)
        {
            //dialogueCanStart = false;
            StartSpeaking();
            sceneOne = false;
            sceneTwo = true;
        }

        if (playableDirectorNPC.state != PlayState.Playing && sceneTwo == true && dialogue.inprogress == false)
        {
            GameObject child = playableDirectorNPC.transform.GetChild(0).gameObject;
            child.SetActive(true);
            playableDirectorNPC.Play();
            sceneTwo = false;
            sceneThree = true;
        }

        if (playableDirectorNPC.state == PlayState.Paused == true && sceneThree == true )
        {
            dialogue.lines = linesNPCS;
            StartSpeaking();
            sceneThree = false;
            sceneFour = true;
        }

        if (playableDirectorMinigames.state != PlayState.Playing && sceneFour == true && dialogue.inprogress == false)
        {
            GameObject child = playableDirectorMinigames.transform.GetChild(0).gameObject;
            child.SetActive(true);
            playableDirectorMinigames.Play();
            sceneFour = false;
            sceneFive = true;
        }

        if (playableDirectorMinigames.state == PlayState.Paused == true && sceneFive == true)
        {
            dialogue.lines = linesMinigames;
            StartSpeaking();
            sceneFive = false;
            sceneSix = true;
        }

        if (playableDirectorBuffs.state != PlayState.Playing && sceneSix == true && dialogue.inprogress == false)
        {
            GameObject child = playableDirectorBuffs.transform.GetChild(0).gameObject;
            child.SetActive(true);
            playableDirectorBuffs.Play();
            sceneSix = false;
            sceneSeven = true;
        }

        if (playableDirectorBuffs.state == PlayState.Paused == true && sceneSeven == true)
        {
            dialogue.lines = linesStatbuff;
            StartSpeaking();
            sceneSeven = false;
            sceneEight = true;
        }

        if (playableDirectorEnd.state != PlayState.Playing && sceneEight == true && dialogue.inprogress == false)
        {
            GameObject child = playableDirectorEnd.transform.GetChild(0).gameObject;
            child.SetActive(true);
            playableDirectorEnd.Play();
            sceneEight = false;
            sceneNine = true;
        }

        if (playableDirectorEnd.state == PlayState.Paused == true && sceneNine == true)
        {
            sceneSwitch.currentScene = 1;
            sceneSwitch.LoadScene();
        }



    }

    void StartSpeaking()
    {
        dialogue.StartDialogue();
    }

    public void play()
    {
        //playableDirector.Play();

    }
}
