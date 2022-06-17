using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGhoulAnimation : MonoBehaviour
{
    public StandardEnemyBehaviour agent;
    public Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<StandardEnemyBehaviour>();
        foreach (AnimationState state in anim)
        {
            state.speed = 1.0f;
            state.weight = 1.0f;
        }
    }

    private void Update()
    {
        anim.CrossFade("Idle");
        if (agent.wander)
        {
            anim.CrossFade("Walk");
        }
        if (agent.follow)
        {
            anim.CrossFade("Run");
        }
        if (agent.attack)
        {
            anim.CrossFade("Attack1");
        }
    }
}
