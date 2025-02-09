//code provided by sunney valley studio on youtube

using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    private List<Node> childrenNodeList;

    public List<Node> ChildrenNodeList {  get => childrenNodeList;}

    public bool Visited { get; set; }
    public Vector2Int BottomLeftAreaCorner { get; set; }
}