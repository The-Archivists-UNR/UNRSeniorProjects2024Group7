using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//created by Kat Wayman
//code provided by sunney valley studio on youtube
public class LevelCreator : MonoBehaviour
{
    public int levelWidth, levelLength;
    public int roomWidthMin, roomLengthMin;
    //public int roomWidthMax, roomLengthMax;

    [Range(0.0f, 0.9f)]
    public float roomBottomCornerModifier;

    [Range(0.7f, 1.0f)]
    public float roomTopCornerModifier;

    [Range(0.0f, 5.0f)]
    public int roomOffset;

    public int maxIterations;

    public GameObject verticalWall, horizontalWall;
    List<Vector3Int> possibleDoorVerticalPosition;
    List<Vector3Int> possibleDoorHorizontalPosition;
    List<Vector3Int> possibleWallVerticalPosition;
    List<Vector3Int> possibleWallHorizontalPosition;


    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        DestroyAllChildren();
        LevelGenerator generator = new LevelGenerator(levelWidth, levelLength);
        var listRooms = generator.CalculateLevel(maxIterations, roomWidthMin, roomLengthMin,
            roomBottomCornerModifier, roomTopCornerModifier, roomOffset);
        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
        possibleDoorVerticalPosition = new List<Vector3Int>();
        possibleDoorHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();
        possibleWallHorizontalPosition = new List<Vector3Int>();
        for (int i = 0; i < listRooms.Count; i++)
        {
            CreateMesh(listRooms[i].BottomLeftAreaCorner, listRooms[i].TopRightAreaCorner);
        }
    }

    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeftVertex = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightVertex = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftVertex = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightVertex = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftVertex, topRightVertex, bottomLeftVertex, bottomRightVertex
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

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

        GameObject levelFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));

        levelFloor.transform.position = Vector3.zero;
        levelFloor.transform.localScale = Vector3.one;
        levelFloor.GetComponent<MeshFilter>().mesh = mesh;
        levelFloor.GetComponent<MeshRenderer>().material = material;
        levelFloor.transform.parent = transform;

        CreateWall(bottomLeftVertex, bottomRightVertex, horizontalWall); //southmost wall
        CreateWall(topLeftVertex, topRightVertex, horizontalWall); //northmost wall
        CreateWall(bottomLeftVertex, topLeftVertex, horizontalWall);//westmost wall
        CreateWall(bottomRightVertex, topRightVertex, horizontalWall); //eastmostwall
    }
    private void CreateWall(Vector3 pointA, Vector3 pointB, GameObject wallPrefab)
    {
        Vector3 wallPosition = (pointA + pointB)/2;
        float length = Vector3.Distance(pointA, pointB);
        bool isHorizontal = pointA.z == pointB.z;
        GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity,transform);
        wall.transform.localScale = isHorizontal ? new Vector3(length, wall.transform.localScale.y,1) : 
            new Vector3(1, wall.transform.localScale.y, length);
    }
    //private void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    //{
    //    Vector3Int point = Vector3Int.CeilToInt(wallPosition);
    //    if (wallList.Contains(point))
    //    {
    //        doorList.Add(point);
    //        wallList.Remove(point);
    //    }
    //    else
    //    {
    //        wallList.Add(point);
    //    }
    //}
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
