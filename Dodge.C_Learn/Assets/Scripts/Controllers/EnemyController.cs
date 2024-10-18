using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public Sprite[] sprites;

    [SerializeField] EnemyType enemyType;
    EnemyShooter shooter;
    SpriteRenderer spriteRender;
    Rigidbody2D rb;

    private void Awake()
    {
        health = maxHealth;
        shooter = GetComponent<EnemyShooter>(); 
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shooter.enemyType = enemyType;
        shooter.FirePoint = gameObject.transform;
    }
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRender.sprite = sprites[1];
        Invoke("ReturnSprite", 0.05f);

        if (health <= 0)
        {
            Destroy(gameObject);
            //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRender.sprite = sprites[0];
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
