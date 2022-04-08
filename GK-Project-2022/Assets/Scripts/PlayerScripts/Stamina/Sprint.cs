using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour
{
    public Image staminaBar;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerFPS;
    public int maxStamina = 100;
    public int curStamina = 100;
    public int gainStamina = 1;
    public int loseStamina = 1;

    void Update()
    {
        if (playerFPS.m_IsWalking && curStamina <= maxStamina)
        {
            curStamina += gainStamina;
            staminaBar.fillAmount = (float)curStamina / maxStamina;
        }
        else if (!playerFPS.m_IsWalking && curStamina >= loseStamina)
        {
            curStamina -= loseStamina;
            staminaBar.fillAmount = (float)curStamina / maxStamina;
        }
        if (curStamina < loseStamina)
            playerFPS.m_RunSpeed = playerFPS.m_WalkSpeed;
        else 
            playerFPS.m_RunSpeed = playerFPS.m_WalkSpeed * 2;
    }

}
