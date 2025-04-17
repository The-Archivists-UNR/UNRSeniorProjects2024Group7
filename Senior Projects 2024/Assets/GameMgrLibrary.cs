//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameMgrLvl : MonoBehaviour
//{
//    public static GameMgr inst;
//    public List<int> roomIndexes;
//    public List<Room> rooms;
//    public int numRooms;
//    public int currentRoom;
//    [HideInInspector] public bool loadNext;
//    public GameObject player;
//    [HideInInspector] public bool endReached = false;

//    private void Awake()
//    {
//        // inst = this;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        roomIndexes = new List<int>();
//        for (int i = 0; i < numRooms - 1; i++)
//        {
//            int roomIndex = Random.Range(0, rooms.Count);
//            roomIndexes.Add(roomIndex);
//        }
//        roomIndexes.Add(rooms.Count - 1);
//        currentRoom = -1;
//        currentRoom++;
//        loadNext = true;
//        endReached = false;
//        Time.timeScale = 0;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (currentRoom == numRooms)
//        {
//            if (SceneManager.GetActiveScene().name == "occultlevels")
//                SceneManager.LoadScene(2);
//            if (SceneManager.GetActiveScene().name == "KitchenLevels")
//                SceneManager.LoadScene(9);
//            // endReached = true;

//        }
//        if (loadNext == true && endReached == false)
//        {
//            NextRoom();
//            loadNext = false;
//        }
//    }

//    public void NextRoom()
//    {
//        // currentRoom++;
//        rooms[roomIndexes[currentRoom]].EnterRoom(player.transform, 1, 3, 2);
//    }
//}