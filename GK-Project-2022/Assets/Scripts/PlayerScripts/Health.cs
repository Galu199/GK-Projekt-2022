using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public delegate void NoHealthPoints();
    public static event NoHealthPoints Death;

    public Image healthbar;

    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public void UpdateHealthBar(int damage)
    {
        CurrentHealth -= damage;
        healthbar.fillAmount = (float)CurrentHealth / MaxHealth;
        if (CurrentHealth <= 0)
        {
            if (Death != null)
                Death();
        }
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        healthbar.fillAmount = (float)CurrentHealth / MaxHealth;
    }

}
