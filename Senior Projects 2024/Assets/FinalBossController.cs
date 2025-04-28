using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    public static FinalBossController inst;

    public List<GameObject> spawns;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemies;
    public int currentWave = 0;
    public int numWaves;
    public int numEnemies;
    public List<int> crystalWaves;
    public List<Crystal> crystalObjects;
    public GameObject boss;

    public PanelMover gameOver;
    public PanelMover gameWon;

    bool crystalSet;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave < numWaves && enemies.Count == 0)
        {
            if(!crystalWaves.Contains(currentWave))
            {
                currentWave++;
                SpawnWave(1, 4);
            }
            else
            {
                if (!crystalSet)
                {
                    int index = crystalWaves.IndexOf(currentWave);
                    crystalObjects[index].MakeHitable();
                    crystalSet = true;
                }
            }
        }

        for (int i = enemies.Count - 1; i > -1; i--)
        {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }

        if (currentWave == numWaves)
        {
            gameWon.isVisible = true;
            boss.SetActive(false);
            //Time.timeScale = 0;
        }
        if (PlayerMgr.inst.player.health <= 0)
        {
            gameOver.isVisible = true;
            //Time.timeScale = 0;
        }
    }


    public bool bossDead = false;

    public void IncreaseWaveFromCrystal()
    {
        currentWave++;
        crystalSet = false;
    }

    public void SpawnWave(int minEnemies, int maxEnemies)
    {
        Debug.Log("in spawn");
        int upperBound = Mathf.Min(maxEnemies, spawns.Count);
        int numToSpawn = Random.Range(minEnemies, upperBound);
        List<GameObject> spawnsToUse = new List<GameObject>(spawns);
        enemies = new List<GameObject>();
        for (int i = 0; i < numToSpawn; i++)
        {
            int spawnPosIndex = Random.Range(0, spawnsToUse.Count);
            int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject newEnemy = Instantiate(enemyPrefabs[enemyPrefabIndex], spawnsToUse[spawnPosIndex].transform.position, Quaternion.identity);
            enemies.Add(newEnemy);
            newEnemy.SetActive(true);
            spawnsToUse.RemoveAt(spawnPosIndex);
        }
    }
}
