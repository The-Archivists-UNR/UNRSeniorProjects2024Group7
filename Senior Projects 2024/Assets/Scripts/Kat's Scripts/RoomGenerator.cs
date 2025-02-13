using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//created by Kat Wayman
//code provided by sunney valley studio on youtube
public class RoomGenerator : MonoBehaviour
{
    public int levelWidth, levelLength;
    public int roomWidthMin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        LevelGenerator generator = new LevelGenerator(levelWidth, levelLength);
        var listRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
