using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthbar;

    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public void UpdateHealthBar(int damage)
    {
        CurrentHealth -= damage;
        healthbar.fillAmount = (float)CurrentHealth / MaxHealth;
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        healthbar.fillAmount = (float)CurrentHealth / MaxHealth;
    }

}
