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
    string[] linesMinigames = {"Here you'll find different minigames you can play.", "Each was intended to making working the library fun!"};
    string[] linesStatbuff = {"Lastly, at your desk you'll find where you can buy stat increases!"};

    bool greetingStart = false;
    bool StartDone = false;
    bool NPCDone = false;
    bool MinigamesDone = false;
    bool BuffDone = false;

    //Wow this is super redudant but i am brainfried and can't think of a better way to do this
    bool sceneOne = false;
    bool sceneTwo = false;
    bool sceneThree = false;
    bool sceneFour = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print("state:" + playableDirectorStart.state);
        if(playableDirectorStart.state == PlayState.Paused && sceneOne == false)
        {
            //print("This is being sent through");
            greetingStart = true;
            sceneOne = true;
        }
        if (greetingStart == true && dialogue.inprogress == false)
        {
            //print("This is being sent through");
            dialogue.StartDialogue();
            StartDone = true;
            greetingStart = false;
        }

        if (StartDone == true && dialogue.inprogress == false)
        {
            //print("This is being sent through");
            dialogue.lines = linesNPCS;
            dialogue.StartDialogue();
            NPCDone = true;
            StartDone = false;
        }


    }

    public void play()
    {
        //playableDirector.Play();

    }
}
