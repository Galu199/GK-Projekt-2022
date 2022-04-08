using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool readyToAtack = false;
    private float timer = 0.0f;
    public float dmgPerSecond = 1.0f;
    public int HP = 10;
    public int DMG = 1;

    void Update()
    {
        if (readyToAtack) return;
        timer += Time.deltaTime;
        if (timer < 1/dmgPerSecond) return;
        timer = 0.0f;
        readyToAtack = true;
    }

    void OnTriggerStay(Collider collider)
    {
        if (!readyToAtack) return;
        collider.GetComponent<Health>().UpdateHealthBar(DMG);
        readyToAtack = false;
    }

}
