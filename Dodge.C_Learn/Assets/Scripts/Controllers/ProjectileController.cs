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

    private void ActiveFalse()
    {
        ObjectPoolManager.Instance.ReturnObject(gameObject);

        GameObject paticle = ObjectPoolManager.Instance.GetObject("HitParticle");
        paticle.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && myType == AttackerType.Enemy)
        {
            ActiveFalse();
        }
        else if (collision.CompareTag("Enemy") && myType == AttackerType.Player)
        {
            ActiveFalse();
        }
        else if (collision.CompareTag("Boarder"))
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}
