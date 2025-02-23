using System;
using System.Collections.Generic;
using UnityEngine;

//created by Kat Wayman
//code provided by sunney valley studio on youtube
//i pretty faithfully followed this youtube tutorial compared to other tutorials used to teach myself unity because procedural generation is
//hard TT
public class LevelGenerator
{
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int levelWidth;
    private int levelLength;
    public LevelGenerator(int levelWidth, int levelLength)
    {
        this.levelWidth = levelWidth;
        this.levelLength = levelLength;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitoner(levelWidth, levelLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        return new List<Node> (allSpaceNodes);
    }
}