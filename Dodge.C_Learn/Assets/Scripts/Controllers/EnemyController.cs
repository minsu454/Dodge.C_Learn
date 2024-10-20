using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    public float curhealth;
    public float maxHealth;

    [Header("Test")]
    public bool isTest = false;

    private Sprite[] sprites;
    private EnemyShooter shooter;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb;

    private void Awake()
    {
        curhealth = maxHealth;
        shooter = GetComponent<EnemyShooter>(); 
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shooter.enemyType = enemyType;
    }

    public void Start()
    {
        if (isTest)
            SetEnemy(enemyType);
    }

    public void OnEnable()
    {
        Managers.Event.Subscribe(GameEventType.EnemyMoveTimerCompleted, SetMove);
    }

    public void SetEnemy(EnemyType enemyType)
    {
        var charater = Managers.Character.ReturnAll(enemyType);

        spriteRender.sprite = charater.Sprite;

        EnemyInfoSO enemyInfoSO = charater.Info as EnemyInfoSO;
        sprites = enemyInfoSO.Sprites;
        shooter.EnemyInfoSO = enemyInfoSO;
    }

    public void SetDoMove(Vector3 endVec)
    {
        transform.DOMove(endVec, 3).OnComplete(shooter.Shoot);
    }

    public void SetMove(object args)
    {
        rb.velocity = (Vector3)args * shooter.EnemyInfoSO.MoveSpeed;
    }

    void OnHit(int dmg)
    {
        curhealth -= dmg;
        spriteRender.sprite = sprites[1];
        Invoke("ReturnSprite", 0.05f);
        if (curhealth <= 0)
        {
            DieEnemy();
        }
    }
    private void DieEnemy()
    {
        float randomvalue = UnityEngine.Random.Range(0f, 1f);
        if (enemyType == EnemyType.Destroyer03)
        {
            if (randomvalue <= 0.1f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        else if (enemyType == EnemyType.Cruiser04 || enemyType == EnemyType.BattleShip05)
        {
            if (randomvalue <= 0.4f)
            {
                ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
            }
        }
        Destroy(gameObject);
        //ObjectPoolManager.Instance.ReturnObject(OT, gameObject);
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

    private void OnDisable()
    {
        Managers.Event.Unsubscribe(GameEventType.EnemyMoveTimerCompleted, SetMove);
        shooter?.Stop();
    }
}
