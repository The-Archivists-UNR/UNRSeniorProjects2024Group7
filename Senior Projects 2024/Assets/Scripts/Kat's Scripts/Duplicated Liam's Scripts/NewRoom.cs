using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// altered script from liam to account for procedurally generated rooms.
public class NewRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isVisited = false; //kat
    public bool enemiesDead = false;
    public Vector2 dimensions;
    public Vector3 location;
    public List<GameObject> spawns;
    public Vector3 playerSpawnPoint;
    public bool inRoom;
    public int minEnemies;
    public int maxEnemies;
    public int numWaves;
    public int currentWave;
    public List<GameObject> enemyPrefabs;
    public List<NewDoor> doors = new List<NewDoor>();
    public List<GameObject> enemies = new List<GameObject>();
    public bool overrideValues;

    //initializes all the potential positions an enemy can spawn in
    void Start()
    {
        if (!overrideValues)
        {
            minEnemies = Random.Range(1, 2);
            maxEnemies = Random.Range(minEnemies + 1, 3);
            numWaves = Random.Range(1, 2);
        }
    }

    /*
    public void GenerateSpawnPositiions()
    {
        float x, z;
        GameObject spawnParents = new GameObject("Spawn Parents");
        spawnParents.transform.parent = transform;
        spawnParents.transform.localPosition = location;

        for (int i = 0; i < 10; i++)
        {
            float minDist;
            GameObject newSpawn = new GameObject("Spawn");
            newSpawn.transform.parent = spawnParents.transform;

            int count = spawns.Count;

            while(spawns.Count < count)
            {
                minDist = 1000;
                x = Random.Range(10, dimensions.x - 10);
                z = Random.Range(10, dimensions.y - 10);
                newSpawn.transform.localPosition = new Vector3(x, 0, z);
                foreach (GameObject spawn in spawns)
                {
                    float dist = Vector3.Distance(spawn.transform.position, newSpawn.transform.position);
                    if (dist < minDist)
                        minDist = dist;
                }
                if(minDist > 10)
                    spawns.Add(newSpawn);
            }
        }
    }
    */

    public void GenerateSpawnPositions()
    {
        float x, z;
        GameObject spawnParents = new GameObject("Spawns Parent");
        spawnParents.transform.parent = transform;
        spawnParents.transform.localPosition = location;
        spawns = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            x = Random.Range(10, dimensions.x - 10);
            z = Random.Range(10, dimensions.y - 10);
            Vector3 newPosition = new Vector3(x, 0, z);

            GameObject newSpawn = new GameObject("Spawn");
            newSpawn.transform.parent = spawnParents.transform;
            newSpawn.transform.localPosition = newPosition;
            spawns.Add(newSpawn);
        }
    }

    //spawns the player in the room and initializes the current wave to zero
    public void EnterRoom(Transform player)
    {
        if (!isVisited)
        {
            isVisited = true; //kat
            inRoom = true;
            player.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            player.position = playerSpawnPoint;
            player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            currentWave = 0;
            SetDoors(true);
        }
    }

    public void ExitRoom(Transform player, Transform camera, int minEnemies, int maxEnemies)
    {
        inRoom = false;
    }

    //spawns a new wave in the room after a previous wave has been defeated
    public void SpawnWave()
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
            spawnsToUse.RemoveAt(spawnPosIndex);
        }
    }

    //locks/unlocks all the doors in the room allowing/preventing the player from interacting with the door
    public void SetDoors(bool locked)
    {
        foreach (NewDoor door in doors)
        {
            door.locked = locked;
        }
    }

    // checks whether or not to spawn a new wave of enemies and whether or not to unlock the doors in the room
    void Update()
    {
        if (inRoom)
        {
            if (currentWave < numWaves && enemies.Count == 0)
            {
                currentWave++;
                SpawnWave();
            }

            for (int i = enemies.Count - 1; i > -1; i--)
            {
                if (enemies[i] == null)
                    enemies.RemoveAt(i);
            }

            if (currentWave == numWaves && enemies.Count == 0)
            {
                SetDoors(false);
                enemiesDead = true;
                //NewGameMgr.inst.loadNext = true;
            }
        }
    }
}
