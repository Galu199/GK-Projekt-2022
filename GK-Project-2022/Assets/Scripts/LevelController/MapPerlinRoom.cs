using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPerlinRoom : MonoBehaviour
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
                map[y].Add(1);
            }
        }
        //RETURN
        return map;
    }
}
