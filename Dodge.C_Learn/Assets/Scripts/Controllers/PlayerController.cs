using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
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
        setPlayerTrailColor(playerType);

        PlayerInfoSO playerInfoSO = playerClass.Info as PlayerInfoSO;
        curHp = playerInfoSO.MaxHp;
        shooter.Power = playerInfoSO.MaxHp - 1;
        speed = playerInfoSO.MoveSpeed;
        shooter.PlayerInfoSO = playerInfoSO;
    }

    void setPlayerTrailColor(PlayerType playerType)
    {
        Color startColor = Color.black;
        Color endColor = Color.black;

        if (playerType == PlayerType.BlueJet) endColor = new Color(0, 0.5f, 1); // Blue
        else if (playerType == PlayerType.RedFighter) endColor = new Color(1, 0, 0); // Red
        else if (playerType == PlayerType.YellowCanaria) endColor = new Color(1, 1, 0); // Yellow

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        trailRenderer.colorGradient = gradient;
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
