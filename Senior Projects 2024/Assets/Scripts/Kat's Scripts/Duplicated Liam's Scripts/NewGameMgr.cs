using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//modified from liam's script
//modified code will be identified
// new contributions from kat wayman

public class NewGameMgr : MonoBehaviour
{

    public static NewGameMgr inst;
    public List<int> roomIndexes;
    public List<NewRoom> rooms;
    public int numRooms;
    public int currentRoom;
    public NewRoom BossRoom;
    public PanelMover gameOver;
    public PanelMover gameWon;
    public bool loadNext;
    bool inLastRoom;
    private void Awake()
    {
        inst = this;
    }

    // initializes the order of which rooms will appear and initialized variables for counting what room the player is in.
    void Start()
    {
        roomIndexes = new List<int>();
        for (int i = 0; i < numRooms - 1; i++)
        {
            int roomIndex = Random.Range(0, rooms.Count);
            roomIndexes.Add(roomIndex);
        }
        roomIndexes.Add(rooms.Count - 1);
        currentRoom = -1;
        loadNext = true;
    }

    // checks whether or not the current room is in a valid state for the next room to be loaded.
    void Update()
    {
        if (loadNext)
        {
            NextRoom();
            loadNext = false;
        }
        if ((inLastRoom && BossRoom.currentWave == BossRoom.numWaves && BossRoom.enemies.Count == 0))
        {
            gameWon.isVisible = true;
            //Time.timeScale = 0;
        }
        if (PlayerMgr.inst.player.health <= 0)
        {
            gameOver.isVisible = true;
            //Time.timeScale = 0;
        }

    }

    // loads the next room
    public void NextRoom()
    {
        currentRoom++;
        if (currentRoom >= numRooms) //kat 
        {
            EnterBossRoom(); //kat
            return;
        }

        for (int i = currentRoom; i < rooms.Count; i++)
        {
            if (!rooms[roomIndexes[i]].isVisited)
            {
                rooms[roomIndexes[i]].EnterRoom(PlayerMgr.inst.player.transform, Camera.main.transform, 1, 3, 2);
                currentRoom = i;
                return;
            }
        }

    }

    // loads the boss room
    public void EnterBossRoom()
    {
        BossRoom.EnterRoom(PlayerMgr.inst.player.transform, Camera.main.transform, 1, 1, 1);
        inLastRoom = true;
    }
}