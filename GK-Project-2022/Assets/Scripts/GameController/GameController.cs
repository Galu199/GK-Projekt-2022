using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelController levelController;
    public AIController aiController;
    public SelectionManager selectionManager;
    public int level = 0;
    public bool GenerateEnemies = true;
    public bool GenerateMap = true;

    private void Start()
    {

    }

    private void Update()
    {
        if (levelController.player.GetComponent<Health>().CurrentHealth <= 0)
        {
            levelController.MovePlayerToSpawn();
            levelController.MoveEnemyToRandomSpawn();
            levelController.player.GetComponent<Health>().Reset();
        }

        if(selectionManager.elevatorButtonPressed && selectionManager.elevatorPowerPressed)
        {
            GenerateLevel(level++);
            selectionManager.elevatorButtonPressed = false;
            selectionManager.elevatorPowerPressed = false;
        }
        else
        {
            selectionManager.elevatorButtonPressed = false;
        }
    }

    private void GenerateLevel(int level)
    {
        aiController.numberOfenemies = 1;
        aiController.SpawnEnemies();
        levelController.seed = level;
        levelController.GenerateMap();
    }

}
