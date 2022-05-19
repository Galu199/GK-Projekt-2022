using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0-front; 1-back; 2-right, 3-left
    public GameObject[] doors;
    

    public void UpdateRoom(bool[] status)
    {
        for (int i=0; i<4; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
