using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> sounds;
    public AudioSource audioSource;
    public Slider VolumeSlider;

    int i = 0;
    float volume;

    private void Start()
    {
        if(VolumeSlider)
            VolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        else
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Update()
    {
        if (VolumeSlider)
        {
            audioSource.volume = VolumeSlider.value;
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
