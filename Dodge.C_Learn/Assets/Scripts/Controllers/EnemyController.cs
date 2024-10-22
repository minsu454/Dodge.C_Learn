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
    [SerializeField] private EnemyType enemyType;       //적 타입

    private int curhealth;                              //현재체력

    [Header("Test")]
    public bool isTest = false;                         //테스트용

    private Sprite[] sprites;                           //적 애니메이션 sprite
    private EnemyShooter shooter;                       //적이 공격하는 class
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

    /// <summary>
    /// 적 설정해주는 함수
    /// </summary>
    public void SetEnemy(EnemyType enemyType)
    {
        var charater = Managers.Character.ReturnAll(enemyType);
        this.enemyType = enemyType;
        spriteRender.sprite = charater.Sprite;

        EnemyInfoSO enemyInfoSO = charater.Info as EnemyInfoSO;
        sprites = enemyInfoSO.Sprites;
        curhealth = enemyInfoSO.MaxHp;
        shooter.enemyType = enemyType;
        shooter.EnemyInfoSO = enemyInfoSO;
    }

    /// <summary>
    /// Dotween을 써서 목표지점까지 이동 후 사격해주는 함수
    /// </summary>
    public void SetDoMove(Vector3 endVec)
    {
        transform.DOMove(endVec, 3).OnComplete( () => {
            if(gameObject.activeSelf)
                shooter.Shoot();
        });
    }
    
    /// <summary>
    /// 적이 움직이는 함수
    /// </summary>
    public void SetMove(object args)
    {
        rb.velocity = (Vector3)args * shooter.EnemyInfoSO.MoveSpeed;
    }

    /// <summary>
    /// 적이 데미지 받는 함수
    /// </summary>
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

    /// <summary>
    /// 피격 이미지 전환 코루틴
    /// </summary>
    private IEnumerator CoSpriteChanger()
    {
        spriteRender.sprite = sprites[1];
        yield return YieldCache.WaitForSeconds(0.05f);
        spriteRender.sprite = sprites[0];
    }

    /// <summary>
    /// 적이 죽는 함수
    /// </summary>
    private void DieEnemy()
    {
        Managers.Sound.PlaySFX(SfxType.Die_Enemy);
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
