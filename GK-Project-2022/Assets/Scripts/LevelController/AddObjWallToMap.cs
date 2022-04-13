using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjWallToMap
{
    public static List<List<int>> Generate(List<List<int>> map, int objectId)
    {
        //VARIABLE
        var avilible = new List<Tuple<int, int>>();
        var maxIndexX = map.Count - 1;
        var maxIndexY = map[0].Count - 1;
        //Look for free spaces to put object
        for (int y = 0; y < map.Count; y++)     if (map[y][0] == 1 && map[y][0 + 1] == 0) avilible.Add(new Tuple<int, int>(0, y));
        for (int y = 0; y < map.Count; y++)     if (map[y][maxIndexY] == 1 && map[y][maxIndexY - 1] == 0) avilible.Add(new Tuple<int, int>(maxIndexY, y));
        for (int x = 0; x < map[0].Count; x++)  if (map[0][x] == 1 && map[0 + 1][x] == 0) avilible.Add(new Tuple<int, int>(x, 0));
        for (int x = 0; x < map[0].Count; x++)  if (map[maxIndexX][x] == 1 && map[maxIndexX - 1][x] == 0) avilible.Add(new Tuple<int, int>(x, maxIndexX));
        for (int y = 1; y < map.Count - 1; y++)
        {
            for (int x = 1; x < map[y].Count - 1; x++)
            {
                if (map[y][x] == 0) continue;
                if (map[y][x + 1] == 0 || map[y][x - 1] == 0 || map[y + 1][x] == 0 || map[y - 1][x] == 0)
                    avilible.Add(new Tuple<int, int>(x, y));
            }
        }
        //Choose random avilible cord
        var choosen = avilible[UnityEngine.Random.Range(0, avilible.Count)];
        map[choosen.Item2][choosen.Item1] = objectId;
        //RETURN
        return map;
    }
}
