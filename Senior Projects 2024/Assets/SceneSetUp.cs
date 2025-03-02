using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SceneSetUp : MonoBehaviour
{
    public NavMeshSurface surface;
    public GameObject player;
    public RoomItemPlacement roomItemPlacement;
    
    // Start is called before the first frame update
    void Start()
    {
        LevelCreator.inst.CreateLevel();
        LevelCreator.inst.GetRooms();
        while (LevelCreator.inst.rooms.Count < 5)
        {
            LevelCreator.inst.CreateLevel();
            LevelCreator.inst.GetRooms();
        }
        LevelCreator.inst.SetUpRooms();
        NewGameMgr.inst.rooms = LevelCreator.inst.rooms.OrderBy(x => Random.value).ToList();
        NewGameMgr.inst.numRooms = Random.Range(5, NewGameMgr.inst.rooms.Count);
        foreach(NewRoom room in NewGameMgr.inst.rooms)
        {
            roomItemPlacement.roomPosition = room.location;
            roomItemPlacement.roomSize = new Vector3(room.dimensions.x, 0 , room.dimensions.y);
            roomItemPlacement.PlacePrefabs();
        }

        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
