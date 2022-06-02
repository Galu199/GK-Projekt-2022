using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Stack;
    public Sprite image;

    private void Start()
    {
        Stack = 1;
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    public void Rotate(Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetActivity(bool active)
    {
        gameObject.SetActive(active);
    }
}
