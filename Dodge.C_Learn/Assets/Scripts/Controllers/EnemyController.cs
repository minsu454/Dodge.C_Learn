using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public Sprite[] sprites;

    [SerializeField] ObjectType OT; 
    [SerializeField] EnemyType ET; 

    SpriteRenderer SR;
    Rigidbody2D RB;

    private void Awake()
    {
        health = maxHealth;
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
    }
    void OnHit(int dmg)
    {
        health -= dmg;
        SR.sprite = sprites[1];
        Invoke("ReturnSprite", 0.05f);

        if (health <= 0)
        {
            Destroy(gameObject);
            //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
        }
    }

    void ReturnSprite()
    {
        SR.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boarder"))
        {
            Destroy(gameObject);
            //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
        }

        if(collision.CompareTag("PlayerProjectile"))
        {
            ProjectileController bullet = collision.gameObject.GetComponent<ProjectileController>();
            OnHit(bullet.Damage);
        }
    }
}
