using System;
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
        if (collider == null) return;
        if (!readyToAtack) return;
        if (collider.GetComponent<Health>() == null) return;
        collider.GetComponent<Health>().UpdateHealthBar(DMG);
        readyToAtack = false;
    }

    public void Teleport(Vector3 position)
    {
        gameObject.SetActive(false);
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

}
