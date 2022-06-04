using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarBehaviour : MonoBehaviour
{

    public Image healthBar;
    public Image staminaBar;
    public Player player;
    float maxValue=5;
    float height = 40;

   
    private void Update()
    {

        healthBar.rectTransform.sizeDelta = new Vector2(player.getLife()*maxValue, height);
        staminaBar.rectTransform.sizeDelta = new Vector2(player.getStamina()*maxValue, height);
    }
 
}
