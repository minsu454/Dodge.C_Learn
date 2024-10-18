using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : Shooter
{
    float time = 0f;
    public Transform FirePoint;
    public EnemyType enemyType;

    bool isCooldown = false;

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
                FireAround(12, 4);
                break;
            case EnemyType.Cruiser:
                FireArc(50);
                break;
            case EnemyType.Battleship:
                FireAround(50, 2);
                break;
        }
    }
    //protected override void Update()
    //{
    //    if (!isCooldown)
    //    {
    //        ApplyAttack();
    //    }
    //    else
    //    {
    //        time += Time.deltaTime;
    //        if (time >= FireRate)
    //        {
    //            isCooldown = false;
    //            time = 0f;
    //        }
    //    }
    //}

    private void ApplyAttack()
    {
        switch(enemyType)
        {
            case EnemyType.Corvette:
                projectileSpeed = 2f;
                Fire();
                break;
            case EnemyType.Frigate:
                projectileSpeed = 4f;
                FireBurst(2);
                break;
            case EnemyType.Destroyer:
                FireAround(12, 4);
                break;
            case EnemyType.Cruiser:
                FireArc(50);
                break;
            case EnemyType.Battleship:
                FireAround(50, 2);
                break;
        }
    }

    private void SpawnBullet(ObjectType type, Vector3 pos, Vector2 dir)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(type, FirePoint, pos);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = dir * projectileSpeed;
    }
    private void Fire() // 일반적인 사격
    {
        SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero, Vector2.down);
        Reloading();
    }
    private void FireBurst(int fireamount) // 점사
    {
        isCooldown = true;
        StartCoroutine(FireBurstCoroutine(fireamount));
        //for(int i = 0; i < fireamount; i++)
        //{
        //    Invoke("Shoot", i * 0.1f);
        //}
        //Reloading();
    }
    private IEnumerator FireBurstCoroutine(int fireamount)
    {
        for (int i = 0; i < fireamount; i++)
        {
            SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero, Vector2.down);
            yield return new WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
        }
        Reloading();
    }
    private void FireArc(int fireamount) // 호를 그리면서 사격
    {
        isCooldown = true;
        num = 0;
        StartCoroutine(FireArcCoroutine(fireamount));
        //projectileSpeed = 2f;
        //GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //bullet.transform.position = transform.position;
        //bullet.transform.rotation = Quaternion.identity;

        //Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI* 5 * num /fireamount), -1);
        //rb.velocity = dirVec.normalized * projectileSpeed;

        //num++;
        //if (num < fireamount)
        //    Invoke("FireArc", 5f);
        //else
        //{
        //    Reloading();
        //    projectileSpeed = 5f;
        //}
    }
    private IEnumerator FireArcCoroutine(int fireamount)
    {
        projectileSpeed = 2f;
        while (num < fireamount)
        {
            //GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //bullet.transform.position = transform.position;
            //bullet.transform.rotation = Quaternion.identity;
            //rb.velocity = dirVec.normalized * projectileSpeed;
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 5 * num / fireamount), -1);
            SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero, dirVec);
            num++;
            yield return new WaitForSeconds(0.8f); // 각 발사 사이에 딜레이
        }
        Reloading();
        projectileSpeed = 5f;
    }
    private void FireAround(int roundamount ,int firecount) // 원 형태로 사격
    {
        isCooldown = true;
        StartCoroutine(FireAroundCoroutine(roundamount, firecount));
        //projectileSpeed = 2f;
        //for (int i = 0; i < roundamount; i++)
        //{
        //    GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
        //    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //    bullet.transform.position = transform.position;
        //    bullet.transform.rotation = Quaternion.identity;

        //    Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundamount), Mathf.Sin(Mathf.PI * 2 * i / roundamount));
        //    rb.velocity = dirVec * projectileSpeed;
        //}
        //num++;
        //if (num < firecount)
        //    Invoke("FireAround", 1f);
        //else
        //{
        //    Reloading();
        //    projectileSpeed = 5f;
        //}
    }
    private IEnumerator FireAroundCoroutine(int roundamount, int firecount)
    {
        projectileSpeed = 2f;
        for (int count = 0; count < firecount; count++)
        {
            for (int i = 0; i < roundamount; i++)
            {
                GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundamount), Mathf.Sin(Mathf.PI * 2 * i / roundamount));
                rb.velocity = dirVec * projectileSpeed;
            }
            yield return new WaitForSeconds(1f); // 각 발사 사이에 딜레이
        }
        Reloading();
        projectileSpeed = 5f;
    }
    private IEnumerator FireAroundCoroutine(int roundamount, int firecount, Action spawnbullet)
    {
        projectileSpeed = 2f;
        for (int count = 0; count < firecount; count++)
        {
            for (int i = 0; i < roundamount; i++)
            {
                spawnbullet();
            }
            yield return new WaitForSeconds(1f); // 각 발사 사이에 딜레이
        }
        Reloading();
        projectileSpeed = 5f;
    }
    private void Reloading()
    {
        num = 0;
    }
    private IEnumerator CoFire()
    {
        while (true)
        {
            SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero, Vector2.down);

            yield return new WaitForSeconds(5f);
        }
    }
    private IEnumerator CoFireBurst()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                SpawnBullet(ObjectType.EnemyProjectile, Vector3.zero, Vector2.down);
                yield return new WaitForSeconds(0.1f); // 각 발사 사이에 딜레이
            }
            yield return new WaitForSeconds(5f);
        }
    }

}