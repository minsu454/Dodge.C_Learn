using Common.Timer;
using Common.Yield;
using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter
{
    public EnemyType enemyType;
    public EnemyInfoSO EnemyInfoSO;

    private const string ENEMY_PROJECTILE = "EnemyProjectile";

    private int curFireRateCount = 0;

    protected void Start()
    {
        objType = AttackerType.Enemy;
    }

    public void Shoot()
    {
        switch (enemyType)
        {
            case EnemyType.Corvette01:
                StartCoroutine(CoFire());
                break;
            case EnemyType.Frigate02:
                StartCoroutine(CoFireBurst());
                break;
            case EnemyType.Destroyer03:
            case EnemyType.Cruiser04:
                StartCoroutine(CoFireArc());
                break;
            case EnemyType.Battleship05:
                StartCoroutine(CoTimer.Start(EnemyInfoSO.Delay / 2, () => { StartCoroutine(CoFireBurst()); }));
                StartCoroutine(CoFireAround());
                break;
        }
    }
    
    private IEnumerator CoFire()
    {
        while (true)
        {
            SpawnBullet(ENEMY_PROJECTILE, Vector3.zero, Vector2.down, EnemyInfoSO.ProjectileSpeed);

            yield return YieldCache.WaitForSeconds(EnemyInfoSO.Delay);
        }
    }
    private IEnumerator CoFireBurst()
    {
        while (true)
        {
            for (int i = 0; i < EnemyInfoSO.MaxFireRateCount; i++)
            {
                SpawnBullet(ENEMY_PROJECTILE, Vector3.zero, Vector2.down, EnemyInfoSO.ProjectileSpeed);
                yield return YieldCache.WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
            }
            yield return YieldCache.WaitForSeconds(EnemyInfoSO.Delay);
        }
    }
    private IEnumerator CoFireArc()
    {
        while (true)
        {
            while (curFireRateCount < EnemyInfoSO.MaxFireRateCount)
            {
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 5 * curFireRateCount / EnemyInfoSO.MaxFireRateCount), -1);
                SpawnBullet(ENEMY_PROJECTILE, Vector3.zero, dirVec, EnemyInfoSO.ProjectileSpeed);
                curFireRateCount++;
                yield return YieldCache.WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
            }
            yield return YieldCache.WaitForSeconds(EnemyInfoSO.Delay);
            curFireRateCount = 0;
        }
    }
    private IEnumerator CoFireAround()
    {
        while (true)
        {
            curFireRateCount = 0;
            while (curFireRateCount < EnemyInfoSO.MaxFireRateCount)
            {
                int count = 0;

                while (count < 50)
                {
                    float x = Mathf.Cos(Mathf.PI * 2 * count / 45);
                    float y = Mathf.Sin(Mathf.PI * 2 * count / 45);

                    Vector2 dirVec = new Vector2(x, y);
                    SpawnBullet(ENEMY_PROJECTILE, Vector3.zero, dirVec, EnemyInfoSO.ProjectileSpeed);
                    count++;
                }
                curFireRateCount++;
                yield return YieldCache.WaitForSeconds(0.3f);
            }
            yield return YieldCache.WaitForSeconds(EnemyInfoSO.Delay);
            
        }
    }
}