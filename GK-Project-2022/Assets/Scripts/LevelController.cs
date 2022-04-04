using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject player;
    public GameObject prefabWall;
    public GameObject WallsContainer;
    public int mapY = 10;
    public int mapX = 10;
    private int spawnX = 1;
    private int spawnY = 1;
    private List<List<int>> map = new List<List<int>>();//2d map container
    private List<Wall> walls = new List<Wall>();//List of Walls

    private void Start()
    {
        map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY);
        SpawnMap();
        MovePlayer();
    }

    public void SpawnMap()
    {
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                if (map[y][x] == 1) GenerateRock(x, y);
            }
        }
    }

    public void MovePlayer()
    {
        player.GetComponent<CharacterController>().Move(new Vector3(spawnX, 1.5f, spawnY));
    }

    private void GenerateRock(int x, int y)
    {
        GameObject Obj = Instantiate(prefabWall, WallsContainer.transform);
        var wall = Obj.GetComponent<Wall>();
        walls.Add(wall);
        wall.Teleport(new Vector3(x, 2, y));
    }

}
