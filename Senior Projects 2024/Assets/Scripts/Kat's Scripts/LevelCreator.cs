using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//created by Kat Wayman
//code provided by sunney valley studio on youtube
public class LevelCreator : MonoBehaviour
{
    public static LevelCreator inst;
    public int levelWidth, levelLength;
    public int roomWidthMin, roomLengthMin;
    public int roomWidthMax, roomLengthMax;
    public float doorYHeight;
    public float doorx, doory, doorz;

    [Range(0.0f, 0.9f)]
    public float roomBottomCornerModifier;

    [Range(0.7f, 1.0f)]
    public float roomTopCornerModifier;

    [Range(0.0f, 15.0f)]
    public int roomOffset;

    public int maxIterations;

    public GameObject verticalWall, horizontalWall;
    public GameObject verticalDoor, horizontalDoor;

    public Material material;
    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    public void CreateLevel()
    {
        DestroyAllChildren();
        LevelGenerator generator = new LevelGenerator(levelWidth, levelLength);
        var listRooms = generator.CalculateLevel(maxIterations, roomWidthMin, roomLengthMin, roomWidthMax, roomLengthMax,
            roomBottomCornerModifier, roomTopCornerModifier, roomOffset);
        //GameObject wallParent = new GameObject("WallParent");
        //wallParent.transform.parent = transform;
        for (int i = 0; i < listRooms.Count; i++)
        {
            CreateMesh(listRooms[i].BottomLeftAreaCorner, listRooms[i].TopRightAreaCorner);
        }
    }

    public List<NewRoom> rooms;
    public List<GameObject> enemyPrefabs;
    //assumes create level has been called already
    public void GetRooms()
    {
        rooms = new List<NewRoom>(GetComponentsInChildren<NewRoom>());
    }

    public void SetUpRooms()
    {
        foreach (NewRoom room in rooms)
        {
            Debug.Log(rooms.Count);
            room.GenerateSpawnPositions();
            room.enemyPrefabs = enemyPrefabs;
        }
    }

    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        GameObject roomParent = new GameObject("Room");
        NewRoom room = roomParent.AddComponent<NewRoom>();    
        roomParent.transform.parent = transform;
        room.dimensions = new Vector2(topRightCorner.x - bottomLeftCorner.x, topRightCorner.y - bottomLeftCorner.y);
        room.location = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        room.playerSpawnPoint = room.location + new Vector3(10, 0, 10);

        Vector3 bottomLeftVertex = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightVertex = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftVertex = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightVertex = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftVertex, topRightVertex, bottomLeftVertex, bottomRightVertex
        };

        Vector2[] uvs = new Vector2[vertices.Length];

        float roomWidth = topRightCorner.x - bottomLeftCorner.x;
        float roomDepth = topRightCorner.y - bottomLeftCorner.y;
        float textureScale = 0.1f;

        uvs[0] = new Vector2(0f, roomDepth * textureScale);       // Top Left
        uvs[1] = new Vector2(roomWidth * textureScale, roomDepth * textureScale);    // Top Right
        uvs[2] = new Vector2(0f, 0f);                            // Bottom Left
        uvs[3] = new Vector2(roomWidth * textureScale, 0f);     // Bottom Right
        //for (int i = 0; i < uvs.Length; i++)
        //{
        //    uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        //}

        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
        };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GameObject levelFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));

        levelFloor.transform.position = Vector3.zero;
        levelFloor.transform.localScale = Vector3.one;
        levelFloor.GetComponent<MeshFilter>().mesh = mesh;
        levelFloor.GetComponent<MeshRenderer>().material = material;
        levelFloor.transform.parent = roomParent.transform;
        BoxCollider collider = levelFloor.AddComponent<BoxCollider>();
        //collider.size = new Vector3(collider.size.x, 0.1f, collider.size.z);
        collider.size = new Vector3(roomWidth, 0.1f, roomDepth);
        collider.center = new Vector3(bottomLeftCorner.x + roomWidth / 2f, -0.05f, bottomLeftCorner.y + roomDepth / 2f);

        CreateWall(bottomLeftVertex, bottomRightVertex, horizontalWall, true, roomParent.transform); //southmost wall

        CreateWall(topLeftVertex, topRightVertex, horizontalWall, false, roomParent.transform); //northmost wall
        CreateDoor(topLeftVertex, topRightVertex, horizontalDoor, roomParent.transform);

        CreateWall(bottomLeftVertex, topLeftVertex, verticalWall, true, roomParent.transform);//westmost wall
        CreateWall(bottomRightVertex, topRightVertex, verticalWall, false, roomParent.transform); //eastmostwall
        CreateDoor(bottomRightVertex, topRightVertex, verticalDoor, roomParent.transform);
    }
    private void CreateWall(Vector3 pointA, Vector3 pointB, GameObject wallPrefab, bool makeInvisible, Transform parent)
    {
        Vector3 wallPosition = (pointA + pointB)/2;
        float length = Vector3.Distance(pointA, pointB);
        bool isHorizontal = pointA.z == pointB.z;

        wallPosition.y = 6.35f;
        GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity,transform);
        wall.transform.localScale = isHorizontal 
            ? new Vector3(length, wall.transform.localScale.y,1) : 
            new Vector3(1, wall.transform.localScale.y, length);
        if (makeInvisible && wall.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
        {
            renderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
        wall.transform.parent = parent;
    }

    private void CreateDoor(Vector3 pointA, Vector3 pointB, GameObject doorPrefab, Transform parent)
    {
        Vector3 doorPosition = (pointA + pointB)/2;
        bool isHorizontal = pointA.z == pointB.z;
        doorPosition.y = doorYHeight; //anything higher than this makes the doors float
        GameObject door = Instantiate(doorPrefab, doorPosition, Quaternion.identity,transform);
        if (isHorizontal) //since i have a test of bool for if the wall is horizontal, and the horizontal walls are ideal, this is the best method to simply rotate the doors when the walls are vertical
        {
            door.transform.Rotate(0,0,0); 
        }
        else
        {
            door.transform.Rotate(0, 90, 0);
        }
        door.transform.localScale = isHorizontal //changed the ratios for the doors to fit the rooms
            ? new Vector3(doorx, doory, doorz) 
            : new Vector3(doorx,doory,doorz);

        door.transform.parent = parent;

        NewRoom room= door.GetComponentInParent<NewRoom>();
        if (room != null)
        {
            NewDoor doorScript = door.AddComponent<NewDoor>();
            doorScript.room = room;
            doorScript.locked = true;
            room.doors.Add(doorScript);
        }
    }
    private void DestroyAllChildren()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }

}
