using System.Collections;
using System.Collections.Generic;
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
                GameObject bullet = Instantiate(projectileA, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = firePoint.up * projectileSpeed;
                break;
            case 1:
                GameObject bullet1 = Instantiate(projectileA, firePoint.position + Vector3.right * 0.08f, firePoint.rotation);
                GameObject bullet2 = Instantiate(projectileA, firePoint.position + Vector3.left * 0.08f, firePoint.rotation);
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb1.velocity = firePoint.up * projectileSpeed;
                rb2.velocity = firePoint.up * projectileSpeed;
                break;
            case 2:
                GameObject bulleta = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
                GameObject bulletb = Instantiate(projectileA, firePoint.position + Vector3.left * 0.1f, firePoint.rotation);
                GameObject bulletc = Instantiate(projectileA, firePoint.position + Vector3.up * 0.1f, firePoint.rotation);
                Rigidbody2D rba = bulleta.GetComponent<Rigidbody2D>();
                Rigidbody2D rbb = bulletb.GetComponent<Rigidbody2D>();
                Rigidbody2D rbc = bulletc.GetComponent<Rigidbody2D>();
                rba.velocity = firePoint.up * projectileSpeed;
                rbb.velocity = firePoint.up * projectileSpeed;
                rbc.velocity = firePoint.up * projectileSpeed;
                break;
            case 3:
                GameObject bulletaa = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
                GameObject bulletbb = Instantiate(projectileA, firePoint.position + Vector3.left * 0.1f, firePoint.rotation);
                GameObject bulletcc = Instantiate(projectileA, firePoint.position + Vector3.right * 0.2f, firePoint.rotation);
                GameObject bulletdd = Instantiate(projectileA, firePoint.position + Vector3.left * 0.2f, firePoint.rotation);
                Rigidbody2D rbaa = bulletaa.GetComponent<Rigidbody2D>();
                Rigidbody2D rbbb = bulletbb.GetComponent<Rigidbody2D>();
                Rigidbody2D rbcc = bulletcc.GetComponent<Rigidbody2D>();
                Rigidbody2D rbdd = bulletdd.GetComponent<Rigidbody2D>();
                rbaa.velocity = firePoint.up * projectileSpeed;
                rbbb.velocity = firePoint.up * projectileSpeed;
                rbcc.velocity = firePoint.up * projectileSpeed;
                rbdd.velocity = firePoint.up * projectileSpeed;
                break;
            case 4:
                GameObject Bullet = Instantiate(projectileB, firePoint.position , firePoint.rotation);
                Rigidbody2D RB = Bullet.GetComponent<Rigidbody2D>();
                RB.velocity = firePoint.up * projectileSpeed;
                break;

        }
        
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
