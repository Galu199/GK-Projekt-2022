using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SceneLoader 
{

   public IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
      
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
       
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
    }

    


}
