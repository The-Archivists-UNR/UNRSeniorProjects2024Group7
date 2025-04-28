using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMgr : MonoBehaviour
{
    public static SaveMgr inst;
    public GameData gameData;
    private FileDataHandler dataHandler;
    public bool useEncryption = false;
    public string saveName;
    public string playerName;
    public NPCController kid;
    public NPCController ghost;
    public NPCController detective;
    public NPCController dude;
    

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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

        }
    }
    public void SaveData()
    {   
        if (GameStatsMgr.inst != null)
        {
            gameData.enemiesKilled = GameStatsMgr.inst.enemiesKilled;
            gameData.timePlayed = GameStatsMgr.inst.timePlayed;
        }
        gameData.playerName = playerName;
        if(kid != null)
        {
            List<string> memory = kid.dialogueTranscript;
            gameData.kidConvo = memory;
            gameData.kidMemory = kid.memory;
            gameData.kidRelationshipScore = kid.relationshipScore;
        }
        if (detective != null)
        {
            List<string> memory = detective.dialogueTranscript;
            gameData.detectiveConvo = memory;
            gameData.detectiveMemory = detective.memory;
            gameData.detectiveRelationshipScore = detective.relationshipScore;
        }
        if (ghost != null)
        {
            List<string> memory = ghost.dialogueTranscript;
            gameData.ghostConvo = memory;
            gameData.ghostMemory = ghost.memory;
            gameData.ghostRelationshipScore = ghost.relationshipScore;
        }

        if (dude != null)
        {
            List<string> memory = dude.dialogueTranscript;
            gameData.dudeConvo = memory;
            gameData.dudeMemory = dude.memory;
            gameData.detectiveRelationshipScore = dude.relationshipScore;
        }

        if (OpheliaStats.inst != null)
        {
            gameData.ogHP = OpheliaStats.inst.ogHP;
            gameData.ogSpeed = OpheliaStats.inst.ogSpeed;
            gameData.ogDamage = OpheliaStats.inst.ogDamage;
            gameData.HpPercent = OpheliaStats.inst.HpPercent;
            gameData.SpeedPercent = OpheliaStats.inst.SpeedPercent;
            gameData.AttackPercent = OpheliaStats.inst.AttackPercent;
        }

        if(HealthImprovement.inst != null)
        {
            gameData.healthCounter = HealthImprovement.inst.healthCounter;
            gameData.healthMaxBuff = HealthImprovement.inst.maxBuff;
        }

        if(SpeedImprovement.inst != null)
        {
            gameData.speedCounter = SpeedImprovement.inst.speedCounter;
            gameData.speedMaxBuff = SpeedImprovement.inst.maxBuff;
        }

        if(AttackImprovement.inst != null)
        {
            gameData.attackCounter = AttackImprovement.inst.attackCounter;
            gameData.attackMaxBuff = AttackImprovement.inst.maxBuff;
        }

        if(CollectableManagers.inst != null)
        {
            gameData.previouslyActiveBonnie = CollectableManagers.inst.previouslyActiveBonnie;
            gameData.previouslyActiveKirk = CollectableManagers.inst.previouslyActiveKirk;
            gameData.previouslyActiveOswald = CollectableManagers.inst.previouslyActiveOswald;
            gameData.previouslyActiveJulie = CollectableManagers.inst.previouslyActiveJulie;
        }

        if(moneyMgr.inst != null)
        {
            gameData.currency = moneyMgr.inst.currency;
        }

        if(LibraryLock.inst != null)
        {
            gameData.isFantasyCompleted = LibraryLock.inst.isFantasyCompleted;
            gameData.isNoireCompleted = LibraryLock.inst.isNoireCompleted;
            gameData.isScifiCompleted = LibraryLock.inst.isScifiCompleted;
        }

        dataHandler.Save(gameData, saveName);
    }

    public void LoadData()
    {
        gameData = dataHandler.Load(saveName);
        if(GameStatsMgr.inst != null)
        {
            GameStatsMgr.inst.enemiesKilled = gameData.enemiesKilled;
            GameStatsMgr.inst.timePlayed = gameData.timePlayed;
        }
        if(gameData !=  null)
        {
            playerName = gameData.playerName;
        }
        
        if (kid != null)
        {
            
            kid.dialogueTranscript = gameData.kidConvo;
            kid.memory = gameData.kidMemory;
            kid.relationshipScore = gameData.kidRelationshipScore;
        }
        if (detective != null)
        {
            detective.dialogueTranscript = gameData.detectiveConvo;
            detective.memory = gameData.detectiveMemory;
            detective.relationshipScore = gameData.detectiveRelationshipScore;
        }
        if (ghost != null)
        {
            Debug.Log("does it work");
            ghost.dialogueTranscript = gameData.ghostConvo;
            ghost.memory = gameData.ghostMemory;
            ghost.relationshipScore = gameData.ghostRelationshipScore;
        }

        if (dude != null)
        {
            dude.dialogueTranscript = gameData.dudeConvo;
            dude.memory = gameData.dudeMemory;
            dude.relationshipScore = gameData.dudeRelationshipScore;
        }

        if (OpheliaStats.inst != null)
        {
            OpheliaStats.inst.ogHP = gameData.ogHP;
            OpheliaStats.inst.ogSpeed = gameData.ogSpeed;
            OpheliaStats.inst.ogDamage = gameData.ogDamage;
            OpheliaStats.inst.HpPercent = gameData.HpPercent;
            OpheliaStats.inst.SpeedPercent = gameData.SpeedPercent;
            OpheliaStats.inst.AttackPercent = gameData.AttackPercent;
        }

        if (moneyMgr.inst != null)
            moneyMgr.inst.currency = gameData.currency;

        if (HealthImprovement.inst != null)
        {
            HealthImprovement.inst.healthCounter = gameData.healthCounter;
            HealthImprovement.inst.maxBuff = gameData.healthMaxBuff;
            HealthImprovement.inst.UpdateHealth();
            HealthImprovement.inst.coinText.UpdateMoneyText();
        }

        if (SpeedImprovement.inst != null)
        {
            SpeedImprovement.inst.speedCounter = gameData.speedCounter;
            SpeedImprovement.inst.maxBuff = gameData.speedMaxBuff;
            SpeedImprovement.inst.UpdateSpeed();
        }

        if (AttackImprovement.inst != null)
        {
            AttackImprovement.inst.attackCounter = gameData.attackCounter;
            AttackImprovement.inst.maxBuff = gameData.attackMaxBuff;
            AttackImprovement.inst.updateAttack();
        }

        if (CollectableManagers.inst != null)
        {
            CollectableManagers.inst.previouslyActiveBonnie = gameData.previouslyActiveBonnie;
            CollectableManagers.inst.previouslyActiveKirk = gameData.previouslyActiveKirk;
            CollectableManagers.inst.previouslyActiveOswald = gameData.previouslyActiveOswald;
            CollectableManagers.inst.previouslyActiveJulie = gameData.previouslyActiveJulie;
        }

        if (LibraryLock.inst != null)
        {
            LibraryLock.inst.isFantasyCompleted = gameData.isFantasyCompleted;
            LibraryLock.inst.isNoireCompleted = gameData.isNoireCompleted;
            LibraryLock.inst.isScifiCompleted = gameData.isScifiCompleted;
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
        moneyMgr.inst.currency = 300;
        OpheliaStats.inst.ogDamage = 15;
        OpheliaStats.inst.ogSpeed = 15;
        OpheliaStats.inst.ogHP = 100;
        OpheliaStats.inst.HpPercent = 1;
        OpheliaStats.inst.SpeedPercent = 1;
        OpheliaStats.inst.AttackPercent = 1;
        gameData.playerName = playerName;
        string path = Path.Combine(Application.persistentDataPath, "saves");
        Debug.Log(path);
        dataHandler = new FileDataHandler(path, useEncryption);
        SaveData();
        SceneSwitch.inst.currentScene = 11;
        SceneSwitch.inst.LoadScene();
    }

    public void LoadSaveSlot()
    {
        if (saveSelect.inst.slots[saveSelect.inst.saveSlotNum - 1].GetComponent<UIHover>().slotExists)
        {
            
            saveName = "slot" + saveSelect.inst.saveSlotNum + ".game";
            LoadData();
            SceneSwitch.inst.LoadScene();
        }
    }
}
