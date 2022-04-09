using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelController : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public AIController aiController;
    public GameObject player;
    public GameObject prefabWall;
    public GameObject WallsContainer;
    public int mapY = 10;
    public int mapX = 10;
    public int spawnX = 1;
    public int spawnY = 1;
    public int seed = 0;
    [Range(0, 2)] public int MapGenNumber = 0;
    private List<List<int>> map = new List<List<int>>();//2d map container
    private List<List<int>> mapOptimized = null;
    private List<Wall> walls = new List<Wall>();//List of Walls

    public void GenerateMap()
    {
        switch (MapGenNumber)
        {
            case 0:
            default:
                map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
            case 1:
                if ((map = MapMazeRoom.Generate(mapX, mapY, spawnX, spawnY, seed)) == null)
                    map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
            case 2:
                map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
        }
        MovePlayerToSpawn();
        MoveEnemyToRandomSpawn();
        //navMeshSurface.BuildNavMesh();
        mapOptimized = optimizeMapWalls.Generate3(map);
        SpawnMap(mapOptimized);
    }

    public void MovePlayerToSpawn()
    {
        player.SetActive(false);
        player.transform.position = (new Vector3(spawnX * prefabWall.transform.localScale.x, 1.5f, spawnY * prefabWall.transform.localScale.z));
        player.SetActive(true);
    }

    public void MoveEnemyToRandomSpawn()
    {
        if (map == null) return;
        foreach (var enemy in aiController.enemies)
        {
            var freefield = RandomFreeField.Generate(map);
            enemy.transform.position = (new Vector3(freefield.Item1* prefabWall.transform.localScale.x, 1.5f, freefield.Item2* prefabWall.transform.localScale.z));
        }
    }

    private void SpawnMap(List<List<int>> map)
    {
        foreach (var wall in walls) wall.Delete();
        walls.Clear();
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == 1) GenerateWall(x, y);
            }
        }
    }

    private void GenerateWall(int x, int z)
    {
        GameObject Obj = Instantiate(prefabWall, WallsContainer.transform);
        var wall = Obj.GetComponent<Wall>();
        walls.Add(wall);
        wall.Teleport(new Vector3(x * Obj.transform.localScale.x, 2, z * Obj.transform.localScale.z));
    }

}
