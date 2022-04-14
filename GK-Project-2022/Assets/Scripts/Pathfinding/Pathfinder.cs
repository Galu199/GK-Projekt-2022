using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    PathManager pathManager;
    MovementGrid grid;

    private void Awake()
    {
        pathManager = GetComponent<PathManager>();
        grid = GetComponent<MovementGrid>();
        grid.gridBuildSignal += FindPathForOne;
    }

    public void FindPathForOne(Vector3 from, Vector3 to)
    {
        StartCoroutine(FindPath(from, to));
    }

    IEnumerator FindPath(Vector3 from, Vector3 to)
    {
        Node startNode = grid.NodeFromWorldPosition(from);
        Node targetNode = grid.NodeFromWorldPosition(to);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        Vector3[] waypoints = new Vector3[0];
        bool succesful = false;

        if (/*!startNode.isObstructed && */!targetNode.isObstructed)
        {
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost <= currentNode.fCost || openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    //grid.finalPath = GetFinalPath(startNode, targetNode);
                    succesful = true;
                    break;
                    //return;
                }

                foreach (Node neighborNode in grid.GetNeighboringNodes(currentNode))
                {
                    if (!neighborNode.isObstructed && !closedList.Contains(neighborNode))
                    {
                        int moveCost = currentNode.gCost + GetManhattanDistance(currentNode, neighborNode);

                        if (moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                        {
                            neighborNode.gCost = moveCost;
                            neighborNode.hCost = GetManhattanDistance(neighborNode, targetNode);
                            neighborNode.parent = currentNode;

                            if (!openList.Contains(neighborNode))
                            {
                                openList.Add(neighborNode);
                            }
                        }
                    }
                }
            }
            yield return null;
            if (succesful)
            {
                waypoints = GetFinalPath(startNode, targetNode);
            }
        }
        pathManager.FinishedProcessing(waypoints, succesful);
    }

    Vector3[] GetFinalPath(Node _startingNode, Node _endNode)
    {
        List<Node> finalPath = new List<Node>();
        //List<Vector3> finalPath2 = new List<Vector3>();
        Node currentNode = _endNode;

        while(currentNode != _startingNode)
        {
            finalPath.Add(currentNode);
            //finalPath2.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
        grid.finalPath = finalPath;
        Vector3[] waypoints = SimplifyPath(finalPath);
        //Vector3[] waypoints = finalPath2.ToArray();

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].position);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    int GetManhattanDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
