using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject prefabEnemy;
    public GameObject EnemyContainer;
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    public int numberOfenemies = 1;

    public void SpawnEnemies()
    {
        foreach (var enemy in enemies) enemy.Delete();
        enemies.Clear();
        for (int i = 0; i < numberOfenemies; i++)
        {
            GameObject Obj = Instantiate(prefabEnemy, EnemyContainer.transform);
            var item = Obj.GetComponent<Enemy>();
            enemies.Add(item);
        }
    }
}
