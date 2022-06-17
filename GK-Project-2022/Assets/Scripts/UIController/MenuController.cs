using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject eventSystem;
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    SceneLoader sceneLoader;

    private void Awake()
    {
        
       sceneLoader = new SceneLoader();
       settingsCanvas.enabled = false;
        
        
    }

    public void OnSettingsButtonClick()
    {
        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }
    public void OnStartButtonClick()
    {
       
        SceneManager.LoadScene(1);
        
        
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
