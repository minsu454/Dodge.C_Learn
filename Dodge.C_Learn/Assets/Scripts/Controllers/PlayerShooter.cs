using Common.Yield;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public Transform firePoint;
    public float Power;
    public PlayerInfoSO PlayerInfoSO;

    private void Start()
    {
        objType = AttackerType.Player;
        StartCoroutine(CoShoot());
    }

    IEnumerator CoShoot()
    {
        while (true)
        {
            for (int i = 0; i < PlayerInfoSO.FireRate; i++)
            {
                Shoot();
                yield return YieldCache.WaitForSeconds(FIRERATE_DELAY);
            }
            yield return YieldCache.WaitForSeconds(PlayerInfoSO.delay);
        }
    }

    void Shoot()
    {
        switch (Power)
        {
            case 0:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.zero, Vector2.up * PlayerInfoSO.Speed);
                break;
            case 1:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.08f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.08f, Vector2.up * PlayerInfoSO.Speed);
                break;
            case 2:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.up * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                break;
            case 3:
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.2f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.2f, Vector2.up * PlayerInfoSO.Speed);
                break;
            case 4:
                SpawnBullet(PlayerInfoSO.ProjectileB, Vector3.zero, Vector2.up * PlayerInfoSO.Speed);
                break;
            case 5:
                SpawnBullet(PlayerInfoSO.ProjectileB, Vector3.up * 0.1f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.right * 0.15f, Vector2.up * PlayerInfoSO.Speed);
                SpawnBullet(PlayerInfoSO.ProjectileA, Vector3.left * 0.15f, Vector2.up * PlayerInfoSO.Speed);
                break;
        }
    }
}
