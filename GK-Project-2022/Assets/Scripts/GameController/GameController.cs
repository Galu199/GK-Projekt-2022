using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelController levelController;

    void Start()
    {
        
    }

    void Update()
    {
        if (levelController.player.GetComponent<Health>().CurrentHealth <= 0)
        {
            levelController.MovePlayerToSpawn();
            //levelController.MoveEnemyToRandomSpawn();//TO DO
            levelController.player.GetComponent<Health>().Reset();
        }
    }
}
