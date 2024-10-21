using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using Common.Timer;
using Common.Yield;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    private int curhealth;

    [Header("Test")]
    public bool isTest = false;

    private Sprite[] sprites;
    private EnemyShooter shooter;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb;

    private void Awake()
    {
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
        curhealth = enemyInfoSO.MaxHp;
        shooter.EnemyInfoSO = enemyInfoSO;
    }

    public void SetDoMove(Vector3 endVec)
    {
        transform.DOMove(endVec, 3).OnComplete( () => {
            if(gameObject.activeSelf)
                shooter.Shoot();
        });
    }

    public void SetMove(object args)
    {
        rb.velocity = (Vector3)args * shooter.EnemyInfoSO.MoveSpeed;
    }

    void OnHit(int dmg)
    {
        curhealth -= dmg;
        
        if (curhealth <= 0)
        {
            DieEnemy();
        }
        else
        {
            StartCoroutine(CoSpriteChanger());
        }
    }

    private IEnumerator CoSpriteChanger()
    {
        spriteRender.sprite = sprites[1];
        yield return YieldCache.WaitForSeconds(0.05f);
        spriteRender.sprite = sprites[0];
    }

    private void DieEnemy()
    {
        shooter.Stop();
        if (enemyType == EnemyType.Frigate02)
        {
            ObjectPoolManager.Instance.GetObject("ItemPower", transform, Vector3.zero);
        }
        GameManager.Instance.score += shooter.EnemyInfoSO.Score;
        ObjectPoolManager.Instance.ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boarder"))
        {
            shooter.Stop();
            StopCoroutine(CoSpriteChanger());
            ObjectPoolManager.Instance.ReturnObject(gameObject);
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
    }
}
