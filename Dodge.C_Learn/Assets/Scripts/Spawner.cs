
using System;
using UnityEngine;
using UnityEngine.UIElements;
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
/*        GameObject enemy = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;*/
    }

    public void SpawnStageEnemy(Pattern pattern)
    {
        for (int i = 0; i < pattern.spawnPointList.Count; i++)
        {
            EnemyType enemyType = pattern.spawnPointList[i].EnemyType;
            Vector3 pos  = pattern.spawnPointList[i].Pos;

            GameObject enemy = ObjectPoolManager.Instance.GetObject(enemyType.ToString());
            enemy.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;

            //EnemyMovePoint(enemy.GetComponent<EnemyController>(), pos);
        }
    }


    private void EnemyMovePoint(EnemyController enemy, Vector2 position)
    {
        enemy.transform.position = movePoint[UnityEngine.Random.Range(0, movePoint.Length)].position;
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, position, 0.2f);
    }

    private void SpawnProjectile()
    {
/*        GameObject projectile = ObjectPoolManager.Instance.GetObject(ObjectType.ProjectileA);
        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        projectile.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
        화면에 내에 있는 랜덤값 shoot
        controller.RandomShoot();
        MovePoint(projectile.GetComponent<ProjectileController>(), Vector2.zero);*/
    }

    private void MovePoint(ProjectileController projectile, Vector2 position)
    {
        projectile.transform.position = movePoint[UnityEngine.Random.Range(0, movePoint.Length)].position;
        projectile.transform.position = Vector2.MoveTowards(projectile.transform.position, position, 0.2f);
    }










    //private void ProjectileFire()
    //{
    //    ProjectileController projectileController = GetComponent<ProjectileController>();


    //    float minAngle = -45f;
    //    float maxAngle = 45f;

    //    float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
    //    float radians = randomAngle * Mathf.Deg2Rad;

    //}




}