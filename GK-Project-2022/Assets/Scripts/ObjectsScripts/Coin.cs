using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private void Start()
    {
        Stack = 1;
    }

    public override void Use()
    {
        Debug.Log("Coin Used");
    }

    public override Item Clone()
    {
        var item = new Coin();
        item.image = image;
        item.Stack = Stack;
        return item;
    }
}
