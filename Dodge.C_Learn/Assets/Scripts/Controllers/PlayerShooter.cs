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
                GameObject bullet1 = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
                GameObject bullet2 = Instantiate(projectileA, firePoint.position + Vector3.left * 0.1f, firePoint.rotation);
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb1.velocity = firePoint.up * projectileSpeed;
                rb2.velocity = firePoint.up * projectileSpeed;
                break;
            case 2:
                //GameObject bullet1 = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
                //GameObject bullet2 = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
                //GameObject bullet3 = Instantiate(projectileA, firePoint.position + Vector3.right * 0.1f, firePoint.rotation);
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
