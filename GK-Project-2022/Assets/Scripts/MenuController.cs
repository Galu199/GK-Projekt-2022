using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{


    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;

    private void Awake()
    {
       settingsCanvas.enabled = false;
    }

    public void OnSettingsButtonClick()
    {
        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }
    public void OnStartButtonClick()
    {
       

        
    }

    public void OnExitButtonClick()
    {
        
        Application.Quit();
    }

    public void OnBackToMenuButtonClick()
    {
        settingsCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }

 
}
