using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelController levelController;
    public AIController aiController;
    public SelectionManager selectionManager;
    public GameObject player;
    public int level = 0;
    public bool GenerateEnemies = true;
    public bool GenerateMap = true;
    public bool PowerOn = false;

    private void Start()
    {
        GenerateLevel(level++);
    }

    private void OnEnable()
    {
        Health.Death += IfPalyerisDead;
        SelectionManager.OnElevatorClick += IfElevatorButtonIsPressed;
        SelectionManager.OnPowerClick += IfElevatorPowerSwitchIsPressed;
        SelectionManager.OnItemClick += IfItemIsClickedPutItInInventory;
        player.GetComponent<Equipment>().ItemUsed += Inventory_ItemUsed;
    }

    private void OnDisable()
    {
        Health.Death -= IfPalyerisDead;
        SelectionManager.OnElevatorClick -= IfElevatorButtonIsPressed;
        SelectionManager.OnPowerClick -= IfElevatorPowerSwitchIsPressed;
        SelectionManager.OnItemClick -= IfItemIsClickedPutItInInventory;
        if(player!=null) player.GetComponent<Equipment>().ItemUsed -= Inventory_ItemUsed;
    }

    private void Update()
    {

    }

    private void GenerateLevel(int level)
    {
        aiController.numberOfenemies = 1;
        levelController.seed = (int)(level*Time.deltaTime*Random.Range(10000000,90000000));
        switch (level % 5)
        {
            case 0:
                levelController.MapGenNumber = 2;
                levelController.mapX = Random.Range(7, 15);
                levelController.mapY = Random.Range(7, 15);
                break;
            default:
                levelController.MapGenNumber = 1;
                levelController.mapX = Random.Range(10, 21);
                levelController.mapY = Random.Range(10, 21);
                break;
        }
        levelController.GenerateMap();
    }
    
    private void IfPalyerisDead()
    {
        if (levelController.player.GetComponent<Health>().CurrentHealth <= 0)
        {
            levelController.MovePlayerToSpawn();
            levelController.MoveEnemyToRandomSpawn();
            levelController.player.GetComponent<Health>().Reset();
        }
    }

    private void IfElevatorButtonIsPressed()
    {
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
    }

    private void IfElevatorPowerSwitchIsPressed()
    {
        if (selectionManager.elevatorPowerPressed)
        {
            selectionManager.elevatorPowerPressed = false;
            PowerOn = true;
            foreach (var item in FindObjectsOfType<ElevatorButton>()) item.TogglePower(PowerOn);
        }
    }

    private void IfItemIsClickedPutItInInventory()
    {
        if (selectionManager.clickedItem != null)
        {
            player.GetComponent<Equipment>().AddItem(selectionManager.clickedItem);
            player.GetComponent<Equipment>().DrawInventory();
            //selectionManager.clickedItem.SetActivity(false);
            selectionManager.clickedItem.Delete();
            selectionManager.clickedItem = null;
        }
    }

    private void Inventory_ItemUsed(object sender, EquipmentEventArgs e)
    {
        var item = e.item;
        if (item.GetType() == typeof(Coin))
        {
            Debug.Log("Coin used");
        }
        else
        if (item.GetType() == typeof(Piwo))
        {
            Debug.Log("Beer used");
            player.GetComponent<Equipment>().RemoveItem(item);
            player.GetComponent<Equipment>().DrawInventory();
            player.GetComponent<Health>().Reset();
        }
        else
        if (item.GetType() == typeof(Solder))
        {
            Debug.Log("Solder used");
            //TO DO
            // if 75z³ koniec gry win else dialog nie masz hasju na ects-a
        }
    }
}
