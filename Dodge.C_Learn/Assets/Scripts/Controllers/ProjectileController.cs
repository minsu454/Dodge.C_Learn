using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    float speed = 0.5f;
    [SerializeField] ObjectType OT;
    public float Damage;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Shoot(Vector3 vec)
    {
        rigidbody2D.velocity = vec*speed;
    }
    public void RandomShoot()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        rigidbody2D.velocity = direction * 0.2f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Boarder"))
        {
            ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
        }
    }
}
