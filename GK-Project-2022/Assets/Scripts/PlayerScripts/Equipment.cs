using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    /*[HideInInspector]*/ 
    public List<Item> ListOfItems = new List<Item>();

    public GameObject Inventory;

    private void Start()
    {
        DrawInventory();
    }

    public void AddToEq(Item item)
    {
        if(item is Coin)
        {
            foreach(var i in ListOfItems)
            {
                if(i is Coin)
                {
                    i.Stack++;
                    return;
                }
            }
        }
        ListOfItems.Add(item);
    }

    public void DrawInventory()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i < ListOfItems.Count)
            {
                Inventory.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = ListOfItems[i].image;
                Inventory.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ListOfItems[i].Stack.ToString();
            }
        }
    }

}
