using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class MovementGrid : MonoBehaviour
{
    
    
    [SerializeField] LevelController levelController;
    Transform startPosition;

    [Tooltip("Mask for unwalkable objects.")]
    [SerializeField] LayerMask isWall;

    [Tooltip("Size of grid.")]
    [SerializeField] Vector2 gridWorldSize; // size of grid

    [Tooltip("Size of single node.")]
    [SerializeField] float nodeRadius; // radius of node in grid
    [SerializeField] float nodeSpawnDistance; // distance between nodes of grid

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int sizeX;
    int sizeY;

    public delegate void gridIsBuilt(Vector3 from, Vector3 to);
    public event gridIsBuilt gridBuildSignal;

    private void Awake()
    {
        levelController = GetComponent<LevelController>();
        //startPosition.position.x = levelController.mapX;

        startPosition = GetComponent<Transform>();
        startPosition.Translate(new Vector3(0, 0, 0));
        nodeDiameter = nodeRadius * 2;
        sizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        sizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        levelController.onMapConstructed.AddListener(StartBuildingGrid);
    }

    void StartBuildingGrid()
    {
        StartCoroutine(BuildGrid());
    }

    IEnumerator BuildGrid()
    {
        yield return new WaitForFixedUpdate();

        grid = new Node[sizeX, sizeY];
        Vector3 bottomLeft = startPosition.position;// transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                    + Vector3.forward * (y * nodeDiameter + nodeRadius);

                bool wall = false;
                /*Physics.CheckBox(worldPoint, new Vector3(nodeRadius/2, nodeRadius / 2, nodeRadius / 2),
                    Quaternion.identity, isWall);*/
                if(Physics.CheckSphere(worldPoint, nodeRadius, isWall))
                {
                    wall = true;
                }

                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
        Vector3 placeholder = new Vector3(0, 0, 0);
        gridBuildSignal(placeholder, placeholder);
    }

    public Node NodeFromWorldPosition(Vector3 _worldPos)
    {
        float xpoint = (_worldPos.x - startPosition.position.x) / gridWorldSize.x;
        float ypoint = (_worldPos.z - startPosition.position.z) / gridWorldSize.y;

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((sizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((sizeY - 1) * ypoint);

        return grid[x, y];
    }

    public List<Node> GetNeighboringNodes(Node _node)
    {
        List<Node> neighboringNodes = new List<Node>();

        int xCheck = _node.gridX; ;
        int yCheck = _node.gridY;

        if(CheckNode(xCheck + 1, yCheck))
        {
                neighboringNodes.Add(grid[xCheck + 1, yCheck]);

        }
        if (CheckNode(xCheck - 1, yCheck))
        {
            neighboringNodes.Add(grid[xCheck - 1, yCheck]);

        }
        if (CheckNode(xCheck, yCheck + 1))
        {
            neighboringNodes.Add(grid[xCheck, yCheck + 1]);

        }
        if (CheckNode(xCheck, yCheck - 1))
        {
            neighboringNodes.Add(grid[xCheck, yCheck - 1]);
        }

        return neighboringNodes;
    }

    bool CheckNode(int xCheck, int yCheck)
    {
        if (xCheck >= 0 && xCheck < sizeX)
        {
            if (yCheck >= 0 && yCheck < sizeY)
            {
                return true;
            }
        }

        return false;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node node in grid)
            {
                if (node.isObstructed == false)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Handles.Label(node.position, $"{node.gridX}, {node.gridY}");
                }
                
                if(finalPath != null)
                {
                    if (finalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - nodeSpawnDistance));
            }
        }
    }*/
    
}
