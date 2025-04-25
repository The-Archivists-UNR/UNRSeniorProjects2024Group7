using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogMgr : MonoBehaviour
{
    public TextMeshProUGUI content;
    public GameObject menu;
    public GameObject optionsScreen;
    public GameObject contentScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha3))
        {
            ToggleMenu();
        }
    }

    public void OpenLog(NPCController npc)
    {
        SaveMgr.inst.LoadData();
        contentScreen.SetActive(true);
        DisplayLog(npc);
        optionsScreen.SetActive(false);
    }

    public void CloseLog()
    {
        optionsScreen.SetActive(true);
        contentScreen.SetActive(false);        
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void DisplayLog(NPCController npc)
    {
        string display = "";

        foreach(string line in npc.dialogueTranscript)
        {
            display += "\n" + line;
        }

        content.text = display;
    }
}
