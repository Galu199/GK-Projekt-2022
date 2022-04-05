using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapMazeRoom
{
    static public List<List<int>> Generate(int sizeX = 3, int sizeY = 3, int spawnX = 1, int spawnY = 1)
    {
        //IMPORTANT RULES
        if (sizeX < 3 || sizeY < 3) return null;
        if (spawnX < 1 || spawnY < 1) return null;
        //CHECK IF SPAWN CORRECT BEFORE SPAWN
        if (spawnX % 2 == 0) spawnX -= 1;
        if (spawnY % 2 == 0) spawnY -= 1;
        //VARIABLES
        var map = new List<List<int>>();
        var path = new List<Tuple<int, int>>();
        var here = new Tuple<int, int>(spawnX, spawnY);
        var step = 0;
        var n = false;
        var e = false;
        var s = false;
        var w = false;
        var finish = false;
        //INITIALIZE
        for (int y = 0; y < sizeY; y++)
        {
            map.Add(new List<int>());
            for (int x = 0; x < sizeX; x++)
            {
                map[y].Add(1);
            }
        }
        //CHECK IF SIZE CORRECT AFTER INITIALIZE
        if (sizeX % 2 == 0) sizeX -= 1;
        if (sizeY % 2 == 0) sizeY -= 1;
        //Look for a free spot on parity block
        void look()
        {
            n = false;
            e = false;
            s = false;
            w = false;
            if (here.Item2 + 2 < sizeY)
            {
                if (map[here.Item2 + 2][here.Item1] == 1)
                {
                    s = true;
                }
            }
            if (here.Item2 - 2 > 0)
            {
                if (map[here.Item2 - 2][here.Item1] == 1)
                {
                    n = true;
                }
            }
            if (here.Item1 + 2 < sizeX)
            {
                if (map[here.Item2][here.Item1 + 2] == 1)
                {
                    e = true;
                }
            }
            if (here.Item1 - 2 > 0)
            {
                if (map[here.Item2][here.Item1 - 2] == 1)
                {
                    w = true;
                }
            }
        }
        //Check if all the parity spots are taken
        void check()
        {
            List<int> test = new List<int>();
            for (int y = 1; y < sizeY; y += 2)
            {
                for (int x = 1; x < sizeX; x += 2)
                {
                    test.Add(map[y][x]);
                }
            }
            List<int> unique = test.Distinct().ToList();
            if (unique.Count == 1)
            {
                finish = true;
            }
        }
        //Moving the pointer to draw a maze
        void move()
        {
            if (n == false && s == false && e == false && w == false)
            {
                step += 1;
                here = (path[path.Count - step]);
            }
            else
            {
                var currentCord = new Tuple<int, int>(here.Item1, here.Item2);
                path.Add(currentCord);
                step = 0;
                bool go = false;
                while (go == false)
                {
                    var direction = UnityEngine.Random.Range(1, 4 + 1);
                    if (direction == 1 && s == true)
                    {
                        here = new Tuple<int, int>(here.Item1, here.Item2 + 2);
                        go = true;
                    }
                    if (direction == 2 && n == true)
                    {
                        here = new Tuple<int, int>(here.Item1, here.Item2 - 2);
                        go = true;
                    }
                    if (direction == 3 && e == true)
                    {
                        here = new Tuple<int, int>(here.Item1 + 2, here.Item2);
                        go = true;
                    }
                    if (direction == 4 && w == true)
                    {
                        here = new Tuple<int, int>(here.Item1 - 2, here.Item2);
                        go = true;
                    }
                }
                map[here.Item2][here.Item1] = 0;
                int srodeky = (path[path.Count - step - 1].Item2 + here.Item2) / 2;
                int srodekx = (path[path.Count - step - 1].Item1 + here.Item1) / 2;
                map[srodeky][srodekx] = 0;
            }
        }
        //LOOP of actions
        while (finish == false)
        {
            look();
            move();
            check();
        }
        //EXTARS

        //RETURN
        return map;
    }
}
