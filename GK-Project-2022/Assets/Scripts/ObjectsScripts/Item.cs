using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int Stack = 0;
    public Sprite image;

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

    public abstract Item Clone();
    public abstract void Use();
}
