using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCController : MonoBehaviour
{
    new public string name;
    public Vector3 spawnPos;
    public int relationshipScore;
    public string scoreSavingFile;
    public string prompt;
    public Boolean willingToTalk = true;
    public List<string> dialogueTranscript;
    public string memory;
    
    // Start is called before the first frame update
    void Start()
    {
        memory = "";
        dialogueTranscript = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeRelationshipScore(int increase) //use negative value to decrease
    {
        relationshipScore += increase;
    }
}
