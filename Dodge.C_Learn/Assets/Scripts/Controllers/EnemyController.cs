using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
            DieEnemy();
        }
    }
    private void DieEnemy()
    {
        float randomvalue = Random.Range(0f, 1f);
        if (enemyType == EnemyType.Destroyer03)
        {
            if (randomvalue <= 0.1f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        else if (enemyType == EnemyType.Cruiser04 || enemyType == EnemyType.Battleship05)
        {
            if (randomvalue <= 0.4f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        Destroy(gameObject);
        //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
    }

    public void SetEnemy(EnemyType enemyType)
    { 
        var charater = Managers.Character.ReturnAll(enemyType);

        spriteRender.sprite = charater.Sprite;

        EnemyInfoSO enemyInfoSO = charater.Info as EnemyInfoSO;
        sprites = enemyInfoSO.sprites;
        shooter.EnemyInfoSO = enemyInfoSO;
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
