using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 100f;
    float time = 0f;
    public float fireRate;
    void Shoot()
    {
        projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
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
