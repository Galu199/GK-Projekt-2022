using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> sounds;
    public AudioSource audioSource;
    public Scrollbar VolumeBar;

    int i = 0;
    float volume;

    private void Start()
    {
        if(VolumeBar)
            VolumeBar.value = PlayerPrefs.GetFloat("MusicVolume");
        else
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Update()
    {
        if (VolumeBar)
        {
            audioSource.volume = VolumeBar.value;
        }
        volume = audioSource.volume;

        if (!audioSource.isPlaying && audioSource.isActiveAndEnabled)
        {
            if (i >= sounds.Count)
            {
                i = 0;
            }
            audioSource.clip = sounds[i];
            audioSource.Play();
            i++;
        }
    }

    private void OnDestroy()
    {
        VolumePrefs();
    }

    public void VolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

}
