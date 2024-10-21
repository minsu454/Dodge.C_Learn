using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprRenderer;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private PlayerShooter shooter;
    private bool isHit;
    private float speed;
    private int curHp; 

    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    /// <summary>
    /// 초기화 Awake : Rigidbody 가져오기
    /// </summary>
    private void Awake()
    {
        shooter = GetComponent<PlayerShooter>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(ConHitEffect());
    }

    public void SetPlayer(PlayerType playerType)
    {
        var playerClass = Managers.Character.ReturnAll(playerType);

        animator.runtimeAnimatorController = playerClass.Animator;
        sprRenderer.sprite = playerClass.Sprite;

        PlayerInfoSO playerInfoSO = playerClass.Info as PlayerInfoSO;
        curHp = playerInfoSO.MaxHp;
        shooter.Power = playerInfoSO.MaxHp - 1;
        speed = playerInfoSO.MoveSpeed;
        shooter.PlayerInfoSO = playerInfoSO;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = moveInput.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position +  nextVec);
    }

    void OnHit()
    {
        curHp--;
        shooter.Power--;
        if (curHp <= 0)
        {
            Destroy(gameObject);
            Managers.Sound.PlaySFX(SfxType.Die_Enemy);
            GameManager.Instance.GameOverPopup();
        }
    }

    IEnumerator ConHitEffect()
    {
        isInvincible = true;
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyProjectile"))
        {
            if (!isInvincible)
            {
                OnHit();
                StartCoroutine(ConHitEffect());
            }
        }
        else if (collision.CompareTag("Power"))
        {
            Upgrade();
        }
        else if (collision.CompareTag("Life"))
        {

        }
    }
    void Upgrade()
    {
        curHp++;
        shooter.Power++;
    }
}
