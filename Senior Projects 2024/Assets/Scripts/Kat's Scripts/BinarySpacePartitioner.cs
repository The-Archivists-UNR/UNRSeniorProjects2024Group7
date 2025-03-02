using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//created by Kat Wayman
//code provided by sunney valley studio on youtube

public class BinarySpacePartitioner
{
    RoomNode rootNode;
    public RoomNode RootNode { get => rootNode; }

    public BinarySpacePartitioner(int levelWidth, int levelLength)
    {
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(levelWidth, levelLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin, int roomWidthMax, int roomLengthMax)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);
        int iterations = 0;
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if ((currentNode.Width > roomWidthMax || currentNode.Length > roomLengthMax)||(currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)) //incorporate roomwidthmax and lengthmax here
            {
                SplitTheSpace(currentNode, listToReturn, roomWidthMin, roomLengthMin, roomWidthMax, roomLengthMax, graph);
            }
        }
        return listToReturn;
    }

    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomWidthMin, int roomLengthMin, int roomWidthMax, int roomLengthMax, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner, roomWidthMin, roomLengthMin, roomWidthMax, roomLengthMax);
        RoomNode node1, node2;
        if (line.Orientation == Orientation.Horizontal)
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner, 
                new Vector2Int(currentNode.TopRightAreaCorner.x, line.Coordinates.y),
                currentNode, currentNode.TreeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, line.Coordinates.y), currentNode.TopRightAreaCorner, currentNode, currentNode.TreeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner, new Vector2Int(line.Coordinates.x, currentNode.TopRightAreaCorner.y), currentNode, currentNode.TreeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y), currentNode.TopRightAreaCorner, currentNode, currentNode.TreeLayerIndex + 1);
        }
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    private void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin, int roomWidthMax, int roomLengthMax) //incorporate maximums here
    {
        Orientation orientation;
        int currentWidth = topRightAreaCorner.x - bottomLeftAreaCorner.x;
        int currentLength = topRightAreaCorner.y - bottomLeftAreaCorner.y;

        bool forceSplitWidth = currentWidth > roomWidthMax;
        bool forceSplitLength = currentLength > roomLengthMax;

        //bool lengthStatus = (currentLength >= 2 * roomLengthMin && currentLength > roomLengthMax);
        //bool widthStatus = (currentWidth >=2 * roomWidthMin && currentWidth > roomWidthMax);
        if (forceSplitWidth && forceSplitLength)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
        else if (forceSplitWidth)
        {
            orientation = Orientation.Vertical;
        }
        else if (forceSplitLength)
        {
            orientation = Orientation.Horizontal;
        }
        else
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
            return new Line(orientation, GetCoordinatesFororientation(
                orientation,
                bottomLeftAreaCorner,
                topRightAreaCorner,
                roomWidthMin,
                roomLengthMin, roomWidthMax, roomLengthMax));
       
    }


    private Vector2Int GetCoordinatesFororientation(Orientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin, int roomWidthMax, int roomLengthMax)
    {
        Vector2Int coordinates = Vector2Int.zero;
        int currentWidth = topRightAreaCorner.x - bottomLeftAreaCorner.x;
        int currentLength = topRightAreaCorner.y - bottomLeftAreaCorner.y;
        if (orientation == Orientation.Horizontal)
        {
            int minimumSplit = bottomLeftAreaCorner.y + roomLengthMin;
            int maximumSplit = topRightAreaCorner.y - roomLengthMin;
            int idealSplit = Mathf.Clamp(Random.Range(minimumSplit, maximumSplit), bottomLeftAreaCorner.y + roomLengthMin,topRightAreaCorner.y- roomLengthMin);
            if (idealSplit - bottomLeftAreaCorner.y > roomLengthMax)
            {
                idealSplit = bottomLeftAreaCorner.y + roomLengthMax;
            }
            if (topRightAreaCorner.y - idealSplit > roomLengthMax)
            {
                idealSplit = topRightAreaCorner.y - roomLengthMax;
            }
            coordinates = new Vector2Int(0, idealSplit);
        }
        else
        {
            int minimumSplit = bottomLeftAreaCorner.x + roomWidthMin;
            int maximumSplit = topRightAreaCorner.x - roomWidthMin;
            int idealSplit = Mathf.Clamp(Random.Range(minimumSplit, maximumSplit), bottomLeftAreaCorner.x + roomWidthMin, topRightAreaCorner.x - roomWidthMin);
            if (idealSplit - bottomLeftAreaCorner.x > roomWidthMax)
            {
                idealSplit = bottomLeftAreaCorner.x + roomWidthMax;
            }
            if (topRightAreaCorner.x - idealSplit > roomWidthMax)
            {
                idealSplit = topRightAreaCorner.x - roomWidthMax;
            }
            coordinates = new Vector2Int(idealSplit, 0);
        }
        return coordinates;
    }
}