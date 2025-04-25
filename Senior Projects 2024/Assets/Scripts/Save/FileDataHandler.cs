using System.IO;
using System;
using UnityEngine.Playables;
using UnityEngine;
using System.Collections.Generic;

public class FileDataHandler
{
    private string dataDirPath = "";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "ECSL";

    public FileDataHandler(string dataDirPath, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileID)
    {
        string fullPath = Path.Combine(dataDirPath, profileID);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data, string profileID)
    {
        string fullPath = Path.Combine(dataDirPath, profileID);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}

[System.Serializable]
public class GameData
{
    public bool savedBefore = false;
    public string playerName;
    public int enemiesKilled;
    public float timePlayed;

    public string ghostMemory;
    public string kidMemory;
    public string detectiveMemory;
    public string dudeMemory;
    public List<string> ghostConvo;
    public List<string> kidConvo;
    public List<string> detectiveConvo;
    public List<string> dudeConvo;
    public int ghostRelationshipScore;
    public int kidRelationshipScore;
    public int detectiveRelationshipScore;
    public int dudeRelationshipScore;

    public float ogHP;
    public float ogSpeed;
    public float ogDamage;
    public float HpPercent;
    public float SpeedPercent;
    public float AttackPercent;

    public int healthCounter;
    public int healthMaxBuff;
    public int attackCounter;
    public int attackMaxBuff;
    public int speedCounter;
    public int speedMaxBuff;

    public bool previouslyActiveJulie;
    public bool previouslyActiveKirk;
    public bool previouslyActiveOswald;
    public bool previouslyActiveBonnie;

    public int currency;

    public bool isFantasyCompleted;
    public bool isNoireCompleted;
    public bool isScifiCompleted;
}