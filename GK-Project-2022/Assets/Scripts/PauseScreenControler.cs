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

        SceneManager.LoadScene(0);
       // StartCoroutine(sceneLoader.LoadAsyncScene(0));
    }

    public void OnBackToGameButtonClick()
    {
        pauseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.enabled)
                pauseScreen.enabled = true;
            else pauseScreen.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Z)&&pauseScreen.enabled)
        {
            StartCoroutine(sceneLoader.LoadAsyncScene(0));
        }
       
    }

  
}
