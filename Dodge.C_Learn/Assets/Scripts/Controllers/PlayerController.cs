using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprRenderer;
    private Vector2 moveInput;
    PlayerShooter shooter;
    bool isHit;

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

    public void SetPlayer(PlayerType playerType)
    {
        var playerClass = Managers.Character.ReturnAll(playerType);

        animator.runtimeAnimatorController = playerClass.Animator;
        sprRenderer.sprite = playerClass.Sprite;
        shooter.attackSO = playerClass.Info as PlayerInfoSO;
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
        shooter.Power--;
        if (shooter.Power < 0)
        {
            Destroy(gameObject);
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
        shooter.Power++;
    }

    
}
