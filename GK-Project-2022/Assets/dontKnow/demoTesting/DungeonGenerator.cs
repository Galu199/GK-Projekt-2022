using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited=false;
        public bool[] status = new bool[4];    
    }

    List<Cell> board;
    public Vector2 size;
    public int startPos = 0;
    public GameObject[] rooms;
    public Vector2 offset;
    public GameObject player;

    private void Awake()
    {
        MazeGenerator();
    }

    private void Start()
    {
        player.transform.SetPositionAndRotation(new Vector3(0,0,0),new Quaternion(0,0,0,0));
    }
    void GenerateDungeon()
    {
        
        for(int i=0; i<size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    int l = Random.Range(0, rooms.Length);
                    
                    var newRoom = Instantiate(rooms[l],
                                            new Vector3(i * offset.x, 0, -j * offset.y),
                                            Quaternion.identity, transform)
                                            .GetComponent<RoomBehaviour>();
                    newRoom.name += " " + i + "-" + j;
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                }
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        for(int i=0; i<size.x; i++)
        {
            for(int j=0; j<size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;
        while (k < 1000)
        {
            k++;
            board[currentCell].visited = true;
            if (currentCell == board.Count - 1)
                break;

            //check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);
            if (neighbors.Count == 0)
            {
                if(path.Count==0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                   
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if(newCell>currentCell)
                {
                    //down or right
                    if(newCell-1==currentCell)
                    {
                        //right
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;

                    }
                    else
                    {
                        //down
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }

                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;

                    }
                    else
                    {
                        
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    // Update is called once per frame
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        //check up
        if(cell-size.x>=0&&!board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right
        if ((cell+1) % size.x !=0 && !board[Mathf.FloorToInt(cell +1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left
        if (cell%size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbors;
    }
}
