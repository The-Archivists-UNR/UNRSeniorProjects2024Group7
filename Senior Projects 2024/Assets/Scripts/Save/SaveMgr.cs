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
        dataHandler.Save(gameData, saveName);
    }

    public void LoadData()
    {
        gameData = dataHandler.Load(saveName);
        GameStatsMgr.inst.enemiesKilled = gameData.enemiesKilled;
    }
}
