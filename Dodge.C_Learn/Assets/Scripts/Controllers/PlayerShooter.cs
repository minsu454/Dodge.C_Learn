using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectileA;
    public GameObject projectileB;
    public Transform firePoint;
    public float projectileSpeed = 100f;
    float time = 0f;
    public float fireRate;
    public float Power;

    void Shoot()
    {
        switch (Power)
        {
            case 0:
                SpawnBullet(ObjectType.ProjectileA, Vector3.zero);
                break;
            case 1:
                SpawnBullet(ObjectType.ProjectileA, Vector3.right * 0.08f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.left * 0.08f);
                break;
            case 2:
                SpawnBullet(ObjectType.ProjectileA, Vector3.right * 0.1f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.left * 0.1f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.up * 0.1f);
                break;
            case 3:
                SpawnBullet(ObjectType.ProjectileA, Vector3.right * 0.1f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.right * 0.2f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.left * 0.1f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.left * 0.2f);
                break;
            case 4:
                SpawnBullet(ObjectType.ProjectileB, Vector3.zero);
                break;
            case 5:
                SpawnBullet(ObjectType.ProjectileB, Vector3.up * 0.1f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.right * 0.15f);
                SpawnBullet(ObjectType.ProjectileA, Vector3.left * 0.15f);
                break;
        }
    }

    private void SpawnBullet(ObjectType type , Vector3 vec)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(type, firePoint , vec);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * projectileSpeed;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= fireRate)
        {
            Shoot();
            time = 0f;
        }
    }
}
