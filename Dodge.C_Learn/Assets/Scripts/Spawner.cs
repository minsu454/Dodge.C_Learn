using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    float spawnTime = 1;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        GameObject enemy = ObjectPoolManager.Instance.GetObject(ObjectType.ProjectileA);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
    }

}
