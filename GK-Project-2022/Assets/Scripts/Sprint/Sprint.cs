using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerFPS;
    public Image staminaBar;
    public float maxStamina = 100;
    public float curStamina = 100;
    public float gainStamina = 1f;
    public float loseStamina = 1f;

    void Update()
    {
        if (playerFPS.m_IsWalking && curStamina <= maxStamina)
        {
            curStamina += gainStamina;
            staminaBar.fillAmount = curStamina / maxStamina;
        }
        else if (!playerFPS.m_IsWalking && curStamina >= loseStamina)
        {
            curStamina -= loseStamina;
            staminaBar.fillAmount = curStamina / maxStamina;
        }
        if (curStamina < loseStamina)
            playerFPS.m_RunSpeed = playerFPS.m_WalkSpeed;
        else 
            playerFPS.m_RunSpeed = playerFPS.m_WalkSpeed * 2;
    }

}
