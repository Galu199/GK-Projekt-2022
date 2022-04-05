using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optimizeMapWalls
{
    public static List<List<int>> Generate(int sizeX, int sizeY, List<List<int>> _map)
    {
        //VARIABLES
        var map = _map;
        var wallsToSave = new List<Tuple<int, int>>();
        var indexX = sizeX - 1;
        var indexY = sizeY - 1;
        //FIND WALLS
        for (int y = 0; y < indexY; y++)
        {
            if (map[y][0 + 1] == 0)
                wallsToSave.Add(new Tuple<int, int>(0, y));
        }
        for (int y = 0; y < indexY; y++)
        {
            if (map[y][indexX - 1] == 0)
                wallsToSave.Add(new Tuple<int, int>(indexX, y));
        }
        for (int x = 0; x < indexX; x++)
        {
            if (map[0 + 1][x] == 0)
                wallsToSave.Add(new Tuple<int, int>(x, 0));
        }
        for (int x = 0; x < indexX; x++)
        {
            if (map[indexY - 1][x] == 0)
                wallsToSave.Add(new Tuple<int, int>(x, indexY));
        }
        for (int y = 1; y < sizeY - 1; y++)
        {
            for (int x = 1; x < sizeX - 1; x++)
            {
                if (map[y + 1][x] == 0 || map[y - 1][x] == 0 || map[y][x + 1] == 0 || map[y][x - 1] == 0)
                    wallsToSave.Add(new Tuple<int, int>(x, y));
            }
        }
        //DELETE WALLS
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if (!wallsToSave.Contains(new Tuple<int, int>(x, y)))
                    map[y][x] = 0;
            }
        }
        //RETURN
        return map;
    }
}
