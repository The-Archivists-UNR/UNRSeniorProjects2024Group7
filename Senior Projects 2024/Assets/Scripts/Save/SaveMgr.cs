using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveMgr : MonoBehaviour
{
    public static SaveMgr inst;
    private GameData gameData;
    private FileDataHandler dataHandler;
    public bool useEncryption = false;
    public string saveName;
    public string playerName;
    public NPCController kid;
    public NPCController ghost;
    public NPCController detective;

    // Start is called before the first frame update
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
            gameData = new GameData();
            string path = Path.Combine(Application.persistentDataPath, "saves");
            Debug.Log(path);
            dataHandler = new FileDataHandler(path, useEncryption);
        }
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SaveData();
        if(Input.GetKeyDown(KeyCode.Alpha2))
            LoadData();
    }
    public void SaveData()
    {
        gameData.enemiesKilled = GameStatsMgr.inst.enemiesKilled;
        gameData.timePlayed = GameStatsMgr.inst.timePlayed;
        gameData.playerName = playerName;
        if(kid != null)
        {
            List<string> memory = kid.dialogueTranscript;
            gameData.kidConvo = memory;
            gameData.kidMemory = kid.memory;
        }
        if (detective != null)
        {
            List<string> memory = detective.dialogueTranscript;
            gameData.detectiveConvo = memory;
            gameData.detectiveMemory = detective.memory;
        }
        if (ghost != null)
        {
            List<string> memory = ghost.dialogueTranscript;
            gameData.ghostConvo = memory;
            gameData.ghostMemory = ghost.memory;
        }

        dataHandler.Save(gameData, saveName);
    }

    public void LoadData()
    {
        gameData = dataHandler.Load(saveName);
        GameStatsMgr.inst.enemiesKilled = gameData.enemiesKilled;
        GameStatsMgr.inst.timePlayed = gameData.timePlayed;
        playerName = gameData.playerName;
        if (kid != null)
        {
            kid.dialogueTranscript = gameData.kidConvo;
            kid.memory = gameData.kidMemory;
        }
        if (detective != null)
        {
            detective.dialogueTranscript = gameData.detectiveConvo;
            detective.memory = gameData.detectiveMemory;
        }
        if (ghost != null)
        {
            ghost.dialogueTranscript = gameData.ghostConvo;
            ghost.memory = gameData.ghostMemory;
        }

    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void NewSaveSlot()
    {
        saveName = "slot" + saveSelect.inst.saveSlotNum + ".game";
        gameData = new GameData();
        gameData.playerName = playerName;
        string path = Path.Combine(Application.persistentDataPath, "saves");
        Debug.Log(path);
        dataHandler = new FileDataHandler(path, useEncryption);
        SceneSwitch.inst.LoadScene();
    }
}
