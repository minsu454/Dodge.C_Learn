using Common.Timer;
using Common.Yield;
using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter
{
    public EnemyType enemyType;             //적 타입
    public EnemyInfoSO EnemyInfoSO;         //적 기본정보 SO

    private const string ENEMY_PROJECTILE = "EnemyProjectile";  //적 투사체 이름
    private int curFireRateCount = 0;                           //연사시 체크될 Count

    protected void Start()
    {
        objType = AttackerType.Enemy;
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
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
            case EnemyType.BattleShip05:
                StartCoroutine(CoTimer.Start(EnemyInfoSO.Delay / 2, () => { StartCoroutine(CoFireBurst()); }));
                StartCoroutine(CoFireAround());
                break;
        }
    }
    
    /// <summary>
    /// 공격을 멈추는 함수
    /// </summary>
    public void Stop()
    {
        switch (enemyType)
        {
            case EnemyType.Corvette01:
                StopCoroutine(CoFire());
                break;
            case EnemyType.Frigate02:
                StopCoroutine(CoFireBurst());
                break;
            case EnemyType.Destroyer03:
            case EnemyType.Cruiser04:
                StopCoroutine(CoFireArc());
                break;
            case EnemyType.BattleShip05:
                StopCoroutine(CoFireBurst());
                StopCoroutine(CoFireAround());
                break;
        }
    }

    /// <summary>
    /// 단발 사격 코루틴
    /// </summary>
    private IEnumerator CoFire()
    {
        while (true)
        {
            SpawnBullet(ENEMY_PROJECTILE, Vector3.zero, Vector2.down, EnemyInfoSO.ProjectileSpeed);

            yield return YieldCache.WaitForSeconds(EnemyInfoSO.Delay);
        }
    }

    /// <summary>
    /// EnemyInfoSO.MaxFireRateCount만큼 연사하는 코루틴
    /// </summary>
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

    /// <summary>
    /// EnemyInfoSO.MaxFireRateCount만큼 호를 그리며 연사하는 코루틴
    /// </summary>
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

    /// <summary>
    /// EnemyInfoSO.MaxFireRateCount만큼 전방향 연사하는 코루틴
    /// </summary>
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