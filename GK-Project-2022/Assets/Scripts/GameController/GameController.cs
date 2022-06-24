using UnityEngine;
using UnityEngine.SceneManagement;
using Main.Assets.Scripts;
using System;
using System.Collections.Generic;

[Serializable]
public struct AucioClipItem
{
    public string name;
    public AudioClip clip;
}

public class GameController : MonoBehaviour
{
    public LevelController levelController;
    public AIController aiController;
    public SelectionManager selectionManager;
    public GameObject player;
    public AudioSource audioSourceForItems;
    public List<AucioClipItem> clips;
    public GameObject UI;
    public int level = 0;
    public int ECTS_cost = 75;
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
        if (player != null) player.GetComponent<Equipment>().ItemUsed -= Inventory_ItemUsed;
    }


    private void GenerateLevel(int level)
    {
        audioSourceForItems.clip = null;
        aiController.numberOfenemies = 1;
        levelController.seed = (int)(level * Time.deltaTime * UnityEngine.Random.Range(10000000, 90000000));
        switch (level % 5)
        {
            case 0:
                levelController.MapGenNumber = 2;
                levelController.mapX = UnityEngine.Random.Range(7, 15);
                levelController.mapY = UnityEngine.Random.Range(7, 15);
                break;
            default:
                levelController.MapGenNumber = 1;
                levelController.mapX = UnityEngine.Random.Range(10, 21);
                levelController.mapY = UnityEngine.Random.Range(10, 21);
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
            PlayThis("button");
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
            PlayThis("lever");
            selectionManager.elevatorPowerPressed = false;
            PowerOn = true;
            foreach (var item in FindObjectsOfType<ElevatorButton>()) item.TogglePower(PowerOn);
        }
    }

    private void IfItemIsClickedPutItInInventory()
    {
        if (selectionManager.clickedItem != null)
        {
            PlayThis("pickup");
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
            //Debug.Log("Coin used");
            PlayThis("coin");
        }
        else
        if (item.GetType() == typeof(Piwo))
        {
            //Debug.Log("Beer used");
            if (audioSourceForItems.isPlaying) audioSourceForItems.Stop();
            PlayThis("beer");
            player.GetComponent<Equipment>().RemoveItem(item);
            player.GetComponent<Equipment>().DrawInventory();
            player.GetComponent<Health>().Reset();
        }
        else
        if (item.GetType() == typeof(Solder))
        {
            //Debug.Log("Solder used");
            //TO DO
            // if 75z³ koniec gry win else dialog nie masz hasju na ects-a
            foreach (var it in player.GetComponent<Equipment>().ListOfItems)
            {
                if (it.GetType() != typeof(Coin)) continue;
                if (it.Stack < ECTS_cost) continue;
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadSceneAsync(2);
                return;
            }
            var messageBox2 = Helpers.BringMessageBox(UI);
            messageBox2.SetMessage($"Potrzebujesz {ECTS_cost}z³ na ECTS-a!");
            messageBox2.Dissapear();
        }
    }

    private void PlayThis(string clipName)
    {
        if (audioSourceForItems.clip == null || clips.Find(x => x.clip.name == audioSourceForItems.clip.name).name != clipName || !audioSourceForItems.isPlaying)
        {
            audioSourceForItems.clip = clips.Find(x => x.name == clipName).clip;
            audioSourceForItems.Play();
        }
    }

}
