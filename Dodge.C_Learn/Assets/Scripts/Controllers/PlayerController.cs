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
    private PlayerShooter shooter;      //플레이어 공격하는 class
    private float speed;                //플레이어 스피드
    private int curHp;                  //플레이어 현재 체력

    public float invincibilityDuration = 2f;    //무적지속시간
    private bool isInvincible = false;          //무적인지 아닌지 확인하는 변수

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
        StartCoroutine(CoHitEffect());
    }

    /// <summary>
    /// 플레이어 설정해주는 함수
    /// </summary>
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

    /// <summary>
    /// 뉴 인풋 시스템에서 호출해주는 함수
    /// </summary>
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized;
    }

    /// <summary>
    /// 플레이어별 Trail Color를 설정해주는 함수
    /// </summary>
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

    private void FixedUpdate()
    {
        Vector2 nextVec = moveInput.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position +  nextVec);
    }

    /// <summary>
    /// 피격 시 호출되는 함수
    /// </summary>
    void OnHit()
    {
        curHp--;
        shooter.Power--;
        if (curHp <= 0)
        {
            Destroy(gameObject);
            Managers.Sound.PlaySFX(SfxType.Die_Enemy);
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// 피격 시 무적과 애니메이션 주는 코루틴
    /// </summary>
    IEnumerator CoHitEffect()
    {
        isInvincible = true;
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        animator.SetBool("isHit", false);
    }

    /// <summary>
    /// 업그레이드 함수
    /// </summary>
    private void Upgrade()
    {
        curHp++;
        shooter.Power++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyProjectile"))
        {
            if (!isInvincible)
            {
                OnHit();
                StartCoroutine(CoHitEffect());
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
}
