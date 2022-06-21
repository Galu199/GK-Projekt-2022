using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public enum soundType
    {
        Breath,
        Attack,
        Aggressive
    }

    public AudioSource Source;
    public List<AudioClip> Clips;
    public StandardEnemyBehaviour agent;

    private void Start()
    {
        Source.maxDistance = agent.sightRange * 2f;
    }

    private void Update()
    {
        if (!isActiveAndEnabled) return;
        if (agent.wander)
        {
            if (Source.isPlaying) return;
            Source.clip = Clips[(int)soundType.Breath];
            Source.Play();
        }
        if (agent.follow)
        {
            if (Source.clip == Clips[(int)soundType.Breath])
                Source.Stop();
            if (Source.isPlaying) return;
            Source.clip = Clips[(int)soundType.Attack];
            Source.Play();
        }
        if (agent.attack)
        {
            if (Source.clip != Clips[(int)soundType.Aggressive])
                Source.Stop();
            if (Source.isPlaying) return;
            Source.clip = Clips[(int)soundType.Aggressive];
            Source.Play();
        }
    }
}
