using Common.Yield;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public float Power;                 //플레이어 총알 레벨
    public PlayerInfoSO PlayerInfoSO;   //플레이어 기본정보 SO

    private void Awake()
    {
        furerateDelay = 0.05f;
        objType = AttackerType.Player;
    }

    private void Start()
    {
        StartCoroutine(CoShoot());
    }

    /// <summary>
    /// 플레이어 공격하는 코루틴
    /// </summary>
    IEnumerator CoShoot()
    {
        while (true)
        {
            for (int i = 0; i < PlayerInfoSO.MaxFireRateCount; i++)
            {
                Shoot();
                yield return YieldCache.WaitForSeconds(furerateDelay);
            }
            yield return YieldCache.WaitForSeconds(PlayerInfoSO.Delay);
        }
    }

    /// <summary>
    /// power에 따라 공격력과 배치 다르게 설정되는 투사체 발사 함수
    /// </summary>
    void Shoot()
    {
        switch (Power)
        {
            case 0:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.zero, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            case 1:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.08f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.08f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            case 2:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.up * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            case 3:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.2f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.2f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            case 4:
                SpawnBullet(PlayerInfoSO.ProjectileB, Vector3.zero, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            case 5:
                SpawnBullet(PlayerInfoSO.ProjectileB, Vector3.up * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.15f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.15f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
            default:
                SpawnBullet(PlayerInfoSO.ProjectileB, Vector3.up * 0.1f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.15f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.15f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.25f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.25f, Vector2.up, PlayerInfoSO.ProjectileSpeed);
                break;
        }
    }
}
