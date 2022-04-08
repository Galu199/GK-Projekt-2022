using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void Teleport(Vector3 position)
    {
        GetComponent<Transform>().position = position;
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
