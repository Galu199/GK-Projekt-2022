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
    public bool PowerOn = false;

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

        if (selectionManager.elevatorButtonPressed)
        {
            selectionManager.elevatorButtonPressed = false;
            if (PowerOn)
            {
                PowerOn = false;
                foreach (var item in FindObjectsOfType<ElevatorButton>()) item.TogglePower(PowerOn);
                GenerateLevel(level++);
            }
        }

        if (selectionManager.elevatorPowerPressed)
        {
            selectionManager.elevatorPowerPressed = false;
            PowerOn = true;
            foreach (var item in FindObjectsOfType<ElevatorButton>()) item.TogglePower(PowerOn);
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
