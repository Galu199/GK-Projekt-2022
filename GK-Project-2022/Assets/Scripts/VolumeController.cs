using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    Scrollbar scrollbar;
    [SerializeField]
    AudioSource audioSource;
    
    private void Awake()
    {
        
      
    }
    public void ChangeVolume()
    {
        audioSource.volume = scrollbar.value;
        PlayerPrefs.SetFloat("volume", audioSource.volume);
        Debug.Log(PlayerPrefs.GetFloat("volume"));
    }
    
}
