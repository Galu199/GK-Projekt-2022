using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum objectId
{
    Air,
    ItemCoin,
    ItemBeer,
    Wall,
    WallElevator,
    WallPowerSwitch
}

public class LevelController : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public AIController aiController;
    public GameObject player;

    public GameObject prefabWall;
    public GameObject prefabWallElevator;
    public GameObject prefabWallPowerSwitch;
    public GameObject prefabCoin;
    public GameObject prefabBeer;

    public GameObject WallsContainer;
    public GameObject ItemContainer;

    public int mapY = 10;
    public int mapX = 10;
    public int spawnX = 1;
    public int spawnY = 1;
    public int seed = 0;
    [Range(0, 2)] public int MapGenNumber = 0;

    private List<List<int>> map = new List<List<int>>();//2d map container
    private List<List<int>> mapOptimized = null;//2d map container ready to render
    private List<Wall> walls = new List<Wall>();//List of Walls
    private List<Item> items = new List<Item>();//List of Walls

    public void GenerateMap()
    {
        switch (MapGenNumber)
        {
            default:
            case 0:
                map = MapRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
            case 1:
                map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
            case 2:
                if ((map = MapMazeRoom.Generate(mapX, mapY, spawnX, spawnY, seed)) == null)
                    map = MapTunnelingRoom.Generate(mapX, mapY, spawnX, spawnY, seed);
                break;
        }
        MovePlayerToSpawn();
        MoveEnemyToRandomSpawn();
        AddObjWallToMap.Generate2(ref map, (int)objectId.WallElevator);
        AddObjWallToMap.Generate2(ref map, (int)objectId.WallPowerSwitch);
        for (int i = 0; i < 5; i++) AddItemToMap.Generate2(ref map, (int)objectId.ItemCoin);
        if (Random.Range(0, 2) % 2 == 0) AddItemToMap.Generate2(ref map, (int)objectId.ItemBeer);
        mapOptimized = optimizeMapWalls.Generate3(map);
        SpawnMap(mapOptimized);
        //navMeshSurface.BuildNavMesh();
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
            enemy.transform.position = (new Vector3(freefield.Item1 * prefabWall.transform.localScale.x, 1.5f, freefield.Item2 * prefabWall.transform.localScale.z));
        }
    }

    private void SpawnMap(List<List<int>> _map)
    {
        foreach (var wall in walls) wall.Delete();
        walls.Clear();
        foreach (var item in items)
        {
            if (item != null)
                item.Delete();
        }
        items.Clear();
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                switch (_map[y][x])
                {
                    default:
                    case (int)objectId.Air:
                        break;
                    case (int)objectId.Wall:
                        GenerateWall(prefabWall, x, y);
                        break;
                    case (int)objectId.WallElevator:
                        GenerateWall(prefabWallElevator, x, y);
                        RotateObject(walls[walls.Count - 1], x, y);
                        break;
                    case (int)objectId.WallPowerSwitch:
                        GenerateWall(prefabWallPowerSwitch, x, y);
                        RotateObject(walls[walls.Count - 1], x, y);
                        break;
                    case (int)objectId.ItemCoin:
                        GenerateItem(prefabCoin, x, y);
                        break;
                    case (int)objectId.ItemBeer:
                        GenerateItem(prefabBeer, x, y);
                        break;
                }
            }
        }
    }

    private void GenerateWall(GameObject prefab, int x, int z)
    {
        GameObject Obj = Instantiate(prefab, WallsContainer.transform);
        var item = Obj.GetComponent<Wall>();
        walls.Add(item);
        item.Teleport(new Vector3(x * prefabWall.transform.localScale.x, prefabWall.transform.position.y, z * prefabWall.transform.localScale.z));
    }

    private void GenerateItem(GameObject prefab, int x, int z)
    {
        GameObject Obj = Instantiate(prefab, ItemContainer.transform);
        var item = Obj.GetComponent<Item>();
        items.Add(item);
        item.Teleport(new Vector3(x * prefabWall.transform.localScale.x, Obj.transform.position.y, z * prefabWall.transform.localScale.z));
    }

    private void RotateObject(Wall obj, int x, int y)
    {
        if (y < mapY - 1 && map[y + 1][x] < (int)objectId.Wall)
        {
            //do nothing
        }
        else
        if (x < mapX - 1 && map[y][x + 1] < (int)objectId.Wall)
        {
            obj.Rotate(new Vector3(0, 90.0f, 0));
        }
        else
        if (y > 0 && map[y - 1][x] < (int)objectId.Wall)
        {
            obj.Rotate(new Vector3(0, 180.0f, 0));
        }
        else
        if (x > 0 && map[y][x - 1] < (int)objectId.Wall)
        {
            obj.Rotate(new Vector3(0, -90.0f, 0));
        }
    }

}
