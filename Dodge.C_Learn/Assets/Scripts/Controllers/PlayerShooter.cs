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

    public float time = 0.1f;

    void Shoot()
    {
        switch (Power)
        {
            case 0:
                SpawnBullet(projectTile_A, Vector3.zero);
                break;
            case 1:
                SpawnBullet(projectTile_A, Vector3.right * 0.08f);
                SpawnBullet(projectTile_A, Vector3.left * 0.08f);
                break;
            case 2:
                SpawnBullet(projectTile_A, Vector3.right * 0.1f);
                SpawnBullet(projectTile_A, Vector3.left * 0.1f);
                SpawnBullet(projectTile_A, Vector3.up * 0.1f);
                break;      
            case 3:         
                SpawnBullet(projectTile_A, Vector3.right * 0.1f);
                SpawnBullet(projectTile_A, Vector3.right * 0.2f);
                SpawnBullet(projectTile_A, Vector3.left * 0.1f);
                SpawnBullet(projectTile_A, Vector3.left * 0.2f);
                break;
            case 4:
                SpawnBullet(projectTile_B, Vector3.zero);
                break;
            case 5:
                SpawnBullet(projectTile_B, Vector3.up * 0.1f);
                SpawnBullet(projectTile_A, Vector3.right * 0.15f);
                SpawnBullet(projectTile_A, Vector3.left * 0.15f);
                break;
        }
    }
    
    private void SpawnBullet(string projectTile, Vector3 vec)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(projectTile, firePoint , vec);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * projectileSpeed;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time >= FireRate)
        {
            Shoot();
            time = 0f;
        }
    }
}
