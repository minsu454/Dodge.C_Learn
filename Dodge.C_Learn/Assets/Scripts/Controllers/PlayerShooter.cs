using Common.Yield;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public Transform firePoint;
    public float Power;

    private const string projectTile_A = "ProjectileA";
    private const string projectTile_B = "ProjectileB";

    private void Start()
    {
        objType = ObjectType.Player;
        StartCoroutine(CoShoot());
    }

    IEnumerator CoShoot()
    {
        while (true)
        {
            Shoot();

            yield return YieldCache.WaitForSeconds(attackSO.delay);
        }
    }

    void Shoot()
    {
        switch (Power)
        {
            case 0:
                SpawnBullet(projectTile_A, Vector3.zero, Vector2.up);
                break;
            case 1:
                SpawnBullet(projectTile_A, Vector3.right * 0.08f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.left * 0.08f, Vector2.up);
                break;
            case 2:
                SpawnBullet(projectTile_A, Vector3.right * 0.1f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.left * 0.1f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.up * 0.1f, Vector2.up);
                break;
            case 3:
                SpawnBullet(projectTile_A, Vector3.right * 0.1f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.right * 0.2f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.left * 0.1f, Vector2.up);
                SpawnBullet(projectTile_A, Vector3.left * 0.2f, Vector2.up);
                break;
            case 4:
                SpawnBullet(projectTile_B, Vector3.zero, Vector2.up);
                break;
            case 5:
                SpawnBullet(projectTile_B, Vector3.up * 0.1f, Vector2.zero);
                SpawnBullet(projectTile_A, Vector3.right * 0.15f, Vector2.zero);
                SpawnBullet(projectTile_A, Vector3.left * 0.15f, Vector2.zero);
                break;
        }
    }
}
