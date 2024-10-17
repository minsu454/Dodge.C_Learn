
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    float spawnTime = 1;

    public Transform[] movePoint;

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
            SpawnProjectile();
            timer = 0;
        }
    }

    private void Spawn()
    {
<<<<<<< Updated upstream
        GameObject enemy = ObjectPoolManager.Instance.GetObject(ObjectType.Object);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
        EnemyMovePoint(enemy.GetComponent<EnemyController>(), Vector2.zero );         
=======
        GameObject enemy = ObjectPoolManager.Instance.GetObject(ObjectType.ProjectileA);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
        PlayerShooter playerShooter = new PlayerShooter();
>>>>>>> Stashed changes
    }

    private void EnemyMovePoint(EnemyController enemy, Vector2 position)
    {
        enemy.transform.position = movePoint[UnityEngine.Random.Range(0, movePoint.Length)].position;
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, position, 0.2f);
    }

    private void SpawnProjectile()
    {
        GameObject projectile = ObjectPoolManager.Instance.GetObject(ObjectType.ProjectileA);
        projectile.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
        MovePoint(projectile.GetComponent<ProjectileController>(), Vector2.zero);
    }

    private void MovePoint(ProjectileController projectile, Vector2 position)
    {
        projectile.transform.position = movePoint[UnityEngine.Random.Range(0, movePoint.Length)].position;
        projectile.transform.position = Vector2.MoveTowards(projectile.transform.position, position, 0.2f);
    }

    // 방향 










    //private void ProjectileFire()
    //{
    //    ProjectileController projectileController = GetComponent<ProjectileController>();


    //    float minAngle = -45f;
    //    float maxAngle = 45f;

    //    float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
    //    float radians = randomAngle * Mathf.Deg2Rad;

    //}




}