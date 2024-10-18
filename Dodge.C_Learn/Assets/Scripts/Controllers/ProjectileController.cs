using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    public int Damage;
    public AttackerType myType;

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Debug.Log("켜지니?");
    }

    public void Shoot(Vector3 vec)
    {
        projectileRb.velocity = vec;
    }
    public void RandomShoot()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        projectileRb.velocity = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && myType == AttackerType.Enemy)
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
        else if (collision.CompareTag("Enemy") && myType == AttackerType.Player)
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
        else if (collision.CompareTag("Boarder"))
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}
