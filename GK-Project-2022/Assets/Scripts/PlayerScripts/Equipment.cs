using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public List<Item> ListOfItems = new List<Item>();
    public GameObject Inventory;

    public event EventHandler<EquipmentEventArgs> ItemUsed;

    private void Start()
    {
        DrawInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            UseItem(0);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            UseItem(1);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            UseItem(2);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            UseItem(3);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            UseItem(4);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            UseItem(5);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            UseItem(6);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            UseItem(7);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            UseItem(8);
        }
    }

    public void AddItem<T>(T item) where T : Item
    {
        //T item = (T)Convert.ChangeType(_item, typeof(T));
        //Debug.Log(typeof(T));
        //Debug.Log(item.GetType());
        //Debug.Log(item is Coin);
        //Debug.Log(item is Piwo);
        //Debug.Log(item as Coin);
        //Debug.Log(item as Piwo);
        foreach (var i in ListOfItems)
        {
            if (i.GetType() == item.GetType())
            {
                i.Stack++;
                return;
            }
        }
        ListOfItems.Add(item);
        return;
    }

    public void RemoveItem<T>(T item) where T : Item
    {
        foreach (var i in ListOfItems)
        {
            if (i.GetType() == item.GetType())
            {
                i.Stack--;
                if (i.Stack<=0)
                    ListOfItems.Remove(i);
                return;
            }
        }
        Debug.Log("No such item in this Equipment");
        return;
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
            else
            {
                Inventory.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                Inventory.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void UseItem(int index)
    {
        if (index < ListOfItems.Count && index >= 0)
        {
            if (ItemUsed != null)
            {
                ItemUsed(this, new EquipmentEventArgs(ListOfItems[index]));
            }
        }
    }

}

public class EquipmentEventArgs : EventArgs
{
    public Item item;
    public EquipmentEventArgs(Item item)
    {
        this.item = item;
    }
}
