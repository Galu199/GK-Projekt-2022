using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToMap : MonoBehaviour
{
    public static List<List<int>> Generate(List<List<int>> map, int objectId)
    {
        //VARIABLE
        var freeSpace = new List<Tuple<int, int>>();
        //Finding free space for item to spawn
        for (int y = 1; y < map.Count - 1; y++)
        {
            for (int x = 1; x < map[y].Count - 1; x++)
            {
                if (map[y][x]==0)
                {
                    freeSpace.Add(new Tuple<int, int>(x, y));
                }
            }
        }
        //Adding One item
        var choosen = freeSpace[UnityEngine.Random.Range(0, freeSpace.Count)];
        map[choosen.Item2][choosen.Item1] = objectId;
        //RETUNR
        return map;
    }

    public static void Generate2(ref List<List<int>> map, int objectId)
    {
        //VARIABLE
        var freeSpace = new List<Tuple<int, int>>();
        //Finding free space for item to spawn
        for (int y = 1; y < map.Count - 1; y++)
        {
            for (int x = 1; x < map[y].Count - 1; x++)
            {
                if (map[y][x] == 0)
                {
                    freeSpace.Add(new Tuple<int, int>(x, y));
                }
            }
        }
        //Adding One item
        var choosen = freeSpace[UnityEngine.Random.Range(0, freeSpace.Count)];
        map[choosen.Item2][choosen.Item1] = objectId;
    }
}
