using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isObstructed;
    public int gridX;
    public int gridY;
    public Vector3 position;
    public Node parent;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node(bool _isObstructed, Vector3 a_pos, int a_gridX, int a_gridY)
    {
        isObstructed = _isObstructed;
        gridX = a_gridX;
        gridY = a_gridY;
        position = a_pos;
    }
}
