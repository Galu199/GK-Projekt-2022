using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoom 
{
    static public List<List<int>> Generate(int sizeX = 4, int sizeY = 4, int spawnX = 1, int spawnY = 1, int seed = 0)
    {
        //SEED
        UnityEngine.Random.InitState(seed);
        //VARIABLES
        var map = new List<List<int>>();
        //INITIALIZE
        for (int y = 0; y < sizeY; y++)
        {
            map.Add(new List<int>());
            for (int x = 0; x < sizeX; x++)
            {
                if (x == 0 || y == 0 || y == sizeY - 1 || x == sizeX - 1)
                    map[y].Add((int)objectId.Wall);
                else
                    map[y].Add((int)objectId.Air);
            }
        }
        //RETURN
        return map;
    }
}
