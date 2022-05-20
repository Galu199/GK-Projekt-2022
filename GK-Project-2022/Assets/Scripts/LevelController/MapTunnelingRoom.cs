using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTunnelingRoom
{
    static public List<List<int>> Generate(int sizeX = 4, int sizeY = 4, int spawnX = 1, int spawnY = 1, int seed = 0)
    {
        //SEED
        UnityEngine.Random.InitState(seed);
        //VARIABLES
        var map = new List<List<int>>();
        var X = spawnX;
        var Y = spawnY;
        var maxFreeSpace = Mathf.Floor(sizeX * sizeY * 0.9f);
        //INITIALIZE
        for (int y = 0; y < sizeY; y++)
        {
            map.Add(new List<int>());
            for (int x = 0; x < sizeX; x++)
            {
                map[y].Add((int)objectId.Wall);
            }
        }
        //TUNNELING
        while (maxFreeSpace > 0)
        {
            map[Y][X] = (int)objectId.Air;
            maxFreeSpace -= 1;
            var kierunek = Random.Range(1, 4 + 1);//range is max exlusive
            switch (kierunek)
            {
                case 1:
                    if (Y > 1)
                    {
                        Y--;
                    }
                    break;
                case 2:
                    if (Y < sizeY - 2)
                    {
                        Y++;
                    }
                    break;
                case 3:
                    if (X > 1)
                    {
                        X--;
                    }
                    break;
                case 4:
                    if (X < sizeX - 2)
                    {
                        X++;
                    }
                    break;
            }
        }
        //RETURN
        return map;
    }
}
