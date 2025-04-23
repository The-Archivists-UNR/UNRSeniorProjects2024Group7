using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    public List<GameObject> spawns;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
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
