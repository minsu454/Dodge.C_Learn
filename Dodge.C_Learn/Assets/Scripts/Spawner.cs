using JetBrains.Annotations;
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
        GameObject enemy = ObjectPoolManager.Instance.GetObject(ObjectType.Object);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
        EnemyController enemyController = GetComponent<EnemyController>();

    }






    //private void ProjectileFire()
    //{
    //    ProjectileController projectileController = GetComponent<ProjectileController>();


    //    float minAngle = -45f;
    //    float maxAngle = 45f;

    //    float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
    //    float radians = randomAngle * Mathf.Deg2Rad;

    //}



    //private void ShootTargetPoint()
    //{
    //    GameObject Projectile = ObjectPoolManager.Instance.GetObject(ObjectType.ProjectileB);
    //    transform.position = Vector2.MoveTowards(Projectile.transform.position, targetPoint, ProjectileController.Speed);
    //}
}