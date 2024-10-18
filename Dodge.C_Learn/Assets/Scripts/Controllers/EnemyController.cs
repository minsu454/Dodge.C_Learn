using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public Sprite[] sprites;
    public float Score;

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
    }
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRender.sprite = sprites[1];
        Invoke("ReturnSprite", 0.05f);

        if (health <= 0)
        {
            DistroyEnemy();
        }
    }

    private void DistroyEnemy()
    {
        float randomvalue = Random.Range(0f, 1f);
        if (enemyType == EnemyType.Destroyer)
        {
            if (randomvalue <=  0.1f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        else if(enemyType == EnemyType.Cruiser || enemyType == EnemyType.Battleship)
        {
            if (randomvalue <= 0.4f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        Destroy(gameObject);
        //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
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
