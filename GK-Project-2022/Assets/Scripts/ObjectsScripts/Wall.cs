using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
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
}
