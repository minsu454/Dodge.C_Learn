using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    PlayerShooter shooter;
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;
    /// <summary>
    /// 초기화 Awake : Rigidbody 가져오기
    /// </summary>
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        shooter = GetComponent<PlayerShooter>();
        rb = GetComponent<Rigidbody2D>();
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
        shooter.Power --;
        if (shooter.Power < 0)
        {
            Destroy(gameObject);
        }

    }
    void Upgrade()
    {
        shooter.Power++;
    }
    IEnumerator ConHitEffect()
    {
        isInvincible = true;
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        anim.SetBool("isHit", false);
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
}
