using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public Sprite[] sprites;

    float time = 0f;
    public GameObject projectile;
    public float firerate;

    [SerializeField] ObjectType OT; 
    [SerializeField] EnemyType ET; 

    public SpriteRenderer SR;
    public Rigidbody2D RB;

    private void Awake()
    {
        health = maxHealth;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= firerate)
        {
            Shoot();
            time = 0f;
        }
    }
    void Shoot()
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(ObjectType.EnemyProjectile, gameObject.transform, Vector3.zero);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 5f;
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
