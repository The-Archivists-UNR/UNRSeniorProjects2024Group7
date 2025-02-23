//created by Kat Wayman
//code provided by sunney valley studio on youtube
using UnityEngine;
public class RoomNode : Node
{
    public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, Node parentNode, int index) : base(parentNode)
    {
        this.BottomLeftAreaCorner = bottomLeftAreaCorner;
        this.TopRightAreaCorner = topRightAreaCorner;
        this.BottomRightAreaCorner = new Vector2Int(topRightAreaCorner.x, bottomLeftAreaCorner.y);
        this.TopLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, topRightAreaCorner.y);  
        this.TreeLayerIndex = index;
    }

    public int Width { get => (int)(TopLeftAreaCorner.x-BottomLeftAreaCorner.x); }
    public int Length { get => (int)(TopRightAreaCorner.y-BottomLeftAreaCorner.y); }
}