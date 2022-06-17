using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenControler : MonoBehaviour
{
    SceneLoader sceneLoader;
    float time = 0.0f;

    private void Awake()
    {
        sceneLoader = new SceneLoader();
    }
    private void Update()
    {
        time = time + Time.deltaTime;
        Debug.Log(time);
        if (time > 8.0f)
        {
            time = 0f;
            StartCoroutine(sceneLoader.LoadAsyncScene(0));
          
        }
    }

}
