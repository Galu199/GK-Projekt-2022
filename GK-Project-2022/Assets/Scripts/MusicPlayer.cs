using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> sounds;
    public AudioSource audioSource;
    int i = 0;

    private void Awake()
    {
        audioSource.volume = (float) PlayerPrefs.GetFloat("volume");
        
    }
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.isActiveAndEnabled)
        {
            audioSource.clip = sounds[i];
            audioSource.Play();
            if (i < sounds.Count)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }


}
