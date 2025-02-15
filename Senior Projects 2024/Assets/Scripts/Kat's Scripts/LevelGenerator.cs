using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//created by Kat Wayman
//code provided by sunney valley studio on youtube
//i pretty faithfully followed this youtube tutorial compared to other tutorials used to teach myself unity because procedural generation is
//hard TT
public class LevelGenerator
{
    List<RoomNode> allNodesCollection = new List<RoomNode>();
    private int levelWidth;
    private int levelLength;
    public LevelGenerator(int levelWidth, int levelLength)
    {
        this.levelWidth = levelWidth;
        this.levelLength = levelLength;

    }

    public List<Node> CalculateLevel(int maxIterations, int roomWidthMin, int roomLengthMin, int roomWidthMax, int roomLengthMax, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset, int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(levelWidth, levelLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin, roomWidthMax, roomLengthMax);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin, roomLengthMax, roomWidthMax);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerModifier, roomOffset);
        
        CorridorGenerator corridorGenerator = new CorridorGenerator();
        var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

       return new List<Node> (roomList);
       //return new List<Node> (roomList).Concat(corridorList).ToList();
    }
}