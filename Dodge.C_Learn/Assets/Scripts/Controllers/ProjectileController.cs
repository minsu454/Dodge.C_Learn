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

    public void Move(Vector3 vec, float speed)
    {
        projectileRb.velocity = vec * speed;
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
