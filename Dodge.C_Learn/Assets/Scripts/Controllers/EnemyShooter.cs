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

    public EnemyType ET;

    int num = 0;

    private void Update()
    {
        if (!isCooldown)
        {
            ApplyAttack();
            Reloading();
        }
        else
        {
            time += Time.deltaTime;
            if (time >= firerate)
            {
                isCooldown = false;
                time = 0f;
            }
        }
    }

    private void ApplyAttack()
    {
        switch( ET )
        {
            case EnemyType.Corvette:
                Fire();
                break;
            case EnemyType.Frigate:
                FireBurst(2);
                break;
            case EnemyType.Destroyer:
                FireAround(12, 1);
                break;
            case EnemyType.Cruiser:
                FireArc(50);
                break;
            case EnemyType.Battleship:
                FireAround(50, 5);
                break;
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
    private void Fire() // 일반적인 사격
    {
        Shoot();
        Reloading();
    }
    private void FireBurst(int fireamount) // 점사
    {
        for(int i = 0; i < fireamount; i++)
        {
            Invoke("Shoot", i * 0.1f);
        }
        Reloading();
    }
    private void FireArc(int fireamount) // 호를 그리면서 사격
    {
        projectileSpeed = 2f;
        GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI* 2 * num /fireamount), -1);
        rb.velocity = dirVec * projectileSpeed;

        num++;
        if (num < fireamount)
            Invoke("ArcFire", 0.1f);
        else
        {
            projectileSpeed = 5f;
        }
    }
    private void FireAround(int roundamount ,int firecount) // 원 형태로 사격
    {
        projectileSpeed = 2f;
        for (int i = 0; i < roundamount; i++)
        {
            GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundamount), Mathf.Sin(Mathf.PI * 2 * i / roundamount));
            rb.velocity = dirVec * projectileSpeed;
        }
        num++;
        if (num < firecount)
            Invoke("FireAround", 1f);
        else
        {
            Reloading();
            projectileSpeed = 5f;
        }
    }
    private void Reloading()
    {
        isCooldown = true;
        num = 0;
    }
}