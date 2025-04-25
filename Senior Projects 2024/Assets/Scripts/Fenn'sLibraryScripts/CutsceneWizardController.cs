using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneWizardController : MonoBehaviour
{

    public CutsceneDialogue dialogue;
    public SceneSwitch sceneMgr;
    bool sceneStart = false;
    // Start is called before the first frame update
    void Start()
    {
        //dialogue.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStart == false)
        {
            dialogue.StartDialogue();
            sceneStart = true;
        }
        if(dialogue.inprogress == false)
        {
            sceneMgr.currentScene = 5;
            sceneMgr.LoadScene();
        }
        
    }
}
