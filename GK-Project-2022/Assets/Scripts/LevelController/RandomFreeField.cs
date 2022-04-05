using System;
using System.Collections.Generic;

public class RandomFreeField
{
    public static Tuple<int, int> Generate(int sizeX, int sizeY, List<List<int>> map)
    {
        //VARIABLES
        var result = new Tuple<int, int>(0, 0);
        var freeFields = new List<Tuple<int, int>>();
        //CHECK FREE FIELDS
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
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
