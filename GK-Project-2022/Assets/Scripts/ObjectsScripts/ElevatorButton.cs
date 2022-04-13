using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Material RedOn;
    public Material RedOff;

    public void TogglePower(bool statusOn)
    {
        if (statusOn)
        {
            transform.GetComponent<MeshRenderer>().material = RedOn;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material = RedOff;
        }
    }
}
