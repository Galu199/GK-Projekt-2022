using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PauseScreenControler : MonoBehaviour
{
    
  
    [SerializeField] Canvas pauseScreen;
    SceneLoader sceneLoader;

    

    private void Awake()
    {
       
        pauseScreen.enabled = false;
        sceneLoader = new SceneLoader();
    }

    public void OnBackToMenuButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadSceneAsync(0);
    }

    public void OnBackToGameButtonClick()
    {
       
        pauseScreen.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.enabled)
            {
                pauseScreen.enabled = true;
                Time.timeScale = 0;
               
            }
            else { pauseScreen.enabled = false;
                Time.timeScale = 1;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)&&pauseScreen.enabled)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(1);
            StartCoroutine(sceneLoader.LoadAsyncScene(0));
        }
       
    }

  
}
