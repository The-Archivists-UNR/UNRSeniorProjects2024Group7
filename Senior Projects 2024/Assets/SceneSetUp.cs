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
    public Weapon opheliaWeapon;
    public string sceneMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioMgr.inst.PlayMusic(sceneMusic);
        Entity opheliaEntity = player.GetComponent<Entity>();
        OpheliaStats.inst.AppplyStats(opheliaEntity, opheliaWeapon);
        LevelCreator.inst.CreateLevel();
        LevelCreator.inst.GetRooms();
        int maxIterations = 0;
        while (LevelCreator.inst.rooms.Count < 5 && maxIterations < 100)
        {
            LevelCreator.inst.CreateLevel();
            LevelCreator.inst.GetRooms();
        }
        LevelCreator.inst.SetUpRooms();
        NewGameMgr.inst.rooms = LevelCreator.inst.rooms.OrderBy(x => Random.value).ToList();
        NewGameMgr.inst.numRooms = Random.Range(2, 3);
        if(roomItemPlacement != null && roomItemPlacement.prefabs.Count != 0)
        {
            foreach (NewRoom room in NewGameMgr.inst.rooms)
            {
                GameObject itemTemp;
                roomItemPlacement.decorationsParent = room.gameObject.transform;
                roomItemPlacement.roomPosition = room.location;
                roomItemPlacement.roomSize = new Vector3(room.dimensions.x, 0, room.dimensions.y);
                roomItemPlacement.PlacePrefabs();
                itemTemp = roomItemPlacement.PlaceItems();
                room.itemHolder = itemTemp;
            }
        }
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
