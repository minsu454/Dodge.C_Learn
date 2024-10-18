using Common.Yield;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : Shooter
{
    public EnemyType enemyType;

    bool isCooldown = false;

    private const string projectTile_A = "ProjecTileA";
    private const string projectTile_B = "ProjectileB";
    private const string enemyProjectile = "EnemyProjectile";

    int num = 0;
    protected void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Corvette:
                projectileSpeed = 2f;
                StartCoroutine(CoFire());
                break;
            case EnemyType.Frigate:
                projectileSpeed = 4f;
                StartCoroutine(CoFireBurst());
                break;
            case EnemyType.Destroyer:
                projectileSpeed = 2f;
                StartCoroutine(CoFireArc());
                break;
            case EnemyType.Cruiser:
                projectileSpeed = 5f;
                StartCoroutine(CoFireArc());
                break;
            case EnemyType.Battleship:
                projectileSpeed = 2f;
                StartCoroutine(CoFireAround());
                StartCoroutine(CoFireBurst());
                break;
        }
    }
    private void SpawnBullet(string curBullet, Vector3 pos, Vector2 dir)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(curBullet, FirePoint, pos);
        ProjectileController projectTileController = bullet.GetComponent<ProjectileController>();
        projectTileController.myType = ObjectType.Enemy;
        projectTileController.Shoot(dir * projectileSpeed);
    }
    private IEnumerator CoFire()
    {
        while (true)
        {
            SpawnBullet(enemyProjectile, Vector3.zero, Vector2.down);

            yield return YieldCache.WaitForSeconds(5f);
        }
    }
    private IEnumerator CoFireBurst()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                SpawnBullet(enemyProjectile, Vector3.zero, Vector2.down);
                yield return YieldCache.WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
            }
            yield return YieldCache.WaitForSeconds(5f);
        }
    }
    private IEnumerator CoFireArc()
    {
        while (true)
        {
            while (num < 50)
            {
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 5 * num / 50), -1);
                SpawnBullet(enemyProjectile, Vector3.zero, dirVec);
                num++;
                yield return YieldCache.WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
            }
            yield return YieldCache.WaitForSeconds(5f);
            num = 0;
        }
    }
    private IEnumerator CoFireAround()
    {
        while (true)
        {
            int count = 0;
            while (count < 4)
            {
                while (num < 50)
                {
                    Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * num / 50), Mathf.Sin(Mathf.PI * 2 * num / 50));
                    SpawnBullet(enemyProjectile, Vector3.zero, dirVec);
                    num++;
                }
                count++;
                yield return YieldCache.WaitForSeconds(0.1f);
            }
            yield return YieldCache.WaitForSeconds(1f);
            num = 0;
        }
    }
}