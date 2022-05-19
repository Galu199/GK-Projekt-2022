using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> sounds;
    public AudioSource audioSource;
    // Start is called before the first frame update
    int i = 0;
    
    private void Update()
    {
        if(!audioSource.isPlaying&&audioSource.isActiveAndEnabled)
        {
            if (i != sounds.Count)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            audioSource.clip = sounds[i];
            audioSource.Play();
        }
    }


}
