using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : MonoBehaviour 
{
    float time = 0f;
    public float firerate;
    private float projectileSpeed = 5f;
    public GameObject projectile;
    public Transform firePoint;

    bool isCooldown = false;

    int curPatternCount = 0;
    int maxPatternCount;
    private void Update()
    {
        time += Time.deltaTime;
        if (!isCooldown)
        {
            if (time >= firerate)
            {
                ArcFire();
                time = 0f;
            }
        }
        else
        {
            isCooldown = false;
        }
    }
    private void Fire()
    {
        time = Time.deltaTime;
        if (time >= firerate)
        {
            Shoot();
            time = 0f;
        }
    }

    private void Shoot()
    {
        SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero);
    }
    private void SpawnBullet(ObjectType type, Vector3 vec)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(type, firePoint, vec);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * projectileSpeed;
    }
    private void BurstFire()
    {
        for(int i = 0; i < 4; i++)
        {
            Invoke("Shoot", i * 0.1f);
        }
    }

    private void ArcFire()
    {
        maxPatternCount = 50;
        projectileSpeed = 3f;
        GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI* 2 * curPatternCount/maxPatternCount), -1);
        rb.velocity = dirVec * projectileSpeed;

        curPatternCount++;
        if (curPatternCount < maxPatternCount)
            Invoke("ArcFire", 0.1f);
        else
        {
            Reloading();
            projectileSpeed = 5f;
        }
    }
    private void Reloading()
    {
        isCooldown = true;
        curPatternCount = 0;
    }
}