using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SceneSetUp : MonoBehaviour
{
    public NavMeshSurface surface;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        LevelCreator.inst.CreateLevel();
        LevelCreator.inst.GetRooms();
        LevelCreator.inst.SetUpRooms();
        NewGameMgr.inst.rooms = LevelCreator.inst.rooms.OrderBy(x => Random.value).ToList();
        NewGameMgr.inst.numRooms = Random.Range(5, NewGameMgr.inst.rooms.Count);
        //player.SetActive(false);
        surface.BuildNavMesh();
        //player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
