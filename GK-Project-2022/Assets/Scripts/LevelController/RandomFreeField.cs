using System;
using System.Collections.Generic;

public class RandomFreeField
{
    public static Tuple<int, int> Generate(List<List<int>> map)
    {
        //VARIABLES
        var result = new Tuple<int, int>(0, 0);
        var freeFields = new List<Tuple<int, int>>();
        freeFields.Add(result);
        //CHECK FREE FIELDS
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == 0)
                    freeFields.Add(new Tuple<int, int>(x, y));
            }
        }
        //CHOOSE RANDOM ONE
        result = freeFields[UnityEngine.Random.Range(0, freeFields.Count)];
        //RETURN
        return result;
    }
}
