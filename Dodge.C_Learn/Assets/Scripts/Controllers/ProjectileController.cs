using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    float speed = 0.5f;
    [SerializeField] ObjectType OT;
    public int Damage;

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }
    
    public void Shoot(Vector3 vec)
    {
        projectileRb.velocity = vec*speed;
    }
    public void RandomShoot()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        projectileRb.velocity = direction * 0.2f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OT == ObjectType.EnemyProjectile)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Boarder"))
            {
                ObjectPoolManager.Instance.ReturnObject(gameObject);
            }
        }
        else 
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Boarder"))
            {
                ObjectPoolManager.Instance.ReturnObject(gameObject);
            }
        }
    }
}
